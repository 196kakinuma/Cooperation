﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networks;
using UnityEngine.Networking;

namespace Main
{
    public class MainSceneinitializer : MonoBehaviour
    {

        [SerializeField]
        GameObject trackerMasterPref;



        // Use this for initialization
        void Start ()
        {
            if ( NetworkInitializer.Instance.cameraType == CameraType.VR )
            {

                //GameMaster生成
                //Instantiate (gameMasterPref);


            }
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}