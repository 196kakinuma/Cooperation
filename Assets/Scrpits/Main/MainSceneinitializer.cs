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
        GameObject trackerMasterPref;

        [SerializeField]
        GameObject room;

        [SerializeField]
        GameObject startButton;

        // Use this for initialization
        void Start ()
        {
            if ( NetworkInitializer.Instance.cameraType == CameraType.VR )
            {
                //MR用のトラッカーマスターを生成
                NetworkServer.Spawn (Instantiate (trackerMasterPref));
                NetworkServer.Spawn (Instantiate (room));
                NetworkServer.Spawn (Instantiate (startButton));
            }
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}