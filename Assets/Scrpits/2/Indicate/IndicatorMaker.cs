using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Indicate
{
    public class IndicatorMaker : MonoBehaviour
    {
        [SerializeField]
        Indicator controller;
        [SerializeField]
        GameObject IndiManagePref;
        
        IndicatorManager manager;

        // Use this for initialization
        void Start()
        {
            manager = Instantiate(IndiManagePref).GetComponent<IndicatorManager>();
            manager.indicator = controller;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}