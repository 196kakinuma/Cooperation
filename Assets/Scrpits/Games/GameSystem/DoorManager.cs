using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Games.Enemy;
using UnityEngine.Networking;

namespace Games.GameSystem
{
    public class DoorManager : MonoBehaviour
    {
        [SerializeField]
        GameObject doorPref;
        Door[] doors;

        /// <summary>
        /// Enemyの入っている部屋のリスト
        /// </summary>
        Dictionary<Enemy.Enemy, Door> enemyRoomList;

        public void InitializeGameStart ()
        {
            enemyRoomList = new Dictionary<Enemy.Enemy, Door> ();
            GenarateDoor ();
        }

        void GenarateDoor ()
        {
            if ( doors != null )
            {
                Debug.Log ("ドアの初期化は初めてではありません");
                foreach ( var d in doors )
                {
                    Destroy (d.gameObject);
                }
            }
            doors = new Door[GameSettings.Instance.doorNum];
            var anchors = WorldAnchorKeeper.Instance.doorSpawnTransform;
            for ( int i = 0; i < GameSettings.Instance.doorNum; i++ )
            {

                var d = Instantiate (doorPref, anchors[i]);
                doors[i] = d.GetComponent<Door> ();
                NetworkServer.Spawn (d);
                doors[i].Initialize (i);
            }
        }

        /// <summary>
        /// チェック時間になったエネミーを呼び出し,
        /// 鍵のチェックをするか、入室するか退室するか
        /// </summary>
        /// <returns>部屋には入れたらtrue. 入れなかったらfalse</returns>
        public bool EnemyCheckHandle ( Enemy.Enemy enemy )
        {

            //すでにenemyはどこかの部屋にはいっているなら-鍵チェックして入室か退室
            if ( enemyRoomList.ContainsKey (enemy) )
            {
                var d = enemyRoomList[enemy];
                if ( d.KeyLock )//鍵が締まっている
                {
                    ExitRoom (enemy, d);
                    return false;
                }
                else //鍵が開いている
                {
                    GamesOver (enemy, d);
                    //念のためtrueを返すが,GameTimerを止める
                    return true;
                }
            }
            else //まだenemyはどこの部屋にも入っていなかった
            {
                //入る部屋を決める
                //int decideRoom = Random.Range (0, doors.Length);
                int decideRoom = 1;
                if ( enemyRoomList.ContainsValue (doors[decideRoom]) )//入ろうとした部屋にすでにだれか入っていた
                {
                    Debug.Log ("だれかすでにいた");
                    var d = this.GetNextDoor (decideRoom);

                    //隣のドアを試して開いていなかった
                    if ( enemyRoomList.ContainsValue (d) )
                    {
                        Debug.Log ("埋まっていたよ");
                        return false;
                    }
                    else //開いていたので入室
                    {
                        EnterRoom (enemy, d);
                        return true;
                    }

                }
                else//まだ誰も入っていなかった
                {
                    EnterRoom (enemy, doors[decideRoom]);
                    return true;
                }

            }
        }

        #region ENEMYACTION

        void EnterRoom ( Enemy.Enemy e, Door d )
        {
            if ( d.KeyLock )
            {
                Debug.Log ("前から鍵が締まってました.減点");
                d.KeyLock = false;
            }
            Debug.Log ("入室しました");
            d.SetImageActive (e.Type);
            GameMaster.Instance.ActivateKeyLockGame (d);
            enemyRoomList.Add (e, d);

        }

        void ExitRoom ( Enemy.Enemy e, Door d )
        {

            Debug.Log ("鍵が締まっていました撤退");
            d.SetImageNonActive (e.Type);
            GameMaster.Instance.DisActivateKeyLockGame (d);
            enemyRoomList.Remove (e);
        }

        void GamesOver ( Enemy.Enemy e, Door d )
        {
            Debug.Log ("Games Over");
            //ゲームオーバー
            GameMaster.Instance.FinishGame ();
        }


        Door GetNextDoor ( int i )
        {
            Debug.Log ("before door" + i);
            i++;
            if ( doors.Length == i ) i = 0;

            Debug.Log ("after door" + i);
            return doors[i];
        }
        #endregion

    }
}