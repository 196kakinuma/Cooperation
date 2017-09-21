﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using UnityEngine.UI;

namespace Games.WordPushGame
{
    public class WPGWordButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        Text text;

        [SerializeField]
        WPGButtonAnimation wpgAnim;

        [SerializeField]
        WPGButtonNetworker networker;


        public int buttonNum;
        public string word;

        /// <summary>
        /// ゲームを行うVRクライアントでのみ呼ばれる
        /// 情報を保持するために
        /// </summary>
        /// <param name="text"></param>
        /// <param name="num"></param>
        public void InitializeButtonInfo ( string text, int num )
        {
            buttonNum = num;
            this.word = text;

        }

        /// <summary>
        /// Rpcによりサーバから全クライアントにコールされる
        /// </summary>
        /// <param name="word"></param>
        public void SetWord ( string word )
        {
            Debug.Log ("word set");
            this.text.text = word;
        }


        public void ClickReceive ()
        {
            Debug.Log ("button click");
            WPGMaster.Instance.ReceiveUserResponse (buttonNum);
            wpgAnim.CmdPushMove ();
        }

        public void Reset ()
        {
            wpgAnim.CmdPullMove ();
        }
    }
}