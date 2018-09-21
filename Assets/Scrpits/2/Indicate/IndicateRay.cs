using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Networks;
using UnityEngine.Networking;
namespace C2.Indicate
{

    public class IndicateRay : NetworkBehaviour
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

        [Command]
        public void CmdSetActive(bool b)
        {
            RpcSetActive(b);
        }

        [ClientRpc]
        private void RpcSetActive(bool b)
        {
            gameObject.SetActive(b);
        }

        /// <summary>
        /// 始まりと終わりの点から線を描画する
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        [Command]
        public void CmdSetFromToPoint(Vector3 start ,Vector3 end)
        {
            RpcSetFromToPoint(start, end);
        }
        [ClientRpc]
        private void RpcSetFromToPoint(Vector3 start,Vector3 end)
        {
            line.SetPosition(0, start);
            line.SetPosition(1, end);
        }
    }
}