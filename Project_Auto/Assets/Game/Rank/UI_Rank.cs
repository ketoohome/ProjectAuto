using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCommon;
using TOOL;

namespace GameUI{
    public class UI_Rank : IUIBase {
        [SerializeField]
        GameObject mNode;

        void Start(){
            L_RankManager.Instance.LoadRank("TestRank");
        }

        /// <summary>
        /// 显示排行榜
        /// </summary>
        void ShowRank(){
            transform.Find("Rank").gameObject.SetActive(true);
            Transform content = transform.Find("Rank/Viewport/Content");
            for (int i = 0; i < content.childCount; i++)
            {
                Transform node = content.GetChild(i);
                if (node.name == "Node") continue;
                Destroy(node.gameObject);
            }

            List<IDataNode> datas = L_RankManager.Instance.GetRankData("TestRank").ChildrenIDataNode;

            for (int i = 0 ; i< datas.Count ;i++)
            {
                IDataNode data = datas[i];
                GameObject newNode = GameObject.Instantiate<GameObject>(mNode,mNode.transform.parent);
                newNode.name = "Node" + (i + 1);
                newNode.GetComponent<Text>().text = "No." + (i + 1) + "\t" + data.GetKey() + "\t" + data.GetValue<int>();
                newNode.SetActive(true);
            }
        }

        /// <summary>
        /// 插入元素
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        void Insert(string name, int value){
            L_RankManager.Instance.AddRankNode("TestRank",name,value);
            L_RankManager.Instance.SaveTheRank("TestRank");
            ShowRank();
        }

        /// <summary>
        /// Raises the insert event.
        /// </summary>
        public void OnInsert(){
            string name = transform.Find("Insert/InputField/Text").GetComponent<Text>().text;
            string value = transform.Find("Insert/Source").GetComponent<Text>().text;
            Insert(name,int.Parse(value));
            transform.Find("Insert").gameObject.SetActive(false);
        }

        /// <summary>
        /// Raises the close rank event.
        /// </summary>
        public void OnCloseRank(){
            Destroy(gameObject);
        }
    }
}