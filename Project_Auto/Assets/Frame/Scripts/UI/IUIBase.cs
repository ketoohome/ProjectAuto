using UnityEngine;
using System.Collections;

namespace GameUI{

	[ExecuteInEditMode]
	/// <summary>
	/// UI基础类型
	/// </summary>
	public abstract class IUIBase : MonoBehaviour {

#if UNITY_EDITOR
		void Update(){
			if(name != this.GetType().Name) 
				name = this.GetType().Name;
		}
#endif
	}
}
