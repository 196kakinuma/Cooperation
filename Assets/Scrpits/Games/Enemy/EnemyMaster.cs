using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;

namespace Games.Enemy
{

    public class EnemyMaster : SingletonMonoBehaviour<EnemyMaster>
    {
        [SerializeField]
        GameSystem.DoorManager doorManager;
        [SerializeField]
        GameSystem.GameMaster master;
        Enemy[] enemys;

        [SerializeField]
        GameSystem.GameTimer timer;

        // Use this for initialization
        void Start ()
        {

        }

        void Update ()
        {
            if ( master.IsPlaying )
            {
                EnemyCheck ();
            }
        }

        public void EnemyCheck ()
        {
            float time = timer.GetTime ();
            foreach ( var e in enemys )
            {
                if ( e.NextCheckTime < time )
                {
                    e.SetNextCheck (doorManager.EnemyCheckHandle (e));

                }
            }


        }


        /// <summary>
        /// ゲーム開始前の準備
        /// </summary>
        public void InitializeGameStart ()
        {
            GenerateEnemy ();

        }

        void GenerateEnemy ()
        {
            enemys = new Enemy[Games.GameSettings.Instance.EnemyNum];

            //ランダムに生成しても可
            int[] enemyColor = new int[] { 0, 1, 2 };

            for ( int i = 0; i < GameSettings.Instance.EnemyNum; i++ )
            {
                switch ( ( EnemyType ) enemyColor[i] )
                {
                    case EnemyType.RED:
                        enemys[i] = new Enemy (6f, 20f, EnemyType.RED);
                        break;
                    case EnemyType.BLUE:
                        enemys[i] = new Enemy (70f, 40f, EnemyType.BLUE);
                        break;
                    case EnemyType.GREEN:
                        enemys[i] = new Enemy (80f, 25f, EnemyType.GREEN);
                        break;
                }
            }
        }


    }
}