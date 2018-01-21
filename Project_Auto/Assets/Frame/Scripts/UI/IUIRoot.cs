using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameUI
{
    /// <summary>
    /// UI 基类
    /// </summary>
	public abstract class IUIRoot : U3DSingleton<IUIRoot>
    {
        void Awake()
        {
            // 如果已经初始化了，则不执行一下内容
            if (!IsSingle) { Destroy(gameObject); return; }
            // 主逻辑所挂接的gameObject不能在切换场景的时候删除。
            DontDestroyOnLoad(gameObject);
            // 注册UI
            RegisterUIs();

            // 注册事件
            EventMachine.Register(EventID.Event_UI_Create, OnCreateUI);
            EventMachine.Register(EventID.Event_UI_Delete, OnDeleteUI);
            EventMachine.Register(EventID.Event_UI_DeleteAll, OnDeleteAllUI);
        }

        void OnDestroy()
        {
            // 注销事件
            EventMachine.Unregister(EventID.Event_UI_Create, OnCreateUI);
            EventMachine.Unregister(EventID.Event_UI_Delete, OnDeleteUI);
            EventMachine.Unregister(EventID.Event_UI_DeleteAll, OnDeleteAllUI);
        }

        /// <summary>
        /// Raises the create U event.
        /// </summary>
        /// <param name="args">Arguments.</param>
        void OnCreateUI(params object[] args)
        {
            CreateUI((UIType)args[0]);
        }

        void OnDeleteUI(params object[] args)
        {
            DeleteUI((UIType)args[0]);
        }

        void OnDeleteAllUI(params object[] args)
        {
            DeleteAllUI();
        }

        /// <summary>
        /// The _dict.
        /// </summary>
		protected static readonly Dictionary<int, System.Type> _dict = new Dictionary<int, System.Type>();

        /// <summary>
        /// UI 预制体路径
        /// </summary>
        public static readonly string UIPrefabPath = "UI/";
        
        /// <summary>
        /// Creates the U.
        /// </summary>
        /// <param name="type">Type.</param>
		protected void CreateUI(UIType id)
        {
            System.Type type = null;
            if (_dict.TryGetValue((int)id, out type))
            {
                // 1. 得到相应Prefab的路径
                string path = UIPrefabPath + type.Name;
                // 2. 实例化Prefab并放入UI_Root下
                GameObject prefab = Resources.Load<GameObject>(path);
                if (prefab == null) Debuger.LogError("找不到UI预制体： " + path);

                GameObject obj = GameObject.Instantiate<GameObject>(prefab);
                obj.transform.SetParent(transform);
                obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
                obj.GetComponent<RectTransform>().sizeDelta = Vector2.one;
                obj.transform.localScale = Vector3.one;
            }
        }

        /// <summary>
        /// Deletes the U.
        /// </summary>
		protected void DeleteUI(UIType id)
        {
            System.Type type = null;
            if (_dict.TryGetValue((int)id, out type))
            {
                // 1. 查找实例
                IUIBase obj = gameObject.GetComponentInChildren(type) as IUIBase;
                // 2. 移除实例
                if (obj != null) GameObject.Destroy(obj.gameObject);
            }
        }

        /// <summary>
        /// Deletes all U.
        /// </summary>
		protected void DeleteAllUI()
        {
            Component[] uis = gameObject.GetComponentsInChildren<IUIBase>();
            for (int i = uis.Length - 1; i >= 0; i--)
            {
                GameObject.Destroy(uis[i].gameObject);
            }
        }

        /// <summary>
        /// Registers the U.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
		/*
        public void RegisterUI<T>(UIType id) where T : IUIBase
        {
            var type = typeof(T);
            _dict.Add((int)id, type);
        }
		*/
		protected void RegisterUI(UIType id)
		{
			string classname = id.ToString ().Insert (2, "_");
			System.Type type = System.Type.GetType ("GameUI." + classname);
			_dict.Add((int)id, type);
		}

        /// <summary>
        /// User interfaces the register.
        /// </summary>
		protected void RegisterUIs(){
			// 遍历所有UI预制体，并注册
			foreach (int node in System.Enum.GetValues(typeof(UIType))) {
				UIType type = (UIType)node;
				System.Type c = System.Type.GetType (type.ToString().Insert(2,"_"));
				RegisterUI(type);
			}
		}
    }
}

