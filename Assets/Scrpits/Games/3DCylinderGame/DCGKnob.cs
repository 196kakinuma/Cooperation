using System.Collections;
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
            SetColor ();
        }

        public void NtSetCurrentState ( float y )
        {
            transform.position = new Vector3 (transform.position.x, y, transform.position.z);
            SetColor ();
        }

        void SetColor ()
        {
            master.SetHintColor (knobNum, GetColor (transform.localPosition.y));
        }

        public void NtSetColor ( Color c )
        {
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
                    Debug.Log (colors[i]);
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
            Debug.Log ("local+" + y);
            if ( y < 0 || firstLocalHeight < y ) return;
            master.SetKnobState (knobNum, pos.y);
        }
    }
}