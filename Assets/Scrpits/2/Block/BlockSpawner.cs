using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace C2.Block
{

    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject block;


        int[] raw = new int[24] {0,0,0,0,
                                1,0,0,0,
                                1,0,0,2,
                                1,1,0,0,
                                1,0,0,3,
                                0,0,0,1};

        float time=0f;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if(time>5f)
            {
                Create();
                time = 0f;
            }
        }

        void Create()
        {
            var a = Instantiate(block).GetComponent<BlockPrefab>();
            a.transform.position = new Vector3(0, 1.125f, -5f);
            a.CreateBlocks(raw);
            NetworkServer.Spawn(a.gameObject);
        }
    }
}