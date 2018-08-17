using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;


namespace C2.Test
{
    public class MetaOffsetRecorder : MonoBehaviour
    {
        [SerializeField]
        GameObject[] offsetObj;
        [SerializeField]
        GameObject tracker;
        ExcelWriter exw;
        [SerializeField]
        string exptag = "-1";

        Vector3[] keeper;
        int count = 0;
        // Use this for initialization
        void Start ()
        {

            keeper = new Vector3[12];
        }


        // Update is called once per frame
        void Update ()
        {
            if ( Input.GetKeyUp (KeyCode.Space) )
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

                if ( count == 12 )//終了
                {
                    exw = new ExcelWriter ();
                    exw.InitializeFile ("MetaOffset" + exptag);
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