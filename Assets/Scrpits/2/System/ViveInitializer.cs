using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace C2.System
{
    /// <summary>
    /// こんとろーらのコピーをSpwanしたり
    /// </summary>
    public class ViveInitializer : MonoBehaviour
    {

        [SerializeField]
        GameObject CtrlPref;
        HandController rightHandController;
        HandController leftHandController;

        [SerializeField]
        GameObject baseRCtrler;
        [SerializeField]
        GameObject baseLCtrler;


        // Use this for initialization
        void Start ()
        {
            rightHandController = Instantiate (CtrlPref).GetComponent<HandController> ();
            leftHandController = Instantiate (CtrlPref).GetComponent<HandController> ();
            rightHandController.targetObject = baseRCtrler;
            leftHandController.targetObject = baseLCtrler;

            NetworkServer.Spawn (rightHandController.gameObject);
            NetworkServer.Spawn (leftHandController.gameObject);
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}