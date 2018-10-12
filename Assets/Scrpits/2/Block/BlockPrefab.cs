using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Block
{

    public class BlockPrefab : MonoBehaviour
    {

        float speed = 0.5f;

        [SerializeField]
        GameObject[] objs;

        [SerializeField]
        GameObject white;
        [SerializeField]
        GameObject blue;
        [SerializeField]
        GameObject red;


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

        public void CreateBlocks(int[] ints)
        {
            for(int i = 0; i < ints.Length; i++)
            {

                GameObject temp;
                switch (ints[i]) {
                    case 0:
                        break;
                    case 1:
                        temp =Instantiate(white);
                        temp.transform.SetParent(objs[i].transform);
                        temp.transform.localPosition=Vector3.zero;
                        break;
                    case 2:
                        temp =Instantiate( red);
                        temp.transform.SetParent(objs[i].transform);
                        temp.transform.localPosition = Vector3.zero;
                        break;
                    case 3:
                        temp =Instantiate(blue);
                        temp.transform.SetParent(objs[i].transform);
                        temp.transform.localPosition = Vector3.zero;
                        break;
                    default:
                        temp = new GameObject();
                        break;
                }
                

                
            }
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