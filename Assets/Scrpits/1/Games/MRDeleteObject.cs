using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Games
{

    public class MRDeleteObject : MonoBehaviour
    {
        void Awake ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType == CameraType.MR )
            {
                Destroy (this.gameObject);
            }
        }

        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}