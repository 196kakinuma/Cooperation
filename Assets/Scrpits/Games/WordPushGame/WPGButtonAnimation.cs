using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Games.WordPushGame
{
    /// <summary>
    /// 全クライアントで共有するオブジェクトの変化を記述
    /// </summary>
    public class WPGButtonAnimation : NetworkBehaviour
    {
        Vector3 initPos;
        // Use this for initialization
        void Start ()
        {
            initPos = gameObject.transform.localPosition;
        }


        /// <summary>
        /// コントローラがボタンに被っているとき光らせる
        /// </summary>
        [Command]
        public void CmdSelectingEffect ()
        {
            RpcSelectingEffect ();
        }

        [ClientRpc]
        void RpcSelectingEffect ()
        {

        }



    }
}