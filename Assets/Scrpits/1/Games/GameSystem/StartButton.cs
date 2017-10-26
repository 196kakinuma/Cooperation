﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
namespace Games.GameSystem
{
    public class StartButton : MonoBehaviour, IVRObject
    {
        [SerializeField]
        StartButtonHandler handler;

        bool enabled = true;

        public void SetEnabled ( bool b )
        {
            enabled = b;
        }
        public void ClickReceive ()
        {
            if ( !enabled ) return;

            handler.CmdClickStartButton (GameSettings.Instance.tutorial, GameSettings.Instance.experiment);
        }

        public void HoldReceive ( Vector3 pos )
        {

        }


    }
}