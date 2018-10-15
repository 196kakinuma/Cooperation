using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Test
{
    public class MoveForward : MonoBehaviour
    {
        float speed = 0.5f;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.position = transform.position + transform.forward*speed/60f;
        }
    }
}