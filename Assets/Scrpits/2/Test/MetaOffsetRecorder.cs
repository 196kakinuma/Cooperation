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
                keeper[count] = tracker.transform.position - offsetObj[count % 4].transform.position;
                count++;

                if ( count == 12 )//終了
                {
                    exw = new ExcelWriter ();
                    exw.InitializeFile ("MetaOffset");
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