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

        public string word;

        public int buttonNum;


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