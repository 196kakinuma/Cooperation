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
            GameObject parent = new GameObject();
            parent.name = "Parent";
            foreach (Transform wall in obj.transform)
            {
                foreach (Transform o in wall)
                {
                    //TODO:移動を記録するオブジェクトとして登録する

                    GameObject a = Instantiate(o.gameObject);
                    a.transform.parent = parent.transform;
                    NetworkServer.Spawn(a);
                }
            }
        }
    }
}