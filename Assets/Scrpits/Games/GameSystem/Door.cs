using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
namespace Games.GameSystem
{


    public class Door : MonoBehaviour
    {
        [SerializeField]
        GameObject doorWood;

        [SerializeField]
        Image windowImage;

        [SerializeField]
        DoorNetwork network;

        [SerializeField]
        Text lockText;

        public Transform keyLockGamePosition;

        int doorNum;
        public int DoorNum
        {
            get { return doorNum; }
            private set { doorNum = value; }
        }
        bool visit;
        public bool Visit
        {
            get { return visit; }
            private set { visit = value; }
        }

        Enemy.Enemy enemy;
        public Enemy.Enemy VisitEnemy
        {
            get { return enemy; }
            private set { enemy = value; }
        }

        bool keyLock = false;
        public bool KeyLock
        {
            get { return keyLock; }
            set
            {
                network.CmdSetLockText (value);
                keyLock = value;
            }
        }

        /// <summary>
        /// ゲーム開始前に毎回呼ばれる
        /// </summary>
        /// <param name="doorNum"></param>
        public void Initialize ( int doorNum )
        {
            DoorNum = doorNum;
            VisitEnemy = null;
            Visit = false;
            keyLock = false;
        }

        /// <summary>
        /// ロックされているの表示
        /// </summary>
        public void NtSetLockText ( bool b )
        {
            if ( b )
            {
                lockText.text = "LOCKED";
                lockText.color = Color.red;
            }
            else
            {
                lockText.text = "OPEN";
                lockText.color = Color.green;
            }
        }

        #region WINDOW

        public void SetImageActive ( Enemy.EnemyType type )
        {
            network.CmdSetWindowsImageActive (type);
        }

        public void NtSetImageActive ( Enemy.EnemyType type )
        {
            windowImage.gameObject.SetActive (true);
        }

        public void SetImageNonActive ( Enemy.EnemyType type )
        {
            network.CmdSetWindowImageNonActive (type);
        }

        public void NtSetImageNonActive ( Enemy.EnemyType type )
        {
            Debug.Log ("お化け非表示");
            windowImage.gameObject.SetActive (false);
        }
        #endregion
    }
}