using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using UnityEngine.UI;

namespace Games.WordMuchGame
{
    public enum SELECTBUTTON
    {
        UP,
        DOWN
    }


    public class WMGSelectButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        SELECTBUTTON selectButton = SELECTBUTTON.UP;


        int buttonNum;
        public int ButtonNum
        {
            get { return buttonNum; }
            set { buttonNum = value; }
        }

        public void ClickReceive ()
        {
            WMGMaster.Instance.ClickSelectButton (selectButton, ButtonNum);
        }
    }
}