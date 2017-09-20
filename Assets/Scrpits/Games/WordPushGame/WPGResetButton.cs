using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;

namespace Games.WordPushGame
{

    public class WPGResetButton : MonoBehaviour, IVRObject
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
            Debug.Log ("resetButton");
            WPGMaster.Instance.ResetAll ();
        }
    }
}