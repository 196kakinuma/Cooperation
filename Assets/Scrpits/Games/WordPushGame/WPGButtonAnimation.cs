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
            initPos = gameObject.transform.position;
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

        /// <summary>
        /// ボタンが押されたときのアニメーション
        /// </summary>
        public void PushMove ()
        {
            Debug.Log ("push move");
            CmdPushMove ();
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
            gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 0);
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