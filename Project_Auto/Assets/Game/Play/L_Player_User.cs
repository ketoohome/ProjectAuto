using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameLogic{
    /// <summary>
    /// 用户
    /// </summary>
	public class L_Player_User : L_Player {

		protected CharacterController m_Controller = null;

        /// <summary>
        /// 初始化
        /// </summary>
		protected override void Birth (){}

        /// <summary>
        /// 销毁
        /// </summary>
		protected override void Dead(){
            if (m_Controller == null) return;
            Destroy(m_Controller.gameObject);
            m_Controller = null;
        }

		/// <summary>
		/// 更新，控制
		/// </summary>
		public override void CustomUpdate(){
            if (m_Controller == null) CreateCharacter();

            // 控制角色
			Vector3 move = Vector3.down * 5;

			if (m_ID == 1) {

				if (Input.GetKey (KeyCode.W))
					move += Vector3.forward;
				if (Input.GetKey (KeyCode.S))
					move -= Vector3.forward;
				if (Input.GetKey (KeyCode.A))
					move += Vector3.left;
				if (Input.GetKey (KeyCode.D))
					move -= Vector3.left;
                /*
                move -= Vector3.left * Input.GetAxis("Joy1_LS-x");
                move -= Vector3.forward * Input.GetAxis("Joy1_LS-y");

                if (Input.GetButton("Joy1_A")) move -= Vector3.forward;
                if (Input.GetButton("Joy1_B")) move += Vector3.forward;
                */
            }

			else {
				if (Input.GetKey (KeyCode.UpArrow))
					move += Vector3.forward;
				if (Input.GetKey (KeyCode.DownArrow))
					move -= Vector3.forward;
				if (Input.GetKey (KeyCode.LeftArrow))
					move += Vector3.left;
				if (Input.GetKey (KeyCode.RightArrow))
					move -= Vector3.left;
                 /*
                move -= Vector3.left * Input.GetAxis("Joy2_LS-x");
                move += Vector3.forward * Input.GetAxis("Joy2_LS-y");

                if (Input.GetButton("Joy2_A")) move -= Vector3.forward;
                if (Input.GetButton("Joy2_B")) move += Vector3.forward;
                 */
			}
			m_Controller.Move (move.normalized * Time.deltaTime * 20);

            if (Input.GetMouseButtonDown(0)) {
                EventMachine.SendEvent(EventID.Event_Effect_CameraVibration,0.3f,Vector3.forward,2);
            }
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        protected virtual void CreateCharacter(){
            // 创建角色
            m_Controller = L_Character.CreateCharacter("Character").GetComponent<CharacterController>();
        }
	}
}