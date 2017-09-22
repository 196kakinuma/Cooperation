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
        [Command]
        public void CmdPushMove ()
        {
            Debug.Log ("cmd ");
            RpcPushMove ();
        }

        [ClientRpc]
        void RpcPushMove ()
        {
            Debug.Log ("Rpc ");
            gameObject.transform.localPosition = new Vector3 (gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z - 0.1f);
        }

        /// <summary>
        /// ぼたんが初期位置に戻るあにめーしょん
        /// </summary>
        public void PullMove ()
        {
            CmdPullMove ();
        }

        [Command]
        public void CmdPullMove ()
        {
            RpcPullMove ();
        }

        [ClientRpc]
        void RpcPullMove ()
        {
            gameObject.transform.position = initPos;
        }

    }
}