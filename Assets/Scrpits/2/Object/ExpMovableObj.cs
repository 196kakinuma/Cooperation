using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networks;
using C2.System;

namespace C2.Object
{
    public class ExpMovableObj : MonoBehaviour
    {
        [SerializeField]
        GameObject hideObj;

        [SerializeField]
        GameObject mainObj;

        
        // Use this for initialization
        void Start()
        {
            if(NetworkInitializer.Instance.cameraType==CameraType.VR)
            {
                hideObj.SetActive(true);
            }
            else
            {
                mainObj.SetActive(true);
            }

            if(NetworkInitializer.Instance.cameraType==CameraType.VR && ExpSettings.Instance.AnswerDebug)
            {
                hideObj.SetActive(false);
                mainObj.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}