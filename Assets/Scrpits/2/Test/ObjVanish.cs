﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Test
{

    public class ObjVanish : MonoBehaviour
    {

        [SerializeField]
        GameObject block;

        // Use this for initialization
        void Start()
        {
            if (Networks.NetworkInitializer.Instance.cameraType == CameraType.VR)
            {
                block.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {

            if(other.gameObject.tag=="VRZone"&& Networks.NetworkInitializer.Instance.cameraType == CameraType.VR)
            {
                block.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("aa");
            if (other.gameObject.tag == "VRZone" && Networks.NetworkInitializer.Instance.cameraType == CameraType.VR)
            {
                block.gameObject.SetActive(false);
            }
            if(other.gameObject.tag=="MRZone")
            {
                Destroy(this.gameObject);
            }
        }
    }
}