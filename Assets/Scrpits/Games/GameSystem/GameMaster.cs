using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;
using UnityEngine.Networking;

namespace Games.GameSystem
{
    /// <summary>
    /// ゲームのすべてを統括。タイマーや記録の開始なども扱う
    /// </summary>
    public class GameMaster : SingletonMonoBehaviour<GameMaster>
    {
        //システム系
        [SerializeField]
        GameObject startButtonPref;
        StartButtonHandler startButton;

        //ゲームプレハブ系
        [SerializeField]
        GameObject WPGPref;
        Games.WordPushGame.WPGMaster wpgMaster;

        // Use this for initialization
        void Start ()
        {
            //startボタン
            var b = Instantiate (startButtonPref);
            this.startButton = b.GetComponent<StartButtonHandler> ();
            NetworkServer.Spawn (b);

            //wpg
            var wpg = Instantiate (WPGPref);
            this.wpgMaster = wpg.GetComponent<Games.WordPushGame.WPGMaster> ();
            NetworkServer.Spawn (wpg);
        }

        // Update is called once per frame
        void Update ()
        {

        }

        /// <summary>
        /// startButtonから呼ばれる.ゲームを開始する
        /// </summary>
        public IEnumerator StartGame ( Coroutine buttonAnimation )
        {
            Coroutine prepare = StartCoroutine (StartPrepare ());
            yield return prepare;
            yield return buttonAnimation;

            //タイマーなどを開始
            //敵を動かし始める
            //記録を取り始める

            //スタートの表示
            Debug.Log ("start Game!");


        }

        /// <summary>
        /// ゲームの設定を行う
        /// </summary>
        /// <returns></returns>
        IEnumerator StartPrepare ()
        {
            Debug.Log ("startPrepare");
            StartCoroutine (wpgMaster.InitializeWPG ());
            yield return null;
        }
    }
}