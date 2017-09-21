using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Games.WordPushGame
{
    public class WPGAnswerButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        WPGButtonAnimation anim;
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
            Debug.Log ("answer button");
            WPGMaster.Instance.Answer ();
        }
    }
}