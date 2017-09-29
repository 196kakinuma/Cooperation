using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using Games.GameSystem;

namespace Games.WordMuchGame
{

    public class WMGMaster : MonoBehaviour, IKeyLockGameMaster
    {

        // Use this for initialization
        void Start ()
        {

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