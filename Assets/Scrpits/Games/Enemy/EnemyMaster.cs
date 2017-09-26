using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;

namespace Games.Enemy
{

    public class EnemyMaster : SingletonMonoBehaviour<EnemyMaster>
    {
        Enemy[] enemys;


        void Awake ()
        {


        }
        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        public void GenerateEnemy ()
        {
            enemys = new Enemy[Games.GameSettings.Instance.EnemyNum];

            //ランダムに生成しても可
            int[] enemyColor = new int[] { 0, 1, 2 };

            for ( int i = 0; i < GameSettings.Instance.EnemyNum; i++ )
            {
                switch ( ( EnemyType ) enemyColor[i] )
                {
                    case EnemyType.RED:
                        enemys[i] = new Enemy (60f, 60f, EnemyType.RED);
                        break;
                    case EnemyType.BLUE:
                        enemys[i] = new Enemy (50f, 40f, EnemyType.BLUE);
                        break;
                    case EnemyType.GREEN:
                        enemys[i] = new Enemy (120f, 25f, EnemyType.GREEN);
                        break;
                }
            }
        }
    }
}