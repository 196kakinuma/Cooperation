using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace IkLibrary.Unity
{
    public class ExcelWriter
    {


        string filename;
        string extension = ".csv";
        StreamWriter sw;


        public void InitializeFile ( string name )
        {
            filename = name + System.DateTime.Today.Date.Day + extension;
#if !UNITY_WSA_10_0

            FileStream fs = File.Create (Application.dataPath + "/ExperimentData/" + filename);

            Debug.Log (name);
            // ファイルストリームを閉じて、変更を確定させる
            // 呼ばなくても using を抜けた時点で Dispose メソッドが呼び出される
            fs.Close ();
#endif

            FileInfo fi = new FileInfo (Application.dataPath + "/ExperimentData/" + filename);
            sw = fi.AppendText ();
            sw.WriteLine ("test output");
        }

        /// <summary>
        /// セルに一つ書き出す
        /// </summary>
        /// <param name="word"></param>
        public void WriteWords ( string word )
        {
            sw.Write (word + ",");
        }

        /// <summary>
        /// 改行を書き出す
        /// </summary>
        public void WriteNewLine ()
        {
            sw.WriteLine ("");
        }

        public void CloseFile ()
        {
            sw.Flush ();
#if !UNITY_WSA_10_0
            sw.Close ();
#endif
        }
    }
}