using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace C2.Indicate
{
    public class NetIndicatePoint : NetworkBehaviour
    {
        [SerializeField]
        IndicatePoint p;

        private void Awake()
        {

        }
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
            p.SetActive(b);
        }

        [Command]
        public void CmdSetPosition(Vector3 position)
        {
            RpcSetPosition(position);
        }
        [ClientRpc]
        private void RpcSetPosition(Vector3 position)
        {
            p.Setposition(position);
        }
    }


}