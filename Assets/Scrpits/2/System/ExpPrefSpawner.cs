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

        GameObject[] movableObjs;
        // Use this for initialization
        void Start()
        {
            SpawnExpPref(exp1Pref);
            SpawnMovableObjs();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnExpPref(GameObject obj)
        {
            NetworkServer.Spawn(Instantiate(exp1Pref));

        }

        void SpawnMovableObjs()
        {
            movableObjs = new GameObject[moveObject.Length];
            for(int i =0;i<moveObject.Length;i++)
            {
                movableObjs[i] = SpawnMovablePref(moveObject[i]);
            }
        }

        GameObject SpawnMovablePref(GameObject obj)
        {
            GameObject a = Instantiate(obj);
            NetworkServer.Spawn(a);
            return a;
        }
    }
}