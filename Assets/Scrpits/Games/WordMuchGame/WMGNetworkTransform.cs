using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Objects;
namespace Games.WordMuchGame
{
    public class WMGNetworkTransform : NetworkBehaviour
    {
        [SerializeField]
        WMGMaster master;



        [Command]
        public void CmdSetActive ( bool b )
        {
            RpcSetActive (b);
        }

        [ClientRpc]
        void RpcSetActive ( bool b )
        {
            gameObject.SetActive (b);
        }

        #region Move
        [Command]
        public void CmdPrepareMove ( Vector3 pos, Vector3 forward )
        {
            RpcPrepareMove (pos, forward);
        }
        [ClientRpc]
        void RpcPrepareMove ( Vector3 pos, Vector3 forward )
        {
            master.NtPrepareMove (pos, forward);
        }

        [Command]
        public void CmdAppearRoom ()
        {
            RpcAppearRoom ();
        }

        [ClientRpc]
        void RpcAppearRoom ()
        {
            master.NtAppearRoom ();
        }

        [Command]
        public void CmdExitRoom ()
        {
            RpcExitRoom ();
        }
        [ClientRpc]
        void RpcExitRoom ()
        {
            master.NtExitRoom ();
        }
        #endregion
    }
}