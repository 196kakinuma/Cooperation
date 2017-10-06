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
            //TODO:ランダムに高さを変更する
            float y = Random.Range (0f, firstLocalHeight);
            master.SetKnobState (knobNum, y);
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
            Debug.Log ("y is " + y);
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
            Debug.Log ("aaaa");
            // if ( pos.y > firstLocalHeight || pos.y < 0 ) return;
            master.SetKnobState (knobNum, pos.y);
        }
    }
}