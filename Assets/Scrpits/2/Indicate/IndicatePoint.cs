using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Indicate
{
    public class IndicatePoint : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetActive(bool b)
        {
            gameObject.SetActive(b);
        }

        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }
    }


}