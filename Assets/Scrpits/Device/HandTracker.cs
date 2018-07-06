using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.Networking;

namespace Device
{

    public class HandTracker : NetworkBehaviour
    {
        [SerializeField]
        UnityEngine.XR.XRNode hand;

        // Use this for initialization
        void Start ()
        {

        }

        [ClientCallback]
        // Update is called once per frame
        void Update ()
        {
            //権限のあるクライアントのみ、位置を変更できる
            if ( hasAuthority )
            {
                var hand = UnityEngine.XR.InputTracking.GetLocalPosition (this.hand);
                transform.localPosition = hand;
                transform.rotation = UnityEngine.XR.InputTracking.GetLocalRotation (this.hand);
            }
        }
    }
}