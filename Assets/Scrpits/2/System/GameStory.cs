﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.System
{
    /// <summary>
    /// GameMaster2は何かスポーンさせるように取っておくので、名前かぶりを気にして変更した
    /// やろうとしていることは同じ
    /// </summary>
    public class GameStory : MonoBehaviour
    {
        [SerializeField]
        C2.Block.BlockSpawner spawner;

        public bool gameStart = false;
        public bool gameFinish = false;
        float time = 0f;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(gameStart)
            {
                time += Time.deltaTime;
            }

            if(spawner.currentBlock==null && gameStart)
            {
                spawner.Create(Random.Range(0, 2), Random.Range(0, 5),0);
            }
        }

        /// <summary>
        /// ボタンから呼ばれる
        /// </summary>
        public void StartExp()
        {
            Debug.Log("Start!");



            gameStart = true;
        }

        public void FinishExp()
        {
            gameStart = false;
            gameFinish = true;
            Debug.Log("finish time:"+time);

        }
    }
}