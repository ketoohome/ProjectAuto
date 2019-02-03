using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TOOL
{

    /// <summary>
    /// 状态机状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IState<T> where T : class
    {
        public T Root;
        public IState() { }

        //进入状态  
        public virtual void Enter(T root) { }

        //状态正常执行
        public virtual void Execute(T root) { }

        //退出状态
        public virtual void Exit(T root) { }
    }

    /// <summary>
    /// 状态机
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IStateMachine<T,E> where T : class where E : Enum
    {

        private IState<T> currentState;
        private List<IState<T>> globalStates;
        private Dictionary<E, IState<T>> states;
        private T root;

        public IStateMachine(T _root)
        {
            root = _root;
            currentState = null;
            globalStates = new List<IState<T>>();
            states = new Dictionary<E, IState<T>>();
        }

        public void Add(E key, IState<T> node)
        {
            if (!states.ContainsKey(key))
            {
                states.Add(key, node);
            }
        }

        public IState<T> Get(E key)
        {
            if (states.ContainsKey(key))
            {
                return states[key];
            }
            else
            {
                return null;
            }
        }

        public void SetGlobalState(E key)
        {
            IState<T> state = Get(key);
            if (!globalStates.Contains(state))
            {
                state.Enter(root);
                globalStates.Add(state);
            }
        }

        public void SetCurrentState(E key)
        {
            IState<T> state = Get(key);
            currentState = state;
            currentState.Enter(root);
        }

        public void Update()
        {

            //全局状态的运行
            foreach (IState<T> state in globalStates)
            {
                state.Execute(root);
            }

            //一般当前状态的运行
            if (currentState != null)
                currentState.Execute(root);
        }

        public void ChangeState(E key)
        {
            IState<T> state = Get(key);
            if (state == null)
            {
                Debug.LogError("该状态不存在: " + key);
                return;
            }

            if (currentState == state)
            {
                Debug.LogError("该状态已存在: " + key);
                return;
            }

            //退出之前状态
            if (currentState != null)
                currentState.Exit(root);

            //设置当前状态
            currentState = state;

            //进入当前状态
            if (currentState != null)
                currentState.Enter(root);
        }

        public IState<T> CurrentState()
        {
            //返回目前状态 
            return currentState;
        }
        public List<IState<T>> GlobalStates()
        {
            //返回全局状态 
            return globalStates;
        }

        //
        public void RemoveGlobalState(IState<T> state)
        {
            if (globalStates.Contains(state))
            {
                state.Exit(root);
                globalStates.Remove(state);
            }
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    /// 快速状态机状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IQState<T> where T : class
    {
        public T Root = null;
        public IQState() {}

        //进入状态  
        public virtual void Enter(){}

        //状态正常执行
        public virtual void Execute(){}

        //退出状态
        public virtual void Exit(){}
    }


    /// <summary>
    /// 快速状态机：状态和状态枚举必须为私有类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IQStateMachine<T,E> where T : class where E : System.Enum
    {

        private IQState<T> currentState;
		private List<IQState<T>> globalStates;
		private Dictionary<E, IQState<T>> states;
        private T root;

        public IQStateMachine(T _root)
        {
            root = _root;
            currentState = null;
			globalStates = new List<IQState<T>>();
			states = new Dictionary<E, IQState<T>>();

            int num = 0;
            foreach (E e in System.Enum.GetValues(typeof(E))) {
                string classname = typeof(T).ToString()+"+State" + e.ToString();
                Type t = Type.GetType(classname);
                if (t == null) Debug.LogError("状态[" + e.ToString() + "]没有找到匹配的状态类[" + classname + "]!");
                else {
                    IQState<T> state = (IQState<T>)Activator.CreateInstance(t);
                    state.Root = root;
                    states.Add(e, state);

                    if (num == 0) SetCurrentState(e);
                    num++;
                }
            }
        }

		public void Add(E key, IQState<T> node)
        {
            if (!states.ContainsKey(key))
            {
                states.Add(key, node);
            }
        }

		public IQState<T> Get(E key)
        {
            if (states.ContainsKey(key))
            {
                return states[key];
            }
            else
            {
                return null;
            }
        }

        public void SetGlobalState(E key)
        {
            IQState<T> state = Get(key);
            if (!globalStates.Contains(state))
            {
                state.Enter();
                globalStates.Add(state);
            }
        }

        public void SetCurrentState(E key)
        {
            IQState<T> state = Get(key);
            currentState = state;
            currentState.Enter();
        }

        public void Update()
        {
            
            //全局状态的运行
			foreach (IQState<T> state in globalStates)
            {
                state.Execute();
            }

            //一般当前状态的运行
            if (currentState != null)
                currentState.Execute();
        }

        public void ChangeState(E key)
        {
            IQState<T> state = Get(key);
            if (state == null)
            {
                Debug.LogError("该状态不存在: " + key);
                return;
            }

            if (currentState == state)
            {
                Debug.LogError("该状态已存在: " + key);
                return;
            }

            //退出之前状态
            if (currentState != null)
                currentState.Exit();

            //设置当前状态
            currentState = state;

            //进入当前状态
            if (currentState != null)
                currentState.Enter();
        }

		public IQState<T> CurrentState()
        {
            //返回目前状态 
            return currentState;
        }
		public List<IQState<T>> GlobalStates()
        {
            //返回全局状态 
            return globalStates;
        }

        //
		public void RemoveGlobalState(IQState<T> state)
        {
            if (globalStates.Contains(state))
            {
                state.Exit();
                globalStates.Remove(state);
            }
        }
    }
}

