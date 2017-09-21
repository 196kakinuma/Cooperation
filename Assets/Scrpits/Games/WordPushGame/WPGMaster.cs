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
        GameObject buttonPref;
        [SerializeField]
        GameObject resetButton;
        [SerializeField]
        GameObject answerButton;


        [SerializeField]
        WPGNetworkCreator creator;

        [SerializeField]
        Transform[] buttonPosition;
        WPGWordButton[] wpgWordButtons;


        //問題系
        [SerializeField]
        WPGQuestion question;
        List<string> questionList;

        int randNum = 1;


        // Use this for initialization
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR ) return;


            //4*5ボタンの生成
            wpgWordButtons = new WPGWordButton[buttonPosition.Length];
            for ( int i = 0; i < wpgWordButtons.Length; i++ )
            {
                creator.CmdCreateButton (buttonPref, buttonPosition[i].gameObject);
                wpgWordButtons[i] = creator.button;
            }

            //resetとanswerボタンの生成
            creator.CmdCreateSystemButton (resetButton, this.gameObject);
            creator.CmdCreateSystemButton (answerButton, this.gameObject);


            //デバッグ用のゲーム開始
            //StartCoroutine (InitializeWPG ());

        }

        /// <summary>
        /// ゲームを読み込みすべてのクライアントに命令を出す
        /// </summary>
        /// <returns></returns>
        public IEnumerator InitializeWPG ()
        {
            yield return new WaitForSeconds (3f);

            //ランダムを生成


            //条件の配置を変更

            //問題と正解を読み込む
            questionList = new List<string> ();

            for ( int i = 0; i < question.sheets[0].list.Count; i++ )
            {
                //問題
                if ( randNum == 0 )
                {
                    questionList.Add (question.sheets[0].list[i].one);
                }
                else if ( randNum == 1 )
                {
                    questionList.Add (question.sheets[0].list[i].two);
                }
                //正解を読み込む

                wpgWordButtons[i].InitializeButtonInfo (questionList[i], i);
                //文字を設置する
                creator.CmdSetWord (wpgWordButtons[i].gameObject, wpgWordButtons[i].word);
            }

            //準備前でもボタンなどは前後できるため.
            ResetAll ();

        }

        // Update is called once per frame
        void Update ()
        {

        }

        /// <summary>
        /// すべてのボタンを元の位置に戻す。
        /// 押していない状態にする
        /// </summary>
        public void ResetAll ()
        {
            foreach ( var b in wpgWordButtons )
            {
                b.Reset ();
            }

        }

        /// <summary>
        /// 現在の状況で回答する
        /// </summary>
        public void Answer ()
        {
            StartCoroutine (InitializeWPG ());
        }
    }

}