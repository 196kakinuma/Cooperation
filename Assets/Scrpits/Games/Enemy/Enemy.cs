using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Games.Enemy
{
    public enum EnemyType
    {
        RED,
        BLUE,
        GREEN
    }

    public class Enemy
    {
        //移動周期
        float cycle;
        public float Cycle
        {
            get { return cycle; }
            private set { cycle = value; }
        }
        //侵入街時間
        float stayTime;
        public float StayTime
        {
            get { return stayTime; }
            private set { stayTime = value; }
        }

        //敵の色
        EnemyType type;
        public EnemyType Type
        {
            get { return type; }
            private set { type = value; }
        }




        public Enemy ( float cycle, float stayTime, EnemyType type )
        {
            this.Cycle = cycle;
            this.StayTime = stayTime;
            this.Type = type;
        }


    }
}