using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;
using TOOL;

namespace GameLogic{
    /// <summary>
    /// 玩家可操纵型角色
    /// </summary>
	public class L_Character : L_Actor{
        /// <summary>
        /// 角色预制体的存放路径
        /// </summary>
        public static readonly string CharacterPath = "Actor/Characters/";
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="name">角色名</param>
        /// <returns></returns>
        public static L_Character CreateCharacter(string name)
        {
            GameObject prefab = Resources.Load<GameObject>(CharacterPath + name + "/" + name);
            return GameObject.Instantiate<GameObject>(prefab).GetComponent<L_Character>();
        }

        public override void CustomUpdate(){}
	}
}