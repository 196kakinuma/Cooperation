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

        // Use this for initialization
        void Start()
        {
            GameObject a = Instantiate(block);
            a.transform.position = new Vector3(0,1,-4f);
            NetworkServer.Spawn(a);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}