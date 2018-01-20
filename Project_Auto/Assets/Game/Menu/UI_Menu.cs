using UnityEngine;
using System.Collections;
using GameCommon;

namespace GameUI{

	public class UI_Menu : IUIBase {
		public void OnSinglePlayer(){
			EventMachine.SendEvent(CommonEventID.Event_UI_Menu_Play);
		}

        public void OnTwoPlayer()
        {
            EventMachine.SendEvent(CommonEventID.Event_UI_Menu_TwoPlay);
        }

        public void OnMultiplayer() {
            EventMachine.SendEvent(CommonEventID.Event_UI_Menu_Multiplayer);
        }
	}
}