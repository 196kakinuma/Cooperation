using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using UnityEngine.UI;

namespace Games.WordPushGame
{
    public class WPGWordButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        Text text;

        [SerializeField]
        WPGButtonAnimation wpgAnim;

        [SerializeField]
        WPGButtonNetworker networker;


        public int buttonNum;

        public void InitializeButtonInfo ( string text, int num )
        {
            buttonNum = num;

        }


        public void ClickReceive ()
        {
            Debug.Log ("button click");
            wpgAnim.CmdPushMove ();
        }

        public void Reset ()
        {
            wpgAnim.CmdPullMove ();
        }
    }
}