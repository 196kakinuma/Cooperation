using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Device
{
    public class HandTrigger : MonoBehaviour
    {
#if !UNITY_WSA_10_0

        //このスクリプトが貼られているコントローラ
        [SerializeField]
        SteamVR_Controller.Device device;

        //今オブジェクトを選択しているか
        [SerializeField]
        GameObject selectedObject;
        bool IsSelecting = false;

        // Use this for initialization
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR )
            {
                Destroy (this);
                return;
            }

            Debug.Log ("object manip");
            var trackedobj = GetComponent<SteamVR_TrackedObject> ();
            device = SteamVR_Controller.Input (( int ) trackedobj.index);
        }

        // Update is called once per frame
        void Update ()
        {
            if ( device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger) )
            {
                if ( IsSelecting == true )
                {
                    Debug.Log ("Button Pressed");
                    selectedObject.SendMessage ("ClickReceive");
                }
            }
        }

        void OnTriggerEnter ( Collider othre )
        {
            Debug.Log ("trigger on");
            if ( othre.gameObject.GetComponent (typeof (Objects.IVRObject)) != null )
            {
                Debug.Log ("find");
                IsSelecting = true;
                selectedObject = othre.gameObject;
            }
        }

        void OnTriggerExit ( Collider other )
        {
            if ( other.gameObject != selectedObject ) return;

            IsSelecting = false;
            selectedObject = null;
        }



#endif
    }

}