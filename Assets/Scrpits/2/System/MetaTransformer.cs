using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.System
{
    public class MetaTransformer : MonoBehaviour
    {
        GameObject tracker = null;
        // Use this for initialization
        void Start ()
        {

            tracker = GameObject.Find (" TrackerSyncObj (Clone)");

        }

        // Update is called once per frame
        void Update ()
        {
            if ( tracker == null )
            {
                tracker = GameObject.Find ("TrackerSyncObj (Clone)");
            }
            else
            {
                transform.position = tracker.transform.position;
            }
        }
    }
}