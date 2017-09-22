using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;

namespace Games.GameSystem
{
    /// <summary>
    /// ゲームのすべてを統括。タイマーや記録の開始なども扱う
    /// </summary>
    public class GameMaster : SingletonMonoBehaviour<GameMaster>
    {
        [HideInInspector]
        public Games.GameSystem.StartButtonHandler startButton;

        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}