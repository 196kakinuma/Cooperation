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
        /// <summary>
        /// 次のエネミーの行動時間と、そのエネミーを保持
        /// </summary>
        Dictionary<Enemy, float> nextEnemyMoveList;

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
            foreach ( var e in nextEnemyMoveList )
            {
                if ( e.Value > time )
                {
                    e.Key.SetNextCheck (doorManager.EnemyCheckHandle (e.Key));
                    nextEnemyMoveList.Remove (e.Key);
                    nextEnemyMoveList.Add (e.Key, e.Key.NextCheckTime);
                }
            }
        }


        /// <summary>
        /// ゲーム開始前の準備
        /// </summary>
        public void InitializeGameStart ()
        {
            GenerateEnemy ();
            nextEnemyMoveList = new Dictionary<Enemy, float> ();
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
                        enemys[i] = new Enemy (60f, 60f, EnemyType.RED);
                        break;
                    case EnemyType.BLUE:
                        enemys[i] = new Enemy (50f, 40f, EnemyType.BLUE);
                        break;
                    case EnemyType.GREEN:
                        enemys[i] = new Enemy (120f, 25f, EnemyType.GREEN);
                        break;
                }
                nextEnemyMoveList.Add (enemys[i], enemys[i].NextCheckTime);
            }
        }


    }
}