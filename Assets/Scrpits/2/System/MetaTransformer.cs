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
            var Trot = tracker.transform.rotation.eulerAngles;
            Debug.Log ("trot*" + Trot);
            var Mrot = hmd.transform.rotation.eulerAngles;
            Debug.Log ("mrot:" + Mrot);
            //T.y=M.z
            //T.z=M.y
            //T.x=M.-x

            var z = Trot.y - Mrot.z;
            var y = Trot.z - Mrot.y;
            var x = -( Trot.x + Mrot.x );
            Debug.Log ("x" + x + "y" + y + "z" + z);
            transform.rotation.SetEulerAngles (x, y, z);
        }
    }
}