using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Palyer
{
    public class MRPlayer : MonoBehaviour
    {
        [SerializeField]
        GameObject tracker;
        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {
            gameObject.transform.position = tracker.transform.position;
        }
    }
}