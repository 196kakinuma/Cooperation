using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Games.WordPushGame
{
    public class WPGNetworkCreator : NetworkBehaviour
    {
        [HideInInspector]
        public WPGWordButton button;

        [Command]
        public void CmdCreateButton ( GameObject pref, GameObject parent )
        {
            var b = Instantiate (pref, parent.transform);
            button = b.GetComponent<WPGWordButton> ();
            b.transform.parent = parent.transform;

            NetworkServer.Spawn (b);
        }


        [Command]
        public void CmdCreateSystemButton ( GameObject pref, GameObject parent )
        {
            var b = Instantiate (pref, parent.transform);
            b.transform.parent = parent.transform;
            NetworkServer.Spawn (b);
        }

        [Command]
        public void CmdSetWord ( GameObject b, string word )
        {
            RpcSetWord (b, word);
        }

        [ClientRpc]
        void RpcSetWord ( GameObject b, string word )
        {
            b.GetComponent<WPGWordButton> ().SetWord (word);
        }


    }
}