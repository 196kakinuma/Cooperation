using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Games.GameSystem
{
    public class DoorNetwork : NetworkBehaviour
    {
        [SerializeField]
        Door door;


        [Command]
        public void CmdSetWindowsImageActive ( Enemy.EnemyType type )
        {
            RpcSetWindowImageActive (type);
        }

        [ClientRpc]
        void RpcSetWindowImageActive ( Enemy.EnemyType type )
        {
            door.NtSetImageActive (type);
        }

        [Command]
        public void CmdSetWindowImageNonActive ( Enemy.EnemyType type )
        {
            RpcSetWindowImageNonActive (type);
        }

        [ClientRpc]
        void RpcSetWindowImageNonActive ( Enemy.EnemyType type )
        {
            door.NtSetImageNonActive (type);
        }

    }
}