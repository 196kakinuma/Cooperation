using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace Device
{
    public class PlayerHandMaker : NetworkBehaviour
    {
        [SerializeField]
        GameObject rightHand;
        [SerializeField]
        GameObject leftHand;

        // Use this for initialization
        [ClientCallback]
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType == CameraType.VR )
            {
                CmdCreateHands (this.gameObject);
            }


        }

        [Command]
        private void CmdCreateHands ( GameObject obj )
        {
            Debug.Log ("create hand");
            NetworkServer.SpawnWithClientAuthority (Instantiate (rightHand), obj);
            NetworkServer.SpawnWithClientAuthority (Instantiate (leftHand), obj);
        }
        // Update is called once per frame
        void Update ()
        {

        }
    }
}