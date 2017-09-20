using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
namespace Objects.Test
{
    public class TestCubeColorScript : NetworkBehaviour
    {
        [SerializeField]
        Material _material;

        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        public void ColorChange ()
        {
            if ( _material.color == Color.blue ) _material.color = Color.white;
            else if ( _material.color == Color.white ) _material.color = Color.blue;
        }
    }
}