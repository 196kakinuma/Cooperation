using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Games.WordPushGame
{
    public class WPGNetworkTransform : NetworkBehaviour
    {
        [SerializeField]
        WPGMaster master;

        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        [Command]
        public void CmdPushMove ( int i )
        {
            Debug.Log ("cmd ");
            RpcPushMove (i);
        }

        [ClientRpc]
        void RpcPushMove ( int i )
        {
            Debug.Log ("Rpc ");
            var b = master.wpgWordButtons[i];
            b.transform.localPosition = new Vector3 (b.transform.localPosition.x, b.transform.localPosition.y, b.transform.localPosition.z - 0.1f);
        }
    }
}