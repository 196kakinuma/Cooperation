using System.Collections;
using System.Collections.Generic;
using IkLibrary.Unity;
using UnityEngine;
using System.IO;
namespace Games.GameSystem
{
    public class ExcelWriter : SingletonMonoBehaviour<ExcelWriter>
    {
        public string day;
        public string UserName;
        public string memo = "";

        string extension = ".scv";

        string filename;
        StreamWriter sw;
        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        public void InitializeFile ()
        {
            filename = name + extension;

            FileStream fs = File.Create (Application.dataPath + "/ExperimentData/" + filename);

            Debug.Log (fs.Name);
            // ファイルストリームを閉じて、変更を確定させる
            // 呼ばなくても using を抜けた時点で Dispose メソッドが呼び出される
            fs.Close ();


            FileInfo fi = new FileInfo (Application.dataPath + "/ExperimentData/" + filename);
            sw = fi.AppendText ();
            sw.WriteLine ("test output");
        }

        public void InitWriteUserInfo ()
        {
            sw.WriteLine (day + ",");
            sw.WriteLine (name + ",");

        }

        public void WriteWord ( string word )
        {
            sw.Write (word + ",");
        }

        public void CloseFile ()
        {
            sw.Flush ();
            sw.Close ();
        }
    }
}