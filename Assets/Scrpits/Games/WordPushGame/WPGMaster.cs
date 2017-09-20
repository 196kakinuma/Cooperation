using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using IkLibrary.Unity;

namespace Games.WordPushGame
{
    public class WPGMaster : SingletonMonoBehaviour<WPGMaster>
    {
        [SerializeField]
        GameObject buttonPref;
        [SerializeField]
        GameObject resetButton;
        [SerializeField]
        GameObject answerButton;

        [SerializeField]
        WPGNetworkCreator creator;

        [SerializeField]
        Transform[] buttonPosition;
        WPGWordButton[] wpgWordButtons;
        // Use this for initialization
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR ) return;

            wpgWordButtons = new WPGWordButton[buttonPosition.Length];
            for ( int i = 0; i < wpgWordButtons.Length; i++ )
            {
                creator.CmdCreateButton (buttonPref, buttonPosition[i].gameObject);
                wpgWordButtons[i] = creator.button;
            }

            creator.CmdCreateSystemButton (resetButton, this.gameObject);
            creator.CmdCreateSystemButton (answerButton, this.gameObject);

        }

        // Update is called once per frame
        void Update ()
        {

        }
    }

}