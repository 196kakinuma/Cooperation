using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Games.WordPushGame
{
    public class WPGNetworkCreator : NetworkBehaviour
    {

        [HideInInspector]
        public GameObject calender;



        [Command]
        public void CmdCreateSystemButton ( GameObject pref, GameObject parent )
        {
            var b = Instantiate (pref, parent.transform);
            b.transform.parent = parent.transform;
            NetworkServer.Spawn (b);
        }

        [Command]
        public void CmdCreateCalender ( GameObject pref, GameObject parent )
        {
            var c = Instantiate (pref, parent.transform);
            c.transform.parent = parent.transform;
            NetworkServer.Spawn (c);
            calender = c;
        }




    }
}