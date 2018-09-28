using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace C2.System
{
    /// <summary>
    /// expobjをスポーンさせて、その物体の移動の記録とかの管理も担う
    ///
    /// </summary>
    public class ExpPrefSpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject exp1Pref;

        //動かす用のプレハブ用
        [SerializeField]
        GameObject[] moveObject;

        // Use this for initialization
        void Start()
        {
            SpawnExpPref(exp1Pref);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnExpPref(GameObject obj)
        {
            NetworkServer.Spawn(Instantiate(exp1Pref));
            GameObject parent = new GameObject();
            parent.name = "Parent";

        }
    }
}