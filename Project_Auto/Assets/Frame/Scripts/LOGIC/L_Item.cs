using UnityEngine;
using System.Collections;
using GameCommon;
using TOOL;

namespace GameLogic{
	/// <summary>
	/// 物件类，可被拾取
	/// </summary>
	public abstract class L_Item : L_Actor { 

		public override void CustomUpdate (){
			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				if (gameObject.GetComponent<Collider> ().Raycast (ray, out hit, 100)) Touch ();
			}
		}

		/// <summary>
		/// 触控
		/// </summary>
		protected virtual void Touch(){}
	}

}