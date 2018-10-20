using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;

public enum EXPNUM
{
    ONE,
    TWO,
    THREE
}
namespace C2.System
{
    public class ExpSettings : SingletonMonoBehaviour<ExpSettings>
    {
        public bool IndicateStart=false;
        public bool IndicateEnd = false;
        public bool IndicateLine = false;
        //MRにこんとろーらを表示するか
        public bool Ctrler4MRDebug = false;

        public bool AnswerDebug = false;

        //今回は使用しない
        public EXPNUM expnum;

        /// <summary>
        /// ファイル名に使用する
        /// </summary>
        public string ExpName;

        public int totalTasknum = 10;
        
    }
}