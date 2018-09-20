using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace C2.Indicate
{
    public class IndicatePoint : MonoBehaviour
    {
        [SerializeField]
        NetIndicatePoint net;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CallSetActive(bool b)
        {
            net.CmdSetActive(b);
        }

        public void SetActive(bool b)
        {
            gameObject.SetActive(b);
        }

        public void CallSetPosition(Vector3 position)
        {
            net.CmdSetPosition(position);
        }
        
        public void Setposition(Vector3 position)
        {
            this.transform.position = position;
        }
    }
}