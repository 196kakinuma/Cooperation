using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Block
{

    public class BlockPrefab : MonoBehaviour
    {

        float speed = 0.5f;

        [SerializeField]
        GameObject blocks;

        void Start()
        {
            if (Networks.NetworkInitializer.Instance.cameraType == CameraType.VR)
            {
                blocks.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position = transform.position + transform.forward * speed / 60f;
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.tag == "VRZone" && Networks.NetworkInitializer.Instance.cameraType == CameraType.VR)
            {
                blocks.gameObject.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "VRZone" && Networks.NetworkInitializer.Instance.cameraType == CameraType.VR)
            {
                blocks.gameObject.SetActive(false);
            }
            if (other.gameObject.tag == "MRZone")
            {
                Destroy(this.gameObject);
            }
        }
    }
}