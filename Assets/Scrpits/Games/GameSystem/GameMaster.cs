using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;
using UnityEngine.Networking;

namespace Games.GameSystem
{
    /// <summary>
    /// ゲームのすべてを統括。タイマーや記録の開始なども扱う
    /// </summary>
    public class GameMaster : SingletonMonoBehaviour<GameMaster>
    {
        [SerializeField]
        GameObject startButtonPref;

        StartButtonHandler startButton;

        // Use this for initialization
        void Start ()
        {
            var b = Instantiate (startButtonPref);
            this.startButton = b.GetComponent<StartButtonHandler> ();
            NetworkServer.Spawn (b);
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}