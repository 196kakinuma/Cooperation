﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IkLibrary.Unity;
using UnityEngine.Networking;
using Objects;
using Games.WordMuchGame;
using Games.DWordMuchGame;
using Games.WordPushGame;
using Games.DWordPushGame;
using Games.CG;
using Games.DCG;

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
        [SerializeField]
        GameObject finishButtonPref;
        FinishButtonHandler finishButton;

        //ゲームプレハブ系
        List<IKeyLockGameMaster> gameList = new List<IKeyLockGameMaster> ();
        List<IKeyLockGameMaster> nonUsingGameList;
        Dictionary<Door, IKeyLockGameMaster> usingGameAndDoorList;

        [SerializeField]
        GameObject WPGPref;
        Games.WordPushGame.WPGMaster wpgMaster;
        [SerializeField]
        GameObject DWPGPref;
        DWPGMaster dwpgMaster;
        [SerializeField]
        GameObject WMGPref;
        WMGMaster wmgMaster;
        [SerializeField]
        GameObject DWMGPref;
        DWMGMaster dwmgMaster;
        [SerializeField]
        GameObject CGPref;
        CGMaster cgMaster;
        [SerializeField]
        GameObject DCGPref;
        DCGMaster dcgMaster;


        //ヒント系
        [SerializeField]
        GameObject WPGCalender;
        Games.WordPushGame.WPGCalender wpgCalender;
        [SerializeField]
        GameObject DWPGCalender;
        DWPGCalender dwpgCalender;
        [SerializeField]
        GameObject WMGChair;
        WMGChair wmgChair;
        [SerializeField]
        GameObject DWMGChair;
        DWMGChair dwmgChair;
        [SerializeField]
        GameObject CGHint;
        CGHint cgHint;
        [SerializeField]
        GameObject DCGHint;
        DCGHint dcgHint;



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
            var f = Instantiate (finishButtonPref);
            this.finishButton = f.GetComponent<FinishButtonHandler> ();
            NetworkServer.Spawn (f);
            finishButton.CmdDisappearButton ();

            WPGCreate ();
            DWPGCreate ();
            WMGCreate ();
            DWMGCreate ();
            CGCreate ();
            DCGCreate ();

        }

        void Update ()
        {
            if ( !isPlaying || GameSettings.Instance.debug || GameSettings.Instance.experiment || GameSettings.Instance.tutorial ) return;
            //ゲームクリア
            if ( timer.IsGameEnd () ) GameClear ();
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

        void DWPGCreate ()
        {
            //wpg
            var dwpg = Instantiate (DWPGPref);
            this.dwpgMaster = dwpg.GetComponent<DWPGMaster> ();
            gameList.Add (dwpgMaster);
            NetworkServer.Spawn (dwpg);
            //Hintオブジェクトの生成
            var cal = Instantiate (DWPGCalender);
            this.dwpgCalender = cal.GetComponent<DWPGCalender> ();
            NetworkServer.Spawn (cal);
        }

        void WMGCreate ()
        {
            var wmg = Instantiate (WMGPref);
            this.wmgMaster = wmg.GetComponent<WMGMaster> ();
            gameList.Add (wmgMaster);
            NetworkServer.Spawn (wmg);

            var chair = Instantiate (WMGChair);
            this.wmgChair = chair.GetComponent<WMGChair> ();
            NetworkServer.Spawn (chair);
        }
        void DWMGCreate ()
        {
            var dwmg = Instantiate (DWMGPref);
            this.dwmgMaster = dwmg.GetComponent<DWMGMaster> ();
            gameList.Add (dwmgMaster);
            NetworkServer.Spawn (dwmg);

            var chair = Instantiate (DWMGChair);
            this.dwmgChair = chair.GetComponent<DWMGChair> ();
            NetworkServer.Spawn (chair);

        }
        void CGCreate ()
        {
            var d = Instantiate (CGPref);
            this.cgMaster = d.GetComponent<CGMaster> ();
            gameList.Add (cgMaster);
            NetworkServer.Spawn (d);

            var h = Instantiate (CGHint);
            this.cgHint = h.GetComponent<CGHint> ();
            NetworkServer.Spawn (h);
        }

        void DCGCreate ()
        {
            var d = Instantiate (DCGPref);
            this.dcgMaster = d.GetComponent<DCGMaster> ();
            gameList.Add (dcgMaster);
            NetworkServer.Spawn (d);

            var h = Instantiate (DCGHint);
            this.dcgHint = h.GetComponent<DCGHint> ();
            NetworkServer.Spawn (h);
        }

        #endregion

        /// <summary>
        /// startButtonから呼ばれる.ゲームを開始する
        /// </summary>
        public IEnumerator ColStartGame ( Coroutine buttonAnimation )
        {
            ExprimentDataKeeper.Instance.InitializeNewFile ();
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
            nonUsingGameList = new List<IKeyLockGameMaster> ();
            usingGameAndDoorList = new Dictionary<Door, IKeyLockGameMaster> ();

            foreach ( var game in gameList )
            {
                game.Prepare ();
                nonUsingGameList.Add (game);
            }
            enemyMaster.InitializeGameStart ();
            doorManager.InitializeGameStart ();


            yield return null;
        }

        void GameClear ()
        {
            Debug.Log ("GameClear");
            FinishGame ();
        }

        public void GameOver ()
        {
            Debug.Log ("GameOver");

            FinishGame ();
        }

        /// <summary>
        /// ゲーム終了
        /// </summary>
        public void FinishGame ()
        {
            IsPlaying = false;
            timer.GameFinish ();
            foreach ( var g in gameList )
            {
                g.SetOperationAuthority (false);
            }

            if ( !GameSettings.Instance.tutorial )
            {
                ExprimentDataKeeper.Instance.SetExperimentData (( KeyGames.NONE ), GameTimer.Instance.GetTime (), "GameFinish");
                ExprimentDataKeeper.Instance.AllWriteDownExcel ();
            }
            //スタートボタンを戻す
            startButton.CmdResetStartButton ();

        }

        /// <summary>
        /// 現在使われていないゲームを、その扉に割り当てる
        /// </summary>
        /// <returns></returns>
        public void ActivateKeyLockGame ( Door d )
        {
            IKeyLockGameMaster g;
            if ( GameSettings.Instance.experiment )
                g = GetExpKeyLockGame ();
            else
                g = GetRandomKeyLockGame ();
            nonUsingGameList.Remove (g);
            usingGameAndDoorList.Add (d, g);
            if ( !GameSettings.Instance.tutorial )
                ExprimentDataKeeper.Instance.SetExperimentData (g.GetName (), GameTimer.Instance.GetTime (), "GameInit");
            StartCoroutine (g.Initialize (d));
        }

        public void AppearKeyLockGame ( Door d )
        {
            if ( !GameSettings.Instance.tutorial )
                ExprimentDataKeeper.Instance.SetExperimentData (KeyGames.NONE, GameTimer.Instance.GetTime (), "Apppear");

            usingGameAndDoorList[d].AppearRoom ();
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
            if ( !GameSettings.Instance.tutorial )
                ExprimentDataKeeper.Instance.SetExperimentData (( KeyGames.NONE ), GameTimer.Instance.GetTime (), "Ghost disappear");
            usingGameAndDoorList.Remove (d);



        }

        public bool IsReachToEndExperimentTime ()
        {
            if ( GameSettings.Instance.experiment && keygamePlayCount == GameSettings.Instance.ExpGameTimes )
                return true;
            else return false;
        }
        public void WriteDownAnswerData ()
        {
            ExprimentDataKeeper.Instance.SetExperimentData (( KeyGames.NONE ), GameTimer.Instance.GetTime (), "Answer Correct");
        }
        public void WriteDownMissData ()
        {
            ExprimentDataKeeper.Instance.SetExperimentData (( KeyGames.NONE ), GameTimer.Instance.GetTime (), "Answer not Correct");
        }

        public void WriteDownResetData ()
        {
            ExprimentDataKeeper.Instance.SetExperimentData (( KeyGames.NONE ), GameTimer.Instance.GetTime (), "Press Reset Button");
        }

        /// <summary>
        /// 現在使われていないゲームをランダムに入手する
        /// </summary>
        /// <returns></returns>
        IKeyLockGameMaster GetRandomKeyLockGame ()
        {

            var rand = UnityEngine.Random.Range (0, nonUsingGameList.Count);
            return nonUsingGameList[rand];
        }



        #region TUTORIAL
        public IEnumerator TutorialStart ( Coroutine buttonAnimation )
        {
            Coroutine prepare = StartCoroutine (TutorialStartPrepare ());
            yield return prepare;
            yield return buttonAnimation;

            //タイマーなどを開始
            //timer.GameStart ();
            //敵を動かし始める
            //記録を取り始める

            finishButton.CmdAppearFinishButton ();

            //スタートの表示
            Debug.Log ("start Game!");
            IsPlaying = true;

        }

        IEnumerator TutorialStartPrepare ()
        {
            Debug.Log ("startPrepare");
            nonUsingGameList = new List<IKeyLockGameMaster> ();
            usingGameAndDoorList = new Dictionary<Door, IKeyLockGameMaster> ();


            switch ( GameSettings.Instance.game )
            {
                case KeyGames.WPG:
                    wpgMaster.Prepare ();
                    nonUsingGameList.Add (wpgMaster);
                    break;
                case KeyGames.DWPG:
                    dwpgMaster.Prepare ();
                    nonUsingGameList.Add (dwpgMaster);
                    break;
                case KeyGames.WMG:
                    wmgMaster.Prepare ();
                    nonUsingGameList.Add (wmgMaster);
                    break;
                case KeyGames.DWMG:
                    dwmgMaster.Prepare ();
                    nonUsingGameList.Add (dwmgMaster);
                    break;
                case KeyGames.CG:
                    cgMaster.Prepare ();
                    nonUsingGameList.Add (cgMaster);
                    break;
                case KeyGames.DCG:
                    dcgMaster.Prepare ();
                    nonUsingGameList.Add (dcgMaster);
                    break;
            }

            enemyMaster.InitializeTutorial ();
            doorManager.InitializeGameStart ();


            yield return null;
        }
        #endregion


        #region EXP
        public IEnumerator ExperimentStart ( Coroutine buttonAnimation )
        {
            ExprimentDataKeeper.Instance.InitializeNewFile ();
            Coroutine prepare = StartCoroutine (ExperimentStartPrepare ());
            yield return prepare;
            yield return buttonAnimation;

            //タイマーなどを開始
            timer.GameStart ();
            //敵を動かし始める
            //記録を取り始める


            //スタートの表示
            Debug.Log ("start Game!");
            ExprimentDataKeeper.Instance.SetExperimentData (( KeyGames.NONE ), GameTimer.Instance.GetTime (), "Game Start");
            IsPlaying = true;

        }

        List<IKeyLockGameMaster> ExpGames;
        IEnumerator ExperimentStartPrepare ()
        {
            keygamePlayCount = 0;
            InitRandomQuestionNum ();
            nonUsingGameList = new List<IKeyLockGameMaster> ();
            usingGameAndDoorList = new Dictionary<Door, IKeyLockGameMaster> ();

            ExpGames = new List<IKeyLockGameMaster> ();
            switch ( GameSettings.Instance.FirstExpGame )
            {
                case KeyGames.WPG:
                    wpgMaster.Prepare ();
                    nonUsingGameList.Add (wpgMaster);
                    ExpGames.Add (wpgMaster);
                    break;
                case KeyGames.DWPG:
                    dwpgMaster.Prepare ();
                    nonUsingGameList.Add (dwpgMaster);
                    ExpGames.Add (dwpgMaster);
                    break;
                case KeyGames.WMG:
                    wmgMaster.Prepare ();
                    nonUsingGameList.Add (wmgMaster);
                    ExpGames.Add (wmgMaster);
                    break;
                case KeyGames.DWMG:
                    dwmgMaster.Prepare ();
                    nonUsingGameList.Add (dwmgMaster);
                    ExpGames.Add (dwmgMaster);
                    break;
                case KeyGames.CG:
                    cgMaster.Prepare ();
                    nonUsingGameList.Add (cgMaster);
                    ExpGames.Add (cgMaster);
                    break;
                case KeyGames.DCG:
                    dcgMaster.Prepare ();
                    nonUsingGameList.Add (dcgMaster);
                    ExpGames.Add (dcgMaster);
                    break;

            }
            enemyMaster.InitializeTutorial ();
            doorManager.InitializeGameStart ();


            yield return null;
        }
        public int keygamePlayCount = 0;
        IKeyLockGameMaster GetExpKeyLockGame ()
        {
            keygamePlayCount++;
            return ExpGames[0];
        }

        int[] randomQuestionNum;
        /// <summary>
        /// 実験で使用する重複しない問題番号を生成
        /// </summary>
        void InitRandomQuestionNum ()
        {
            int[] array = new int[GameSettings.Instance.ExpGameTimes];
            for ( int i = 0; i < array.Length; i++ )
                array[i] = i;
            randomQuestionNum = array.OrderBy (i => Guid.NewGuid ()).ToArray ();
            foreach ( var a in randomQuestionNum )
            {
                Debug.Log (a);
            }
        }

        /// <summary>
        /// keygamePlayCountから重複しない問題番号を渡す
        /// </summary>
        /// <returns></returns>
        public int GetRandomQuestionNum ()
        {
            return randomQuestionNum[keygamePlayCount - 1];
        }
        #endregion

    }
}