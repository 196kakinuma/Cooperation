using System.Collections;
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



        public int buttonNum;
        public string word;
        public Vector3 initPos;

        public int positionNum;


        void Start ()
        {
            initPos = transform.localPosition;
        }

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
            initPos = transform.localPosition;

        }

        /// <summary>
        /// Rpcによりサーバから全クライアントにコールされる
        /// </summary>
        /// <param name="word"></param>
        public void SetWord ( string word )
        {
            this.text.text = word;
        }


        public void ClickReceive ()
        {
            Debug.Log ("button click");
            WPGMaster.Instance.ReceiveUserResponse (buttonNum, positionNum);
        }

        public void HoldReceive ( Vector3 pos )
        {

        }

        public void PushMove ()
        {
            transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.1f);
        }

        public void ResetPosition ()
        {
            transform.localPosition = initPos;
        }

        public void SelectEffect ()
        {

        }
    }
}