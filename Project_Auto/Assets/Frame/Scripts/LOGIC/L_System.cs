/*******************************************************************************************************************
 * 作者：杜凯
 * 时间：2016.10.16
 * *****************************************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic{
	/// <summary>
	/// 系统几倍类型，系统负责游戏中所有实际功能的执行。
    ///     系统必须由SystemManager创建，删除可以通过系统类型或者直接Destroy
	/// </summary>
	public abstract class L_System{

		/// <summary>
		/// 系统开始
		/// </summary>
		public abstract void Start();
		/// <summary>
		/// 系统结束
		/// </summary>
		public abstract void End();
		/// <summary>
		/// 更新
		/// </summary>
		public abstract void CustomUpdate ();
	}
}