using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace C2.System
{
    public class ManipObjectHandler : MonoBehaviour
    {
        NetManipObjectHandler net;
        Vector3 beforeCtrlerPos = Vector3.zero;

        // Use this for initialization
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR )
            {
                Destroy (this);
                return;
            }
            net = this.gameObject.AddComponent<NetManipObjectHandler> ();
            gameObject.tag = "MObj";
        }

        // Update is called once per frame
        void Update ()
        {

        }

        public void Manipulate ( Vector3 pos )
        {
            if ( beforeCtrlerPos == Vector3.zero ) beforeCtrlerPos = pos;
            else
            {
                var offset = beforeCtrlerPos - pos;
                transform.position -= offset;
                beforeCtrlerPos = pos;
            }
        }

        /// <summary>
        /// 選択されたときに光るように
        /// </summary>
        public void CmdEmit ()
        {
            net.CmdEmit ();
        }

        /// <summary>
        /// 選択が終わった時に消えるように
        /// </summary>
        public void CmdDisEmit ()
        {
            net.CmdDisEmit ();
        }

    }
}