using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Test
{
    public class forwardCheck : MonoBehaviour
    {
        [SerializeField]
        GameObject Cube;
        // Use this for initialization
        void Start ()
        {
            Instantiate (Cube, this.transform.forward, Quaternion.identity, this.transform);
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}