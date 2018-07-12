using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.System
{
    public class MetaTransformer : MonoBehaviour
    {
        GameObject tracker = null;
        [SerializeField]
        GameObject hmd;
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
                transform.position = tracker.transform.position;
            }

            if ( Input.GetKeyUp (KeyCode.Space) )
            {
                SetOffset ();
            }
        }

        void SetOffset ()
        {
            transform.rotation = Quaternion.Euler (0, 0, 0);
            var TVec = tracker.GetComponent<C2.Test.forwardCheck> ().forward.transform.position - tracker.transform.position;
            var MVec = hmd.GetComponent<C2.Test.forwardCheck> ().forward.transform.position - hmd.transform.position;

            transform.rotation = Quaternion.Euler (TVec - MVec);
        }
    }
}