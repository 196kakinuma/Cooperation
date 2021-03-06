﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C2.System;
using IkLibrary.Unity;
using IkLibrary;

namespace C2.Exp
{
    public class ExpRecorder : SingletonMonoBehaviour<ExpRecorder>
    {
        

        // Use this for initialization
        void Start()
        {
            if(Networks.NetworkInitializer.Instance.playerType==PlayerType.CLIENT)
            {
                Destroy(this);
            }


            leftRightList = new List<int>();
            offsetList = new List<int>();
            taskNumList = new List<int>();
            reverseList = new List<int>();
            taskResultList = new List<string>();
            collisionList = new List<string>();
            timeList = new List<float>();
        }

        List<int> leftRightList;
        List<int> offsetList;
        List<int> taskNumList;
        List<int> reverseList;
        /// <summary>
        /// タスク情報を毎回保持する
        /// </summary>
        public void AddTaskInfo(int LR,int offset ,int taskNum,int reverse)
        {
            leftRightList.Add(LR);
            offsetList.Add(offset);
            taskNumList.Add(taskNum);
            reverseList.Add(reverse);
        }

        List<string> taskResultList;
        List<string> collisionList; // - でぶつかった番号をつなげる
        /// <summary>
        /// タスクが成功したか、もしくは体がぶつかったものの情報を保持する
        /// </summary>
        public void AddTaskCollisionInfo(string result,string collision)
        {
            taskResultList.Add(result);
            collisionList.Add(collision);
        }


        List<float> timeList;
        /// <summary>
        /// タスクが生成された時間を記録する
        /// 最後の記録はタスクが終了した時間
        /// excel保存時に計算してかかる時間を計測する
        /// </summary>
        /// <param name="time"></param>
        public void AddTaskTimeInfo(float time)
        {
            timeList.Add(time);
        }


        /// <summary>
        /// Excelに書き込み命令
        /// </summary>
        public void FinishExp()
        {
            SaveTaskInfo();
            SaveCollisionInfo();
            SaveTimeInfo();
        }

        void SaveTaskInfo()
        {
            ExcelWriter writer = new ExcelWriter();
            writer.InitializeFile(C2.System.ExpSettings.Instance.ExpName + "TaskInfo");
            foreach(var a in leftRightList)
            {
                writer.WriteWords(a.ToString());
            }
            writer.WriteNewLine();
            foreach (var a in offsetList)
                writer.WriteWords(a.ToString());
            writer.WriteNewLine();
            foreach (var a in taskNumList)
                writer.WriteWords(a.ToString());
            writer.WriteNewLine();

            writer.CloseFile();
        }

        void SaveCollisionInfo()
        {
            ExcelWriter writer = new ExcelWriter();
            writer.InitializeFile(C2.System.ExpSettings.Instance.ExpName + "CollisionInfo");
            foreach(var a in taskResultList)
                writer.WriteWords(a.ToString());
            writer.WriteNewLine();
            foreach(var a in collisionList)
                writer.WriteWords(a.ToString());
            writer.WriteNewLine();

            writer.CloseFile();

        }

        void SaveTimeInfo()
        {
            ExcelWriter writer = new ExcelWriter();
            writer.InitializeFile(C2.System.ExpSettings.Instance.ExpName + "TimeInfo");
            foreach(var a in timeList)
                writer.WriteWords(a.ToString());
            writer.WriteNewLine();
            for (int i=0;i<timeList.Count-1;i++)
            {
                writer.WriteWords((timeList[i + 1] - timeList[i]).ToString());
            }
            writer.WriteNewLine();

            writer.CloseFile();
        }
           
    }
}