using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameCommon;

namespace GameUI{

	public class UI_Play : IUIBase {

        void Awake() {
            //transform.FindChild("Text").GetComponent<GUIText>().text = "Waiting...";
        }

        void OnDestroy() { 
        
        }

        public void OnBack() {
            EventMachine.SendEvent(EventID.Event_UI_Play_Back);
        }
	}
}