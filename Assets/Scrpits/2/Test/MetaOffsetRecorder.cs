using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;


namespace C2.Test
{
    enum MeasureTest
    {
        ROTATE,
        MOVE
    }
    public class MetaOffsetRecorder : MonoBehaviour
    {
        [SerializeField]
        GameObject[] offsetObj;
        SteamVR_Controller.Device device;
        [SerializeField]
        MeasureTest testMode = MeasureTest.ROTATE;
        [SerializeField]
        GameObject tracker;
        ExcelWriter exw;
        [SerializeField]
        string exptag = "-1";

        int total = 0;
        [SerializeField]
        Vector3[] keeper;
        int count = 0;
        // Use this for initialization
        void Start ()
        {
            foreach(var a in offsetObj)
            {
                Debug.Log(a.transform.position);
            }
            var trackedobj = GetComponent<SteamVR_TrackedObject> ();
            device = SteamVR_Controller.Input (( int ) trackedobj.index);
            switch ( testMode )
            {
                case MeasureTest.ROTATE:
                    total = 12;
                    break;
                case MeasureTest.MOVE:
                    total = 8;
                    break;
                default:
                    total = 12;
                    break;
            }
            keeper = new Vector3[total];
        }


        // Update is called once per frame
        void Update ()
        {
            if ( device.GetPressUp (SteamVR_Controller.ButtonMask.Trigger) )
            {
                {
                    Vector3 target;
                    //初期誤差を記憶
                    if ( count < 4 )
                    {
                        target = offsetObj[count].transform.position;
                    }
                    else
                    {
                        target = keeper[count % 4];
                    }
                    keeper[count] = tracker.transform.position - target;
                    count++;

                    if ( count == total )//終了
                    {
                        exw = new ExcelWriter ();
                        exw.InitializeFile ("MetaError" + testMode.ToString () + exptag);
                        foreach ( var a in keeper )
                        {
                            exw.WriteWords (a.x.ToString ());
                            exw.WriteWords (a.y.ToString ());
                            exw.WriteWords (a.z.ToString ());
                            exw.WriteNewLine ();
                        }
                        exw.CloseFile ();
                    }
                }
            }
        }
    }
}