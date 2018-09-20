﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Indicate
{
    public class Indicator : MonoBehaviour
    {
        [SerializeField]
        GameObject forward;
        [SerializeField]
        SteamVR_Controller.Device device;

        private bool isPress;

        // Use this for initialization
        void Start()
        {
            var trackedobj = GetComponent<SteamVR_TrackedObject>();
            device = SteamVR_Controller.Input((int)trackedobj.index);
        }

        // Update is called once per frame
        void Update()
        {
            //if(device. GetPress(SteamVR_Controller.ButtonMask.Trigger))
            //{
            //    isPress = true;
            //}
            //else
            //{
            //    isPress = false;
            //}

            isPress=(device.GetPress(SteamVR_Controller.ButtonMask.Trigger)) ? true : false;
        }

        public Vector3 GetPosition()
        {
            return gameObject.transform.position;
        }

        public Vector3 GetForward()
        {
            return forward.transform.position- gameObject.transform.position;
        }

        //TODO:
        public bool GetIsPress()
        {
            return isPress;
        }
    }
}