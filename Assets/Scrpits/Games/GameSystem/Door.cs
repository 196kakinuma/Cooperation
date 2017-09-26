using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.GameSystem
{
    public class Door : MonoBehaviour
    {
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

        /// <summary>
        /// ゲーム開始前に毎回呼ばれる
        /// </summary>
        /// <param name="doorNum"></param>
        public void Initialize ( int doorNum )
        {
            DoorNum = doorNum;
            VisitEnemy = null;
            Visit = false;

        }
    }
}