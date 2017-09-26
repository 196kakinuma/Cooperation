using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using IkLibrary.Unity;

namespace Games.WordPushGame
{
    public class WPGMaster : SingletonMonoBehaviour<WPGMaster>
    {

        [SerializeField]
        WPGNetworkTransform netTransform;

        [SerializeField]
        Transform[] buttonPosition;
        [SerializeField]
        Transform calenderPosition;

        public WPGWordButton[] wpgWordButtons;
        public WPGAnswerButton answerButton;
        public WPGResetButton resetButton;
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


        // Use this for initialization
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR ) return;


            //ゲーム開始前に入力が入った場合のエラーを排除
            clientAnswerList = new List<int> ();

            //デバッグ用のゲーム開始
            StartCoroutine (InitializeWPG ());

        }

        /// <summary>
        /// ゲームを読み込みすべてのクライアントに命令を出す
        /// </summary>
        /// <returns></returns>
        public IEnumerator InitializeWPG ()
        {
            Debug.Log ("init!!!!!!!!!!");
            yield return new WaitForSeconds (5f);

            //ランダムを生成


            //条件の配置を変更

            //問題と正解を読み込む
            InitializeQuestion ();

            InitializeAnswer ();

            InitializeCalender ();

            //準備前でもボタンなどは前後できるため.
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

            if ( correction ) Debug.Log ("answer is correct!!");
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
            if ( clientAnswerList.Contains (i) ) return;
            clientAnswerList.Add (i);
            netTransform.CmdPushMove (i);

        }
    }

}