using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.System
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField]
        GameStory story;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerExit(Collider other)
        {
            if(other.tag=="HAND")
            {
                story.StartExp();
                gameObject.SetActive(false);
            }
        }
    }
}