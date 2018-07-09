using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace C2.System
{
    public class NetManipObjectHandler : NetworkBehaviour
    {

        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        [Command]
        public void CmdEmit ()
        {
            RpcEmit ();
        }
        [ClientRpc]
        void RpcEmit ()
        {
            gameObject.GetComponent<Renderer> ().material.EnableKeyword ("_EMISSION");
            gameObject.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", new Color (255, 204, 0));
        }

        [Command]
        public void CmdDisEmit ()
        {
            RpcDisEmit ();
        }

        [ClientRpc]
        void RpcDisEmit ()
        {
            gameObject.GetComponent<Renderer> ().material.DisableKeyword ("_EMISSION");
        }


    }
}