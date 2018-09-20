using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Indicate
{

    public class IndicateRay : MonoBehaviour
    {
        [SerializeField]
        LineRenderer line;

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
        /// <summary>
        /// 始まりと終わりの点から線を描画する
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void SetFromToPoint(Vector3 start ,Vector3 end)
        {
            line.SetPosition(0, start);
            line.SetPosition(1, end);
        }
    }
}