using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.System
{
    public class HandController : MonoBehaviour
    {
        [SerializeField]
        public GameMaster forward;
        void Awake ()
        {
            if ( Networks.NetworkInitializer.Instance.playerType != PlayerType.HOST )
            {
                Destroy (this);
            }
        }
        [HideInInspector]
        public GameObject targetObject;

        // Use this for initialization
        void Start ()
        {
        }

        // Update is called once per frame
        void Update ()
        {
            this.transform.position = targetObject.transform.position;
            this.transform.rotation = targetObject.transform.rotation;
        }

    }
}