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
            set { keyLock = value; }
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

        public void SetImageActive ( Enemy.EnemyType type )
        {
            windowImage.gameObject.SetActive (true);
        }

        public void SetImageNonActive ()
        {
            windowImage.gameObject.SetActive (false);
        }
    }
}