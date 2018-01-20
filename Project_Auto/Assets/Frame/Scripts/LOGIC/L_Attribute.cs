using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameLogic{
	/// <summary>
	/// 逻辑个体包含的属性 
	/// </summary>
	public class L_Attribute : BaseData<L_Attribute> {}

	/// <summary>
	/// 属性容器
	/// </summary>
	interface IAttributeContainer{
		/// <summary>
		/// 获得属性
		/// </summary>
		/// <returns>The attribure.</returns>
		/// <param name="name">Name.</param>
		L_Attribute GetAttribute(string name);
	}
}