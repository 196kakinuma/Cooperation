using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;

namespace Games
{
    public enum KeyGames
    {
        WPG,
        DWPG,
        WMG,
        DWMG,
        CG,
        DCG
    }
    public class GameSettings : SingletonMonoBehaviour<GameSettings>
    {
        public KeyGames game;

        public bool tutorial;

        [Range (1, 3)]
        public int EnemyNum = 3;
        [Range (3, 5)]
        public int doorNum = 3;

        [Range (5, 60)]
        public int blankRoomTime;

        /// <summary>
        /// ゲーム時間
        /// </summary>
        [Range (60, 300)]
        public int GameDuration;

    }
}