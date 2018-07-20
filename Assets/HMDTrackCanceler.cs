using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMDTrackCanceler : MonoBehaviour
{

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        positionParent.transform.position = ( -1 * UnityEngine.XR.InputTracking.GetLocalPosition (UnityEngine.XR.XRNode.CenterEye) );

    }
}
