using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace C2.System
{
    public class NetManipObjectHandler : NetworkBehaviour
    {
        [SerializeField]
        ManipObjectHandler handler;
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
            handler.Emit ();
        }

        [Command]
        public void CmdDisEmit ()
        {
            RpcDisEmit ();
        }

        [ClientRpc]
        void RpcDisEmit ()
        {
            handler.DisEmit ();
        }


    }
}