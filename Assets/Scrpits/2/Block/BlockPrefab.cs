using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace C2.Block
{
    public enum BLOCKCOLOR {WHITE,RED,BLUE }


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

        [SerializeField]
        List<GameObject> notspwanedObj=new List<GameObject>();



        void Start()
        {
            //if (Networks.NetworkInitializer.Instance.cameraType == CameraType.VR)
            //{
            //    blocks.SetActive(false);
            //}
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
                GameObject tmp;
                switch (ints[i]) {
                    case 0:
                        break;
                    case 1:
                        tmp =Instantiate(white);
                        tmp.transform.SetParent(objs[i].transform);
                        tmp.transform.localPosition = Vector3.zero;
                        tmp.GetComponent<BlockScript>().SetBlockInfo(this, i);
                        notspwanedObj.Add( tmp);

                        break;
                    case 2:
                        tmp =Instantiate( red);
                        tmp.transform.SetParent(objs[i].transform);
                        tmp.transform.localPosition = Vector3.zero;
                        tmp.GetComponent<BlockScript>().SetBlockInfo(this, i);
                        notspwanedObj.Add( tmp);
                        break;
                    case 3:
                        tmp =Instantiate(blue);
                        tmp.transform.SetParent(objs[i].transform);
                        tmp.transform.localPosition = Vector3.zero;
                        tmp.GetComponent<BlockScript>().SetBlockInfo(this, i);
                        notspwanedObj.Add(tmp);
                        break;
                    default:
                        tmp = new GameObject();
                        break;
                }



            }
        }

        public void SpwanBlock()
        {
            foreach (var a in notspwanedObj)
            {
                NetworkServer.Spawn(a);
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
                //blocks.gameObject.SetActive(true);
                SetBlocksActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "VRZone" /*&& Networks.NetworkInitializer.Instance.cameraType == CameraType.VR*/)
            {
                //blocks.gameObject.SetActive(false);
                SetBlocksActive(false);
                Destroy(this.gameObject);
            }
            if (other.gameObject.tag == "MRZone")
            {
                Destroy(this.gameObject);
            }
        }

        public void SetBlocksActive(bool b)
        {
            foreach(var a in notspwanedObj)
            {
                a.GetComponent<BlockScript>().SetBlockActive(b);
            }
        }

        /// <summary>
        /// 衝突してしまったブロックの情報を受け取る
        /// </summary>
        /// <param name="bcolor"></param>
        /// <param name="blocknum"></param>
        public void SetCollitionObject(BLOCKCOLOR bcolor,int blocknum)
        {
            Debug.Log("color;"+bcolor+"num:"+blocknum);
        }
    }
}