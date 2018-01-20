using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameCommon;
using TOOL;

namespace GameUI{

	public class UI_Loading : IUIBase {

        string label = "LOADING";
		public float Progress = 0;
		public int order;

		void Awake(){
			EventMachine.Register (CommonEventID.Event_Loading,OnLoadingProgress);
		}

		void OnDestroy(){
			EventMachine.Unregister (CommonEventID.Event_Loading,OnLoadingProgress);
		}

        void Start() {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = gameObject.AddComponent<Canvas>();
            }
            canvas.overrideSorting = true;
            canvas.sortingOrder = order;
            
			StartCoroutine (UpdateText());
        }

		void Update(){
			// 更新进度条,如果进度为1则自动关闭
			if(Progress >= 1.0f) ClockMachine.It.CreateClock(2.0f,()=>{Destroy(gameObject);}); 
			
		}

		IEnumerator UpdateText(){
			while (true) {
				label += ".";
				if (label == "LOADING.......") label = "LOADING";
				transform.Find("Text").GetComponent<Text>().text = label;
				yield return new WaitForSeconds (0.25f);
			}
		} 

		void OnLoadingProgress(params object[] args){
			if (args.Length == 0) return;
			Progress = (float)args [0];
		}
    }
}