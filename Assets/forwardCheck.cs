using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Test
{
    public class forwardCheck : MonoBehaviour
    {
        [SerializeField]
        GameObject Cube;
        [SerializeField]
        bool checkForward;
        public GameObject forward;
        // Use this for initialization
        void Start ()
        {

            if ( checkForward ) Instantiate (Cube, this.transform.forward, Quaternion.identity, this.transform);
        }
        // Update is called once per frame
        void Update ()
        {

        }
    }
}