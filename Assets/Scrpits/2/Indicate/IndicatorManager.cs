using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace C2.Indicate
{
    public class IndicatorManager : MonoBehaviour
    {
        [HideInInspector]
        public Indicator indicator;

        [SerializeField]
        GameObject pointPref;
        NetIndicatePoint point;
        [SerializeField]
        GameObject RayPref;
        IndicateRay ray;
        [SerializeField]
        GameObject StartPointPref;
        NetIndicatePoint start;


        public bool DrawstartPoint = false;
        public bool Drawray = false;
        public bool DrawIndicatePoint = false;


        private bool beforeIsPress = false;


        // Use this for initialization
        void Start()
        {
            //TODO:後でSpwanすること
            point = Instantiate(pointPref).GetComponent<NetIndicatePoint>();
            NetworkServer.Spawn(point.gameObject);
           // point.CmdSetActive(false);

            ray = Instantiate(RayPref).GetComponent<IndicateRay>();
            NetworkServer.Spawn(ray.gameObject);
            //ray.CmdSetActive(false);

            start = Instantiate(StartPointPref).GetComponent<NetIndicatePoint>();
            //
            NetworkServer.Spawn(start.gameObject);
            //start.CmdSetActive(false);
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
                    if (DrawIndicatePoint) point.CmdSetActive(true);
                    if (Drawray) ray.CmdSetActive(true);
                    if (DrawstartPoint) start.CmdSetActive(true);

                    beforeIsPress = true;
                }

                

                //条件に合わせてactive表示

                if (DrawIndicatePoint)
                {
                    point.CmdSetPosition(v);
                }

                if(Drawray)
                {
                    ray.CmdSetFromToPoint(indicator.GetPosition(),v);
                }

                if(DrawstartPoint)
                {
                    start.CmdSetPosition(indicator.GetPosition());
                }


            }
            else
            {
                if (beforeIsPress)
                {
                    point.CmdSetActive(false);
                    ray.CmdSetActive(false);
                    start.CmdSetActive(false);
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
                reach= hit.point;
            }
            //Debug.DrawRay(indicator.GetPosition(), indicator.GetForward(),Color.red, maxDist);
            return reach;
        }

    }
}