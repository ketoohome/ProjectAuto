using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameLogic{
	/// <summary>
	/// 游戏中逻辑个体，可在逻辑个体管理器中获取，可以被点击
	/// </summary>
	public abstract class L_Actor : MonoBehaviour,IAttributeContainer {


		/// <summary>
		/// Gets the ID
		/// </summary>
		/// <value>The I.</value>
		public uint ID{ get{ return m_ID;}}
		uint m_ID = NoID;

		/// <summary>
		/// 没有初始化的ID数
		/// </summary>
		/// <value>The no I.</value>
		public static uint NoID{get{ return uint.MaxValue;}}

		/// <summary>
		/// The attribute root.
		/// </summary>
		L_Attribute m_AttributeRoot = new L_Attribute();
		protected L_Attribute Attributes{
			get{ return m_AttributeRoot;}
			set{ m_AttributeRoot = value;}
		}

		void Awake(){ 
			m_AttributeRoot.Key = "AttributeRoot";
			m_AttributeRoot.Value = "AttributeRoot";

			m_ID = L_ActorManager.It.Add(this);

			gameObject.name = "__Actor" + "_" + m_ID;

			Birth();
		}

		void OnDestroy(){
			Dead();
		
			L_ActorManager.It.Remove(m_ID);
		}

		/// <summary>
		/// 角色创建时调用
		/// </summary>
		protected virtual void Birth(){}

		/// <summary>
		/// 角色销毁时调用
		/// </summary>
		protected virtual void Dead(){} 

		/// <summary>
		/// 更新时调用
		/// </summary>
		public abstract void CustomUpdate();

		/// <summary>
		/// 获得属性
		/// </summary>
		/// <returns>The attribure.</returns>
		/// <param name="attName">Att name.</param>
		public L_Attribute GetAttribute(string name){
			return m_AttributeRoot.FindChild(name);
		}
	}

	/// <summary>
	/// 角色管理器
	/// </summary>
	public class L_ActorManager : Singleton<L_ActorManager>
	{
		List<L_Actor> m_Actors = new List<L_Actor>();
		/// <summary>
		/// ID计数器
		/// </summary>
		uint m_IDCounter = 0;

		GameObject m_ActorRoot = null;
		public GameObject ActorRoot{
			get{ 
				if (m_ActorRoot == null) m_ActorRoot = GameObject.FindWithTag ("Actors");
				return m_ActorRoot;
			}
		}

		/// <summary>
		/// 添加角色
		/// </summary>
		/// <param name="actor">Actor.</param>
		public uint Add(L_Actor actor){
			m_IDCounter++;
			m_Actors.Add(actor);
			return m_IDCounter;
		}
		
		/// <summary>
		/// 移除角色
		/// </summary>
		/// <param name="id">角色ID.</param>
		public void Remove(uint id){
			L_Actor actor = Find (id);
			if(actor != null) m_Actors.Remove(actor);
		}

        /// <summary>
        /// 清除所有角色
        /// </summary>
        public void Clear() {
            for (int i = 0; i < m_Actors.Count; i++) {
                L_Actor actor = m_Actors[i];
                GameObject.Destroy(actor);
            }
        }

		/// <summary>
		/// 查找角色
		/// </summary>
		/// <param name="id">角色ID</param>
		public L_Actor Find(uint id){
			L_Actor actor = null;
			for(int i = 0;i<m_Actors.Count;i++){
				actor = m_Actors[i];
				if(actor.ID == id) break;
			}
			return actor;
		}
		public T Find<T>(uint id) where T : L_Actor{
			return (T)Find(id);
		}
		/// <summary>
		/// Get the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public L_Actor Get(int index){
			return m_Actors [index];
		}
		/// <summary>
		/// Gets the lenght.
		/// </summary>
		/// <value>The lenght.</value>
		public int Lenght{get{ return m_Actors.Count;}}

		/// <summary>
		/// 更新角色
		/// </summary>
		public void CustomUpdate(){
			for(int i = 0;i<m_Actors.Count;i++){
				m_Actors[i].CustomUpdate();
			}
		}
	}
}