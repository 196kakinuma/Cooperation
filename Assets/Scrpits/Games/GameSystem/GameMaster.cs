using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;
using UnityEngine.Networking;
using Objects;

namespace Games.GameSystem
{
    /// <summary>
    /// ゲームのすべてを統括。タイマーや記録の開始なども扱う
    /// </summary>
    public class GameMaster : SingletonMonoBehaviour<GameMaster>
    {
        [SerializeField]
        Enemy.EnemyMaster enemyMaster;
        [SerializeField]
        DoorManager doorManager;
        [SerializeField]
        GameTimer timer;


        //システム系
        [SerializeField]
        GameObject startButtonPref;
        StartButtonHandler startButton;

        //ゲームプレハブ系
        List<IKeyLockGameMaster> gameList = new List<IKeyLockGameMaster> ();
        List<IKeyLockGameMaster> nonUsingGameList;
        Dictionary<Door, IKeyLockGameMaster> usingGameAndDoorList;

        [SerializeField]
        GameObject WPGPref;
        Games.WordPushGame.WPGMaster wpgMaster;

        //ヒント系
        [SerializeField]
        GameObject WPGCalender;
        Games.WordPushGame.WPGCalender wpgCalender;

        bool isPlaying = false;
        public bool IsPlaying
        {
            get { return isPlaying; }
            private set { isPlaying = value; }
        }

        // Use this for initialization
        void Start ()
        {

            //startボタン
            var b = Instantiate (startButtonPref);
            this.startButton = b.GetComponent<StartButtonHandler> ();
            NetworkServer.Spawn (b);

            WPGCreate ();

        }

        #region KEYLOCKGAME
        void WPGCreate ()
        {
            //wpg
            var wpg = Instantiate (WPGPref);
            this.wpgMaster = wpg.GetComponent<Games.WordPushGame.WPGMaster> ();
            gameList.Add (wpgMaster);
            NetworkServer.Spawn (wpg);
            //Hintオブジェクトの生成
            var cal = Instantiate (WPGCalender);
            this.wpgCalender = cal.GetComponent<WordPushGame.WPGCalender> ();
            NetworkServer.Spawn (cal);
        }
        #endregion

        /// <summary>
        /// startButtonから呼ばれる.ゲームを開始する
        /// </summary>
        public IEnumerator StartGame ( Coroutine buttonAnimation )
        {
            Coroutine prepare = StartCoroutine (StartPrepare ());
            yield return prepare;
            yield return buttonAnimation;

            //タイマーなどを開始
            timer.GameStart ();
            //敵を動かし始める
            //記録を取り始める

            //スタートの表示
            Debug.Log ("start Game!");
            IsPlaying = true;

        }

        /// <summary>
        /// ゲームの設定を行う
        /// </summary>
        /// <returns></returns>
        IEnumerator StartPrepare ()
        {
            Debug.Log ("startPrepare");
            nonUsingGameList = new List<IKeyLockGameMaster> ();
            usingGameAndDoorList = new Dictionary<Door, IKeyLockGameMaster> ();

            foreach ( var game in gameList )
            {
                nonUsingGameList.Add (game);
            }
            enemyMaster.InitializeGameStart ();
            doorManager.InitializeGameStart ();


            yield return null;
        }

        /// <summary>
        /// ゲーム終了
        /// </summary>
        public void FinishGame ()
        {
            IsPlaying = false;
            timer.GameFinish ();

            //スタートボタンを戻す
            startButton.CmdResetStartButton ();
        }

        /// <summary>
        /// 現在使われていないゲームを、その扉に割り当てる
        /// </summary>
        /// <returns></returns>
        public void ActivateKeyLockGame ( Door d )
        {
            var g = GetRandomKeyLockGame ();
            nonUsingGameList.Remove (g);
            usingGameAndDoorList.Add (d, g);
            g.Initialize (d);
        }

        /// <summary>
        /// 敵が離れたときに,Doorとゲームの紐づけを解く
        /// </summary>
        /// <param name="d"></param>
        public void DisActivateKeyLockGame ( Door d )
        {
            var g = usingGameAndDoorList[d];
            nonUsingGameList.Add (g);
            g.Clear ();
            usingGameAndDoorList.Remove (d);
        }

        /// <summary>
        /// 現在使われていないゲームをランダムに入手する
        /// </summary>
        /// <returns></returns>
        IKeyLockGameMaster GetRandomKeyLockGame ()
        {
            int rand = Random.Range (0, nonUsingGameList.Count);
            return nonUsingGameList[rand];
        }
    }
}