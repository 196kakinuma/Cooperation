using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.System
{
    public class HoloLensTransformer : MonoBehaviour
    {
        GameObject tracker = null;
        [SerializeField]
        GameObject positionParent;
        // Use this for initialization
        void Start ()
        {
            tracker = tracker = GameObject.FindGameObjectWithTag ("tracker");
        }

        // Update is called once per frame
        void Update ()
        {
            if ( tracker == null )
            {
                tracker = GameObject.FindGameObjectWithTag ("tracker");
            }
            else
            {
                positionParent.transform.position = ( -1 * UnityEngine.XR.InputTracking.GetLocalPosition (UnityEngine.XR.XRNode.CenterEye) + tracker.transform.position );
            }

        }
    }
}