using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using IkLibrary.Unity;
using Games.GameSystem;
using UnityEngine.UI;

namespace Games.WordMuchGame
{

    public class WMGMaster : SingletonMonoBehaviour<WMGMaster>, IKeyLockGameMaster
    {
        [SerializeField]
        WMGNetworkTransform netTransform;

        /// <summary>
        /// 普通の並び順のcol 不変
        /// </summary>
        [SerializeField]
        WMGCol[] cols;

        /// <summary>
        /// 毎ゲーム変更される
        /// </summary>
        WMGCol[] currentCols;

        //問題系
        [SerializeField]
        WMGQuestion question;
        [SerializeField]
        WMGAnswer answer;

        int randNum = 1;

        bool operationAuthority = false;

        Door currentDoor;
        // Use this for initialization
        void Start ()
        {

        }


        public void SetOperationAuthority ( bool b )
        {
            operationAuthority = b;
            foreach ( var col in cols )
            {
                col.OperationAuthority = this.operationAuthority;
            }
        }
        /// <summary>
        /// ゲーム開始時に呼ばれる
        /// </summary>
        public void Prepare ()
        {
            ResetAll ();
            netTransform.CmdSetActive (false);
        }
        public IEnumerator Initialize ( Door d )
        {
            Debug.Log ("WPG Question init!!!!!!!!!!");
            netTransform.CmdSetActive (true);
            //準備前でもボタンなどは前後できるため.
            ResetAll ();

            //ランダムを生成


            //問題と正解を読み込む
            InitializeQuestion ();

            InitializeAnswer ();

            InitializeHint ();

            InitializeCol ();

            currentDoor = d;
            PrepareMove ();


            yield return true;
        }

        #region INIT
        private void InitializeQuestion ()
        {

        }
        private void InitializeAnswer ()
        {

        }

        private void InitializeHint ()
        {

        }

        private void InitializeCol ()
        {

        }
        #endregion
        /// <summary>
        /// そのゲーム毎に紐づく物をnullにする
        /// </summary>
        public void Clear ()
        {
            currentDoor = null;
            ResetAll ();
            netTransform.CmdSetActive (false);
        }

        public void ResetAll ()
        {

        }

        public void Answer ()
        {
            if ( !operationAuthority ) return;
            bool correction = true;

            if ( correction )
            {
                Debug.Log ("answer is correct!!");
                currentDoor.KeyLock = true;
                ExitRoom ();
            }
            else
            {
                ResetAll ();
                Debug.Log ("missed");
            }
        }

        /// <summary>
        /// 文字を変更するメソッドをRpcにする
        /// </summary>
        public void SetColText ( int colNum, string text )
        {
            netTransform.CmdSetText (colNum, text);
        }

        public void NtSetColText ( int colNum, string text )
        {
            currentCols[colNum].NtSetText (text);
        }

        #region MOVING
        [SerializeField]
        float moveDistance = 2;
        /// <summary>
        /// ドアの下にセット
        /// </summary>
        public void PrepareMove ()
        {
            netTransform.CmdPrepareMove (currentDoor.keyLockGamePosition.position, currentDoor.keyLockGamePosition.forward);
        }

        public void NtPrepareMove ( Vector3 pos, Vector3 forward )
        {
            pos = new Vector3 (pos.x, pos.y - moveDistance, pos.z);
            this.transform.position = pos;
            this.transform.forward = forward;
        }

        /// <summary>
        /// 問題を解くボタン押して,登場させる
        /// </summary>
        public void AppearRoom ()
        {
            netTransform.CmdAppearRoom ();
        }

        public void NtAppearRoom ()
        {
            Debug.Log ("appear");

            //表示する

            //アニメーション
            var pos = transform.position;
            this.transform.position = new Vector3 (pos.x, pos.y + moveDistance, pos.z);
            SetOperationAuthority (true);
        }
        /// <summary>
        /// 問題が正解したら,画面外に出して非アクティブ化
        /// </summary>
        public void ExitRoom ()
        {
            currentDoor.SetButtonActive (true);
            currentDoor.SetButtonWord (false);
            netTransform.CmdExitRoom ();
            //表示を隠す
            Clear ();
        }

        public void NtExitRoom ()
        {
            Debug.Log ("exit");
            SetOperationAuthority (false);

            //あにめーしょん
            var pos = transform.position;
            transform.position = new Vector3 (pos.x, pos.y - moveDistance, pos.z);
        }

        #endregion
    }
}