using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOOL;

namespace GameCommon{
    /// <summary>
    /// 排行榜管理器
    /// Example ：
    ///  L_RankManager.Instance.CreateRank<int>("TestRank");
    ///  L_RankManager.Instance.AddRankNode("TestRank", "player1", 300);
    ///  L_RankManager.Instance.AddRankNode("TestRank", "player2", 100);
    ///  L_RankManager.Instance.SaveTheRank("TestRank",false);
    /// </summary>
    public class L_RankManager : U3DSingleton<L_RankManager> {

        /// <summary>
        /// 排行榜数据类型
        /// </summary>
        class L_RankData : BaseData<L_RankData>{}

        /// <summary>
        /// 排行榜数据跟节点
        /// </summary>
        /// <value>The root data.</value>
        L_RankData RootData{
            get{
                if (mRootData == null) mRootData = new L_RankData();
                return mRootData;
            }
        } L_RankData mRootData;

        /// <summary>
        /// 文件存储路径
        /// </summary>
        protected string FilePath{  get{ return  GameCommon.Common.PathURL + "RankData/";}}
       
        /// <summary>
        /// 创建一个排行榜
        /// </summary>
        /// <param name="name">排行榜名字</param>
        /// <param name="value">排行榜的内容</param>
        /// <typeparam name="T">排行榜的类型</typeparam>
        public void CreateRank<T>(string rankname){
            if (typeof(T) != typeof(int) && typeof(T) != typeof(float)) {
                Debug.LogWarning("排行榜只支持Int 和 float 两种数据类型！");return;
            }

            if (RootData.Exist(rankname)){ Debug.LogWarning("当前排行榜已经存在！"); return; }
            L_RankData rankData = RootData.CreatChildData(rankname,typeof(T).ToString());
        }

        /// <summary>
        /// 加载排行榜（覆盖当前排行榜）
        /// </summary>
        /// <param name="rankname">Rankname.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void LoadRank(string rankname){
            // 覆盖
            L_RankData rankData = RootData.FindChild(rankname);
            if (rankData == null)
            {
                rankData = RootData.CreatChildData(rankname, "none");
            }
            else
            {
                rankData.ClearChildren();
            }
            XmlTool.LoadDataWihtPath<L_RankData>(ref rankData, FilePath + rankname);
        }

        /// <summary>
        /// 给排行榜添加一个元素
        /// </summary>
        /// <param name="rankName">Rank name.</param>
        /// <param name="name">Name.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void AddRankNode(string rankName,string name,object value){
            L_RankData rankData = RootData.FindChild(rankName);
            if (rankData == null){ Debug.LogWarning("排行榜[" + rankName + "]尚未创建！"); return; }

            if (rankData.GetValue<string>() != value.GetType().ToString()) { 
                Debug.LogWarning("排行榜[" + rankName + "]数据类型[" + rankData.Value + "]，与添加的数据[" + name + "]类型["+ value.GetType() +"]不一致！"); 
                return; 
            }

            L_RankData data = rankData.FindChild(name);
            if(data == null) data = rankData.CreatChildData(name,value);
            else data.Value = value;
        }

        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="rankname">排行榜名.</param>
        /// <param name="isOrder">If set to <c>true</c> 正序/逆序.</param>
        public void SaveTheRank(string rankname,  bool isOrder = true){
            L_RankData rankData = RootData.FindChild(rankname);
            OrderRank(rankData,isOrder); // 排序
            if(rankData != null)
                XmlTool.SaveData<L_RankData>(rankData, FilePath + rankname);
        }

        // 获得数据节点
        public IDataNode GetRankData(string rankname, bool isOrder = true){
            L_RankData rankData = RootData.FindChild(rankname);
            OrderRank(rankData,isOrder); // 排序
            return (IDataNode)rankData;
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="rootData">要排序的数据</param>
        /// <param name="isOrder">If set to <c>true</c> 正序/逆序 </param>
        void OrderRank(L_RankData rootData, bool isOrder = true){
            List<L_RankData> datas = rootData.Children;
            for (int j = 0; j < datas.Count - 1; j++)
            {
                for (int i = 0; i < datas.Count - 1; i++)
                {
                    if( isOrder ? 
                        datas[i].GetValue<int>() < datas[i+1].GetValue<int>() : 
                        datas[i].GetValue<int>() > datas[i+1].GetValue<int>())
                    {
                        object v = datas[i].Value;  
                        datas[i].Value = datas[i + 1].Value;  
                        datas[i + 1].Value = v;
                        string k = datas[i].Key;  
                        datas[i].Key = datas[i + 1].Key;  
                        datas[i + 1].Key = k;
                    }
                }
            }
        }

        /// <summary>
        /// Clears the data.
        /// </summary>
        public void ClearData(string name){
            // 清理排行榜
            LoadRank(name);
            RootData.FindChild(name).ClearChildren();
            SaveTheRank(name);
        }
    }
}