using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networks;
using UnityEngine.Networking;

namespace Main
{
    public class MainSceneinitializer : MonoBehaviour
    {
        [SerializeField]
        GameObject gameMasterPref;

        [SerializeField]
        GameObject trackerMasterPref;

        [SerializeField]
        GameObject earthPref;

        [SerializeField]
        GameObject engel;


        // Use this for initialization
        void Start ()
        {
            if ( NetworkInitializer.Instance.cameraType == CameraType.VR )
            {
                //GameMaster生成
                Instantiate (gameMasterPref);

                //NetworkServer.Spawn (Instantiate (room));
                NetworkServer.Spawn (Instantiate (earthPref));
                NetworkServer.Spawn (Instantiate (engel));
            }
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}