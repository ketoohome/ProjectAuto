using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TOOL;

namespace GameLogic
{
    /// <summary>
    /// 规则接口
    /// </summary>
    public interface IJudge {
        bool EndTrigger();
    }

    /// <summary>
    /// 规则创建者
    /// </summary>
    public class JudgeCreater{
        public static IJudge CreateJudge<T>() where T : MonoBehaviour, IJudge
        {
            GameObject obj = new GameObject("__Judge");
            return obj.AddComponent<T>();
        } 
    }

    ///// <summary>
    ///// 游戏规则类型
    ///// </summary>
    //public abstract class L_Judge : U3DSingleton<L_Judge> {
        
    //    /// <summary>
    //    /// 游戏结束条件
    //    /// </summary>
    //    public abstract bool EndTrigger();

    //    /// <summary>
    //    /// 创建
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    public static L_Judge CreateJudge<T>() where T : L_Judge
    //    {
    //        GameObject obj = new GameObject("__Judge");
    //        return obj.AddComponent<T>();
    //    }
    //}
}