using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Games.WordPushGame
{
    public class WPGCalender : NetworkBehaviour
    {
        [SerializeField]
        Text text;

        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        [Command]
        public void CmdSetCalender ( int month, int day )
        {
            RpcSetCalender (month, day);
        }

        [ClientRpc]
        void RpcSetCalender ( int month, int day )
        {
            text.text = month.ToString () + "/" + day.ToString ();
        }
    }
}