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
        [SerializeField]
        bool IsHolding = false;

        [SerializeField]
        bool IsCounting = false;
        [SerializeField]
        float holdTime = 0f;


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

        float holdTimeFrom = 1f;
        // Update is called once per frame
        void Update ()
        {
            if ( IsCounting )
            {
                holdTime += Time.deltaTime;

                if ( holdTimeFrom < holdTime && selectedObject != null )
                    IsHolding = true;

            }
            if ( IsHolding )
                selectedObject.SendMessage ("HoldReceive", transform.position);
            if ( device.GetPressDown (SteamVR_Controller.ButtonMask.Trigger) )
            {
                IsCounting = true;
            }
            if ( device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger) )
            {
                if ( IsSelecting == true && !IsHolding )
                {
                    selectedObject.SendMessage ("ClickReceive");
                }
                IsCounting = false;
                IsHolding = false;
                holdTime = 0f;
            }
        }

        void OnTriggerEnter ( Collider othre )
        {
            if ( ( othre.gameObject.GetComponent (typeof (Objects.IVRObject)) == null ) || IsHolding ) return;

            Debug.Log ("find");
            IsSelecting = true;
            selectedObject = othre.gameObject;

        }

        void OnTriggerExit ( Collider other )
        {
            if ( other.gameObject != selectedObject || IsHolding ) return;

            IsSelecting = false;
            selectedObject = null;
        }



#endif
    }

}