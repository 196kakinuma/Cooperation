using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace C2.Block
{

    public class BlockSpawner : MonoBehaviour
    {

        [SerializeField]
        GameObject blockPref;
        [SerializeField]
        taskNumSheet sheet;

        public  GameObject currentBlock=null;

        int[] raw = new int[24] {0,0,0,0,
                                1,0,0,0,
                                1,0,0,2,
                                1,1,0,0,
                                1,0,0,3,
                                0,0,0,1};

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Create(int pos,int offset,int rawNum,int reverse)
        {
            var a = Instantiate(blockPref).GetComponent<BlockPrefab>();


            a.CreateBlocks(GetTask(rawNum,reverse));

            if (pos==0)
            {
                float tmp = -0.5f + 0.25f * offset;
                a.transform.position = new Vector3(tmp, 1.125f, -5f);
            }
            else if(pos==1){
                float tmp = -0.5f + 0.25f * offset;
                a.transform.position = new Vector3(-5f, 1.125f, tmp);
                a.transform.Rotate(new Vector3(0, 90, 0));
            }
            NetworkServer.Spawn(a.gameObject);
            a.SpwanBlock();
            a.SetBlocksActive(false);

            currentBlock= a.gameObject;
        }


        int[] GetTask(int i,int reverse)
        {
            int[] task = new int[24];

            if (reverse == 0)
            {
                for (int j = 0; j < 6; j++)
                {
                    task[j * 4 + 0] = sheet.sheets[i].list[j].col1;
                    task[j * 4 + 1] = sheet.sheets[i].list[j].col2;
                    task[j * 4 + 2] = sheet.sheets[i].list[j].col3;
                    task[j * 4 + 3] = sheet.sheets[i].list[j].col4;
                }
            }
            else //リバースする            
            {
                for (int j = 0; j < 6; j++)
                {
                    task[j * 4 + 0] = sheet.sheets[i].list[j].col4;
                    task[j * 4 + 1] = sheet.sheets[i].list[j].col3;
                    task[j * 4 + 2] = sheet.sheets[i].list[j].col2;
                    task[j * 4 + 3] = sheet.sheets[i].list[j].col1;
                }
            }
            return task;
        }
    }
}