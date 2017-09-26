using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.GameSystem
{
    public class DoorManager : MonoBehaviour
    {
        Door[] doors;
        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        public void GanarateDoor ()
        {
            doors = new Door[GameSettings.Instance.doorNum];
            for ( int i = 0; i < GameSettings.Instance.doorNum; i++ )
            {
                doors[i] = new Door ();
                doors[i].Initialize (i);
            }
        }
    }
}