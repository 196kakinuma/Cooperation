using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Block
{
    public class BlockScript : MonoBehaviour
    {
        [SerializeField]
        public BLOCKCOLOR blockColor= BLOCKCOLOR.WHITE;

        BlockPrefab prefScript;
        int blockNum;

        // Use this for initialization
        void Start()
        {
            if (Networks.NetworkInitializer.Instance.cameraType == CameraType.MR) Destroy(this);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SetBlockInfo(BlockPrefab scr,int num)
        {
            prefScript = scr;
            blockNum = num;
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (blockColor)
            {
                case BLOCKCOLOR.WHITE:
                    if (other.tag == "HAND" || other.tag=="FOOT" || other.tag=="BODY")
                    {
                        prefScript.SetCollitionObject(blockColor, blockNum);
                    }
                    break;
                case BLOCKCOLOR.RED:
                    if (other.tag == "HAND")
                    {
                        prefScript.SetCollitionObject(blockColor, blockNum);
                    }
                    break;
                case BLOCKCOLOR.BLUE:
                    if (other.tag == "FOOT" )
                    {
                        prefScript.SetCollitionObject(blockColor, blockNum);
                    }
                    break;
            }

        }


    }
}