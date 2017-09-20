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


        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        public void ClickReceive ()
        {
            Debug.Log ("button click");
            wpgAnim.CmdPushMove ();
        }
    }
}