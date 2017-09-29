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

        [SerializeField]
        WMGSelectButton[] buttonUp;

        [SerializeField]
        Text[] text;

        [SerializeField]
        WMGSelectButton[] buttonDown;
        // Use this for initialization
        void Start ()
        {
            for ( int i = 0; i < buttonUp.Length; i++ )
            {
                buttonUp[i].ButtonNum = i;
                buttonDown[i].ButtonNum = i;
            }
        }

        // Update is called once per frame
        void Update ()
        {

        }


        public void SetOperationAuthority ( bool b )
        {

        }
        /// <summary>
        /// ゲーム開始時に呼ばれる
        /// </summary>
        public void Prepare ()
        {

        }
        public IEnumerator Initialize ( Door d )
        {
            yield return null;
        }
        /// <summary>
        /// そのゲーム毎に紐づく物をnullにする
        /// </summary>
        public void Clear ()
        {

        }

        /// <summary>
        /// 文字を変更するメソッドをRpcにする
        /// </summary>
        /// <param name="button"></param>
        /// <param name="buttonNum"></param>
        public void ClickSelectButton ( SELECTBUTTON button, int buttonNum )
        {

        }

        #region MOVING
        /// <summary>
        /// ドアの下にセット
        /// </summary>
        public void PrepareMove ()
        {

        }

        public void NtPrepareMove ( Vector3 pos, Vector3 forward )
        {

        }

        /// <summary>
        /// 問題を解くボタン押して,登場させる
        /// </summary>
        public void AppearRoom ()
        {

        }

        public void NtAppearRoom ()
        {

        }
        /// <summary>
        /// 問題が正解したら,画面外に出して非アクティブ化
        /// </summary>
        public void ExitRoom ()
        {

        }

        public void NtExitRoom ()
        {

        }

        #endregion
    }
}