﻿using System.Collections;
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
        DCG,
        NONE
    }
    public class GameSettings : SingletonMonoBehaviour<GameSettings>
    {
        public KeyGames game;

        /// <summary>
        /// 一つのゲームの試行回数
        /// </summary>
        public int ExpGameTimes = 5;
        public int totalGameNum = 2;

        public KeyGames FirstExpGame;
        public KeyGames SecondExpGame;

        public bool tutorial;
        public bool experiment;
        public bool detaWrite;

        [Tooltip ("Answerを押したら正解になる")]
        public bool debug;

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