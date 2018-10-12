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
        GameObject currentBlock=null;

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
            if (currentBlock == null)
            {
                currentBlock = Create(Random.Range(0, 2), Random.Range(0, 5));
            }
            
        }

        GameObject Create(int pos,int offset)
        {
            var a = Instantiate(blockPref).GetComponent<BlockPrefab>();
            NetworkServer.Spawn(a.gameObject);

            a.CreateBlocks(raw);

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


            
            return a.gameObject;
        }
    }
}