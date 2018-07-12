using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace C2.System
{
    public class ManipObjectHandler : MonoBehaviour
    {
        [SerializeField]
        NetManipObjectHandler net;
        Vector3 beforeCtrlerPos = Vector3.zero;
        [SerializeField]
        Material material;

        [SerializeField]
        GameObject emitObj;
        // Use this for initialization
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR )
            {
                Destroy (this);
                return;
            }
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

        //public void Emit ()
        //{
        //    //gameObject.GetComponent<Renderer> ().material.EnableKeyword ("_EMISSION");
        //    material.EnableKeyword ("_EMISSION");
        //    material.SetColor ("_EmissionColor", new Color (255, 204, 0));
        //    //gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", new Color (255, 204, 0));
        //}

        public void Emit ()
        {
            emitObj.gameObject.SetActive (true);
        }
        /// <summary
        /// >
        /// 選択が終わった時に消えるように
        /// </summary>
        public void CmdDisEmit ()
        {
            net.CmdDisEmit ();
        }

        //public void DisEmit ()
        //{
        //    //gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_EMISSION");
        //    material.DisableKeyword ("_EMISSION");
        //}
        public void DisEmit ()
        {
            emitObj.gameObject.SetActive (false);
        }

    }
}