/*******************************************************************************************************************
 * 作者：杜凯
 * 时间：2016.10.17
 * *****************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TOOL;

namespace GameLogic
{
    /// <summary>
    /// 数据池，数据根节点
    /// </summary>
    public class L_DataPool : U3DSingleton<L_DataPool>
    {
        L_Data rootData = null;
        
		void Awake() {
            rootData = new L_Data();
            rootData.Value = "DataRoot";
            rootData.Key = "DataRoot";
        }

        public List<L_Data> Children { get { return rootData.Children; } }
        public string Key { get { return rootData.Key; } }
		public L_Data FindChild(params string[] keys){
			return rootData.FindChild(keys);
        }
        public L_Data CreatChildData(string key, object value) { return rootData.CreatChildData(key, value); }
    }

    /// <summary>
    /// 游戏用数据类型
    /// </summary>
    public class L_Data : BaseData<L_Data> {}
}