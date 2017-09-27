using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Games.GameSystem;
using IkLibrary.Unity;
using Objects;

namespace Games.WordPushGame
{
    public class WPGMaster : SingletonMonoBehaviour<WPGMaster>, IKeyLockGameMaster
    {

        [SerializeField]
        WPGNetworkTransform netTransform;

        public WPGWordButton[] wpgWordButtons;
        public WPGAnswerButton answerButton;
        public WPGResetButton resetButton;
        [HideInInspector]
        public WPGCalender calenderObj;


        //問題系
        [SerializeField]
        WPGQuestion question;
        List<string> questionList;
        [SerializeField]
        WPGAnswer answer;
        List<int> answerList;
        [SerializeField]
        WPGHint calender;
        int month = 1;
        int day = 1;

        int randNum = 1;


        //答えを格納する
        List<int> clientAnswerList;
        /// <summary>
        /// 正解した後の不要な弄りをカットする為に使用
        /// </summary>
        bool canManipulate = false;

        Door currentDoor;


        // Use this for initialization
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR ) return;


            //ゲーム開始前に入力が入った場合のエラーを排除
            clientAnswerList = new List<int> ();

            //デバッグ用のゲーム開始
            //StartCoroutine (InitializeWPG ());

        }

        /// <summary>
        /// ゲームを読み込みすべてのクライアントに命令を出す
        /// </summary>
        /// <returns></returns>
        public IEnumerator Initialize ( Door d )
        {
            Debug.Log ("init!!!!!!!!!!");
            yield return new WaitForSeconds (2f);

            //ランダムを生成


            //問題と正解を読み込む
            InitializeQuestion ();

            InitializeAnswer ();

            InitializeCalender ();

            //TODO: Doorの位置から移動場所を取得
            currentDoor = d;
            PrepareMove ();

            //準備前でもボタンなどは前後できるため.
            ResetAll ();

        }

        public void Clear ()
        {
            currentDoor = null;
            ResetAll ();

        }
        #region INIT
        private void InitializeQuestion ()
        {
            questionList = new List<string> ();
            for ( int i = 0; i < question.sheets[0].list.Count; i++ )
            {
                //問題
                switch ( randNum )
                {
                    case 0:
                        questionList.Add (question.sheets[0].list[i].one);
                        break;
                    case 1:
                        questionList.Add (question.sheets[0].list[i].two);
                        break;
                }

                wpgWordButtons[i].InitializeButtonInfo (questionList[i], i);
                //文字を設置する

                netTransform.CmdSetWord (wpgWordButtons[i].buttonNum, wpgWordButtons[i].word);
            }
        }

        private void InitializeAnswer ()
        {
            answerList = new List<int> ();
            for ( int i = 0; i < answer.sheets[0].list.Count; i++ )
            {
                switch ( randNum )
                {
                    case 0:
                        answerList.Add (answer.sheets[0].list[i].answer1);
                        break;
                    case 1:
                        answerList.Add (answer.sheets[0].list[i].answer2);
                        break;
                }
            }
        }

        private void InitializeCalender ()
        {
            switch ( randNum )
            {
                case 0:
                    month = calender.sheets[0].list[0].Month;
                    day = calender.sheets[0].list[0].Day;
                    break;
                case 1:
                    month = calender.sheets[0].list[1].Month;
                    day = calender.sheets[0].list[1].Day;
                    break;
            }
            netTransform.CmdSetCalender (month, day);
        }
        #endregion

        /// <summary>
        /// すべてのボタンを元の位置に戻す。
        /// 押していない状態にする
        /// </summary>
        public void ResetAll ()
        {
            for ( int i = 0; i < wpgWordButtons.Length; i++ )
            {
                netTransform.CmdPullMove (i);
            }
            clientAnswerList.Clear ();

        }

        /// <summary>
        /// 現在の状況で回答する
        /// </summary>
        public void Answer ()
        {
            if ( !canManipulate ) return;

            bool correction = true;
            if ( clientAnswerList.Count != answerList.Count ) correction = false;

            if ( correction )
            {
                for ( int i = 0; i < clientAnswerList.Count; i++ )
                {
                    if ( clientAnswerList[i] != answerList[i] )
                    {
                        correction = false;
                        break;
                    }
                }
            }

            if ( correction )
            {
                Debug.Log ("answer is correct!!");
                currentDoor.KeyLock = true;
                canManipulate = false;
                ExitRoom ();
            }
            else
            {
                ResetAll ();
                Debug.Log ("missed");
            }

        }

        /// <summary>
        /// クライアントのボタン入力をここで受け取り、リストに保持する
        /// </summary>
        public void ReceiveUserResponse ( int i )
        {
            if ( !canManipulate ) return;
            if ( clientAnswerList.Contains (i) ) return;
            clientAnswerList.Add (i);
            netTransform.CmdPushMove (i);

        }

        #region MOVING
        public void PrepareMove ()
        {
            netTransform.CmdPrepareMove (currentDoor.keyLockGamePosition.position, currentDoor.keyLockGamePosition.forward);
        }

        public void NtPrepareMove ( Vector3 pos, Vector3 forward )
        {
            this.transform.position = pos;
            this.transform.forward = forward;
        }

        public void AppearRoom ()
        {
            netTransform.CmdAppearRoom ();
        }

        public void NtAppearRoom ()
        {
            Debug.Log ("appear");
            //アニメーション
            canManipulate = true;
        }

        public void ExitRoom ()
        {
            netTransform.CmdExitRoom ();
        }

        public void NtExitRoom ()
        {
            Debug.Log ("exit");
            //あにめーしょん

            gameObject.SetActive (false);
        }
        #endregion


    }

}