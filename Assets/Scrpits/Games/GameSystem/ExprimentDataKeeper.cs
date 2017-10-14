﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;

namespace Games.GameSystem
{
    public class ExprimentDataKeeper : SingletonMonoBehaviour<ExprimentDataKeeper>
    {

        public string day;
        public string UserName;
        public string memo = "";

        string extension = ".scv";
        ExcelWriter eWriter;

        List<int> expGameNums;
        List<float> expTimes;
        List<string> expSituations;
        // Use this for initialization
        void Start ()
        {
            eWriter = new ExcelWriter ();
        }


        /// <summary>
        /// 実験前にそのユーザの情報を格納するファイルを生成
        /// </summary>
        void InitializeNewFile ()
        {
            eWriter.InitializeFile (name);
            eWriter.InitWriteUserInfo (day, name);
            expGameNums = new List<int> ();
            expTimes = new List<float> ();
            expSituations = new List<string> ();

        }

        /// <summary>
        /// 書き込む実験データを受け取る
        /// </summary>
        /// <param name="gameNum"></param>
        /// <param name="time"></param>
        /// <param name="situation"></param>
        public void SetExperimentData ( int gameNum, float time, string situation )
        {
            expGameNums.Add (gameNum);
            expTimes.Add (time);
            expSituations.Add (situation);
        }


        /// <summary>
        /// 実験後のデータ書き出し用
        /// </summary>
        void AllWriteDownExcel ()
        {

        }
    }
}