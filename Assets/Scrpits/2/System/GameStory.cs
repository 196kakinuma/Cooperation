using System.Collections;
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
        int spwnCount = 0;

        // Update is called once per frame
        void Update()
        {
            if (gameFinish) return;


            if(spawner.currentBlock==null && gameStart)
            {
                if (spwnCount == C2.System.ExpSettings.Instance.totalTasknum)
                {
                    FinishExp();
                }
                int lr = Random.Range(0, 2);
                int offset = Random.Range(0, 5);
                int taskNum=Random.Range(0,20);
                int reverse = Random.Range(0, 2);
                //spawner.Create(lr, offset,taskNum,reverse);
                Debug.Log(spwnCount);
                spawner.Create(lr, offset, spwnCount, reverse);
                C2.Exp.ExpRecorder.Instance.AddTaskInfo(lr, offset, taskNum,reverse);
                C2.Exp.ExpRecorder.Instance.AddTaskTimeInfo(time);
                spwnCount++;
            }

            if (gameStart)
            {
                time += Time.deltaTime;
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
            C2.Exp.ExpRecorder.Instance.AddTaskTimeInfo(time);
            C2.Exp.ExpRecorder.Instance.FinishExp();
            gameStart = false;
            gameFinish = true;
            Debug.Log("finish time:"+time);

        }


    }
}