﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
namespace Games.DCG
{
    public class DCGKnob : MonoBehaviour, IVRObject
    {
        [SerializeField, Range (0, 4)]
        int knobNum = 0;
        [SerializeField]
        DCGMaster master;

        Color[] colors;
        [SerializeField]
        Material material;

        float firstLocalHeight;

        float[] heightThreshold;
        // Use this for initialization
        void Start ()
        {

        }

        // Update is called once per frame
        void Update ()
        {

        }

        public void Initialize ( Color[] colors )
        {
            this.colors = colors;
            heightThreshold = new float[colors.Length];
            firstLocalHeight = transform.localPosition.y;
            float t = firstLocalHeight / colors.Length;
            for ( int i = 1; i <= colors.Length; i++ )
            {
                heightThreshold[i - 1] = t * i;
            }
            var y = Random.Range (0, firstLocalHeight);
            SetCurrentState (y);
        }

        /// <summary>
        /// ローカル座標のyを使用する
        /// </summary>
        /// <param name="y"></param>
        void SetCurrentState ( float y )
        {
            Debug.Log ("set current");
            master.SetKnobState (knobNum, y, GetColor (y));
        }

        public void NtSetCurrentState ( float y, Color c )
        {
            Debug.Log ("set current" + y);
            transform.localPosition = new Vector3 (transform.localPosition.x, y, transform.localPosition.z);
            material.color = c;
        }

        public Color GetCurrentColor ()
        {
            return material.color;
        }

        Color GetColor ( float y )
        {
            for ( int i = 0; i < heightThreshold.Length; i++ )
            {

                if ( y <= heightThreshold[i] )
                {
                    return colors[i];
                }
            }
            return colors[colors.Length - 1];
        }

        public void ClickReceive ()
        {

        }

        public void HoldReceive ( Vector3 pos )
        {
            var y = transform.parent.InverseTransformPoint (pos).y;
            if ( y < 0 || firstLocalHeight < y ) return;
            SetCurrentState (y);
        }
    }
}