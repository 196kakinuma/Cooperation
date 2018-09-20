using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace C2.Indicate
{
    public class IndicatePoint : NetworkBehaviour
    {

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
            gameObject.SetActive(b);
        }

        [Command]
        public void CmdSetPosition(Vector3 position)
        {
            RpcSetposition(position);
        }
        [ClientRpc]
        private void RpcSetposition(Vector3 position)
        {
            this.transform.position = position;
        }
    }


}