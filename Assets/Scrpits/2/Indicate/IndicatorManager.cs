using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace C2.Indicate
{
    public class IndicatorManager : MonoBehaviour
    {
        [SerializeField]
        Indicator indicator;

        [SerializeField]
        GameObject pointPref;
        IndicatePoint point;
        [SerializeField]
        GameObject RayPref;
        IndicateRay ray;
        [SerializeField]
        GameObject StartPointPref;
        IndicatePoint start;


        public bool DrawstartPoint = false;
        public bool Drawray = false;
        public bool DrawIndicatePoint = false;


        private bool beforeIsPress = false;


        // Use this for initialization
        void Start()
        {
            //TODO:後でSpwanすること
            point = Instantiate(pointPref).GetComponent<IndicatePoint>();
            point.SetActive(false);
            ray = Instantiate(RayPref).GetComponent<IndicateRay>();
            ray.SetActive(false);
            start = Instantiate(StartPointPref).GetComponent<IndicatePoint>();
            start.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {
            if(indicator.GetIsPress())
            {
                Vector3 v = GetRaycastPoint();
                if (v == Vector3.zero) return;

                if (!beforeIsPress)
                {
                    if (DrawIndicatePoint) point.SetActive(true);
                    if (Drawray) ray.SetActive(true);
                    if (DrawstartPoint) start.SetActive(true);

                    beforeIsPress = true;
                }

                

                //条件に合わせてactive表示

                if (DrawIndicatePoint)
                {
                    point.SetPosition(v);
                }

                if(Drawray)
                {
                    ray.SetFromToPoint(indicator.GetPosition(),v);
                }

                if(DrawstartPoint)
                {
                    start.SetPosition(indicator.GetPosition());
                }


            }
            else
            {
                if (beforeIsPress)
                {
                    point.SetActive(false);
                    ray.SetActive(false);
                    start.SetActive(false);
                    beforeIsPress = false;
                }
            }

        }

        //Raycastをして到達点を得る
        private Vector3 GetRaycastPoint()
        {
            RaycastHit hit;
            float maxDist = 1000f;
            Vector3 reach = Vector3.zero;

            if (Physics.Raycast(indicator.GetPosition(), indicator.GetForward(), out hit,maxDist))
            {
                Debug.Log(hit.point);
                reach= hit.point;
            }
            Debug.DrawRay(indicator.GetPosition(), indicator.GetForward(),Color.red, maxDist);
            return reach;
        }

    }
}