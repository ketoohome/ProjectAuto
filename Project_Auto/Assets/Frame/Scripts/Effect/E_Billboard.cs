using UnityEngine;
using System.Collections;
using GameCommon;

namespace GameEffect
{
	public class E_Billboard : MonoBehaviour {

		void Update () {
            if(Camera.main != null)
                transform.forward = -Camera.main.transform.forward;
		}
	}
}