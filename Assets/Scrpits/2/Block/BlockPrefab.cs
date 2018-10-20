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

        [SerializeField]
        bool stop = false;

        bool isinVRZone=false;
        public void ReverseStopFlag()
        {
            if (isinVRZone) return;
            stop = !stop;
            //if (stop) stop = false;
            //else stop = true;
        }


        void Start()
        {
            colBlklist = new List<int>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!stop)
            {
                Move();
            }
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
                isinVRZone = true;
                SetBlocksActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "VRZone" /*&& Networks.NetworkInitializer.Instance.cameraType == CameraType.VR*/)
            {
                BeforeDestory();
                isinVRZone = false;
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


        bool collred = false;
        bool collblue = false;
        List<int> colBlklist;
        /// <summary>
        /// 衝突してしまったブロックの情報を受け取る
        /// </summary>
        /// <param name="bcolor"></param>
        /// <param name="blocknum"></param>
        public void SetCollitionObject(BLOCKCOLOR bcolor,int blocknum)
        {
            //Debug.Log("color;"+bcolor+"num:"+blocknum);
             switch (bcolor)
            {
                case BLOCKCOLOR.WHITE:
                    colBlklist.Add(blocknum);
                    break;
                case BLOCKCOLOR.RED:
                    collred = true;
                    break;
                case BLOCKCOLOR.BLUE:
                    collblue = true;
                    break;
            }
        }


        /// <summary>
        /// 削除される前にぶつかった情報を格納
        /// </summary>
        void BeforeDestory()
        {
            string str = "";
            foreach(var a in colBlklist)
            {
                str += a.ToString();
                str += "-";
            }

            if (collblue && collred)
                C2.Exp.ExpRecorder.Instance.AddTaskCollisionInfo("TRUE", str);
            else if(collred)
                C2.Exp.ExpRecorder.Instance.AddTaskCollisionInfo("RED", str);
            else if(collblue)
                C2.Exp.ExpRecorder.Instance.AddTaskCollisionInfo("BLUE", str);
            else
                C2.Exp.ExpRecorder.Instance.AddTaskCollisionInfo("FALSE", str);
        }
    }
}