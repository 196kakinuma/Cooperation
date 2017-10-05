using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IkLibrary.Unity;
using Objects;
using Games.GameSystem;

namespace Games.DCG
{
    public enum DCGColor
    {
        BLUE,
        CYAN,
        GRAY,
        GREEN,
        RED,
        WHITE,
        YELLOW
    }
    public class DCGMaster : SingletonMonoBehaviour<DCGMaster>, IKeyLockGameMaster
    {
        [SerializeField]
        DCGNetworkTransform netTransform;
        [SerializeField]
        DCGKnob[] knobs;

        [SerializeField]
        DCGQuestion[] question;
        List<List<DCGColor>> questionList;
        [SerializeField]
        DCGAnswer answer;
        List<DCGColor> answerList;


        [SerializeField]
        bool operationAuthority = false;
        Door currentDoor;

        public DCGHint hintOj;

        int randNum = 0;

        // Use this for initialization
        void Start ()
        {
            if ( Networks.NetworkInitializer.Instance.cameraType != CameraType.VR ) return;
        }


        public void SetOperationAuthority ( bool b )
        {
            operationAuthority = b;
        }

        public void Prepare ()
        {
            ResetAll ();
            netTransform.CmdSetActive (false);

        }

        public IEnumerator Initialize ( Door d )
        {
            netTransform.CmdSetActive (true);
            //準備前でもボタンなどは前後できるため.
            ResetAll ();

            //ランダムを生成


            //問題と正解を読み込む
            InitializeQuestion ();

            InitializeAnswer ();

            InitializeHint ();

            InitializeKnob ();

            currentDoor = d;
            PrepareMove ();


            yield return true;
        }

        public void Clear ()
        {
            currentDoor = null;
            ResetAll ();
            netTransform.CmdSetActive (false);

        }

        #region INIT

        public void InitializeQuestion ()
        {
            questionList = new List<List<DCGColor>> ();
            var l = question[randNum].sheets[0].list;
            for ( int i = 0; i < question[randNum].sheets[0].list.Count; i++ )
            {
                var list = new List<DCGColor> ();
                for ( int j = 0; j < knobs.Length; j++ )
                {
                    switch ( i )
                    {
                        case 0:
                            list.Add (( DCGColor ) l[j].Knob1);
                            break;
                        case 1:
                            list.Add (( DCGColor ) l[j].Knob2);
                            break;
                        case 2:
                            list.Add (( DCGColor ) l[j].Knob3);
                            break;
                        case 3:
                            list.Add (( DCGColor ) l[j].Knob4);
                            break;
                        case 4:
                            list.Add (( DCGColor ) l[j].Knob5);
                            break;
                    }

                }
                questionList.Add (list);
            }
        }

        public void InitializeAnswer ()
        {
            answerList = new List<DCGColor> ();
            var a = answer.sheets[0].list[randNum];
            answerList.Add (( DCGColor ) a.Knob1);
            answerList.Add (( DCGColor ) a.Knob2);
            answerList.Add (( DCGColor ) a.Knob3);
            answerList.Add (( DCGColor ) a.Knob4);
            answerList.Add (( DCGColor ) a.Knob5);
        }

        public void InitializeHint ()
        {
            netTransform.CmdSetHint (GetColorFromDCGColor (randNum));
        }

        private void InitializeKnob ()
        {
            for ( int i = 0; i < knobs.Length; i++ )
            {
                knobs[i].Initialize (GetColorArray (questionList[i]));
            }
        }



        #endregion

        private Color[] GetColorArray ( List<DCGColor> color )
        {
            var c = new Color[color.Count];
            for ( int i = 0; i < c.Length; i++ )
                c[i] = GetColorFromDCGColor (( int ) color[i]);
            return c;
        }

        private Color GetColorFromDCGColor ( int i )
        {
            switch ( randNum )
            {
                case 0:
                    return Color.blue;
                case 1:
                    return Color.cyan;
                case 2:
                    return Color.gray;
                case 3:
                    return Color.green;
                case 4:
                    return Color.red;
                case 5:
                    return Color.white;
                case 6:
                    return Color.yellow;
            }
            Debug.Log ("color is overflow");
            return Color.black;
        }

        public void ResetAll ()
        {

        }

        public void Answer ()
        {
            if ( !operationAuthority ) return;

            bool correction = true;

            if ( correction )
            {
                Debug.Log ("answer is correct!!");
                currentDoor.KeyLock = true;
                ExitRoom ();
            }
            else
            {
                ResetAll ();
                Debug.Log ("missed");
            }

        }

        public void SetKnobState ( int i, float y, Color color )
        {

            if ( !operationAuthority ) return;
            netTransform.CmdKnobMove (i, y, color);
        }
        public void NtKnobMove ( int i, float y, Color color )
        {
            knobs[i].NtSetCurrentState (y, color);
        }

        #region MOVING
        [SerializeField]
        float moveDistance = 2;
        public void PrepareMove ()
        {
            netTransform.CmdPrepareMove (currentDoor.keyLockGamePosition.position, currentDoor.keyLockGamePosition.forward);
        }

        public void NtPrepareMove ( Vector3 pos, Vector3 forward )
        {
            pos = new Vector3 (pos.x, pos.y - moveDistance, pos.z);
            this.transform.position = pos;
            this.transform.forward = forward;
        }

        public void AppearRoom ()
        {
            netTransform.CmdAppearRoom ();
        }

        public void NtAppearRoom ()
        {
            Debug.Log ("appear");

            //表示する

            //アニメーション
            var pos = transform.position;
            this.transform.position = new Vector3 (pos.x, pos.y + moveDistance, pos.z);
            SetOperationAuthority (true);

        }

        public void ExitRoom ()
        {
            currentDoor.SetButtonActive (true);
            currentDoor.SetButtonWord (false);
            netTransform.CmdExitRoom ();
            //表示を隠す
            Clear ();

        }

        public void NtExitRoom ()
        {
            Debug.Log ("exit");
            SetOperationAuthority (false);

            //あにめーしょん
            var pos = transform.position;
            transform.position = new Vector3 (pos.x, pos.y - moveDistance, pos.z);

        }
        #endregion
    }
}