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
        [SerializeField]
        GameObject TrackerPref;
        HandController rightHandController;
        HandController leftHandController;
        HandController tracker;

        [SerializeField]
        GameObject baseRCtrler;
        [SerializeField]
        GameObject baseLCtrler;
        [SerializeField]
        GameObject baseTracker;


        // Use this for initialization
        void Start ()
        {
            rightHandController = Instantiate (CtrlPref).GetComponent<HandController> ();
            leftHandController = Instantiate (CtrlPref).GetComponent<HandController> ();
            tracker = Instantiate (TrackerPref).GetComponent<HandController> ();
            rightHandController.targetObject = baseRCtrler;
            leftHandController.targetObject = baseLCtrler;
            tracker.targetObject = baseTracker;

            if (ExpSettings.Instance.Ctrler4MRDebug)
            {
                NetworkServer.Spawn(rightHandController.gameObject);
                NetworkServer.Spawn(leftHandController.gameObject);
            }
            NetworkServer.Spawn (tracker.gameObject);
        }

        // Update is called once per frame
        void Update ()
        {

        }
    }
}