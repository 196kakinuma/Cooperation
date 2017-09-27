using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Games.GameSystem;
namespace Objects
{

    public interface IKeyLockGameMaster
    {

        IEnumerator Initialize ( Door d );
        /// <summary>
        /// そのゲーム毎に紐づく物をnullにする
        /// </summary>
        void Clear ();
    }
}