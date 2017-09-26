using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Games.Enemy;

namespace Games.GameSystem
{
    public class DoorManager : MonoBehaviour
    {
        Door[] doors;
        Dictionary<Enemy.Enemy, Door> roomList;
        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        public void InitializeGameStart ()
        {
            GenarateDoor ();
        }

        void GenarateDoor ()
        {
            doors = new Door[GameSettings.Instance.doorNum];
            for ( int i = 0; i < GameSettings.Instance.doorNum; i++ )
            {
                doors[i] = new Door ();
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
            if ( roomList.ContainsKey (enemy) )
            {
                if ( roomList[enemy].KeyLock )//鍵が締まっている
                {
                    ExitRoom (enemy);
                    return false;
                }
                else //鍵が開いている
                {
                    Debug.Log ("Games Over");
                    //ゲームオーバー
                    GameMaster.Instance.FinishGame ();
                    //念のためtrueを返すが,GameTimerを止める
                    return true;
                }
            }
            else //まだenemyはどこの部屋にも入っていなかった
            {
                //入る部屋を決める
                int decideRoom = 0;
                if ( roomList.ContainsValue (doors[decideRoom]) )//入ろうとした部屋にすでにだれか入っていた
                {
                    var d = this.GetNextDoor (decideRoom);
                    //隣のドアを試して開いていなかった
                    if ( roomList.ContainsValue (d) )
                    {
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

        void EnterRoom ( Enemy.Enemy e, Door d )
        {
            if ( d.KeyLock )
            {
                Debug.Log ("前から鍵が締まってました.減点");
                d.KeyLock = false;
            }
            Debug.Log ("入室しました");
            roomList.Add (e, d);
        }

        void ExitRoom ( Enemy.Enemy e )
        {
            Debug.Log ("鍵が締まっていました撤退");
            roomList.Remove (e);
        }


        Door GetNextDoor ( int i )
        {

            i++;
            if ( doors.Length == i ) i = 0;
            return doors[i];
        }

    }
}