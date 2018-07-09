using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.System
{
    public class HandControl : MonoBehaviour
    {
        [SerializeField]
        SteamVR_Controller.Device device;
        [SerializeField]
        ManipObjectHandler selected = null;

        bool downTrigger = false;
        bool manipulating = false;
        // Use this for initialization
        void Start ()
        {
            Debug.Log (Networks.NetworkInitializer.Instance.cameraType);
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR )
            {
                Destroy (this);
                return;
            }
            var trackedobj = GetComponent<SteamVR_TrackedObject> ();
            device = SteamVR_Controller.Input (( int ) trackedobj.index);
        }

        // Update is called once per frame
        void Update ()
        {

            if ( manipulating )
            {
                selected.Manipulate (transform.position);
            }

            if ( device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger) )
            {
                downTrigger = false;
                PressUpTrigger ();
            }
            if ( device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger) )
            {
                downTrigger = true;
                PressDownTrigger ();
            }
        }

        void PressDownTrigger ()
        {
            if ( selected == null ) return;
            manipulating = true;

        }

        void PressUpTrigger ()
        {
            manipulating = false;
        }

        void OnTriggerEnter ( Collider other )
        {
            if ( downTrigger || other.gameObject.tag != "MObj" ) return;
            selected = other.gameObject.GetComponent<ManipObjectHandler> ();
            selected.CmdEmit ();
        }

        void OnTriggerExit ( Collider other )
        {
            selected.CmdDisEmit ();
            selected = null;

        }
    }
}