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
            GameObject a = Instantiate(block);
            a.transform.position = new Vector3(0, 1, -5f);
            NetworkServer.Spawn(a);
        }
    }
}