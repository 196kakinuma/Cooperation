using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;

namespace C2.System
{

    //VR側でしか呼ばれない
    public class GameMaster : SingletonMonoBehaviour<GameMaster>
    {

        bool isPlaying = false;
        public bool IsPlaying
        {
            get { return isPlaying; }
            private set { isPlaying = value; }
        }
        [SerializeField]
        GameObject[] expPref1Spawner;

        // Use this for initialization
        void Start ()
        {
            Instantiate(expPref1Spawner[(int)ExpSettings.Instance.expnum]);
        }

        // Update is called once per frame
        void Update ()
        {

        }

       
    }
}