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
            master.wpgWordButtons[i].PushMove ();

        }

        [Command]
        public void CmdPullMove ( int i )
        {
            RpcPullMove (i);
        }

        [ClientRpc]
        void RpcPullMove ( int i )
        {
            master.wpgWordButtons[i].ResetPosition ();

        }

        [Command]
        public void CmdSetWord ( int i, string text )
        {
            RpcSetWord (i, text);
        }

        [ClientRpc]
        void RpcSetWord ( int i, string text )
        {
            master.wpgWordButtons[i].SetWord (text);
        }

        [Command]
        public void CmdSetCalender ( int month, int day )
        {
            RpcSetCalender (month, day);
        }

        [ClientRpc]
        void RpcSetCalender ( int month, int day )
        {
            master.calenderObj.SetCalender (month, day);
        }
    }
}