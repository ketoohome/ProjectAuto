using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using GameCommon;
using TOOL;

namespace GameLogic{
    /// <summary>
    /// 玩家可操纵型角色
    /// </summary>
    public class L_Character_Network : NetworkBehaviour
    {
        //
        CharacterController m_Controller = null;
        public override void OnStartLocalPlayer()
        {
            if (!isLocalPlayer) return;
            m_Controller = GetComponent<CharacterController>();
        }

        void Update(){
            if(!isLocalPlayer) return;

            Vector3 move = Vector3.down * 5;
            if (Input.GetKey(KeyCode.W))
                move += Vector3.forward;
            if (Input.GetKey(KeyCode.S))
                move -= Vector3.forward;
            if (Input.GetKey(KeyCode.A))
                move += Vector3.left;
            if (Input.GetKey(KeyCode.D))
                move -= Vector3.left;
            m_Controller.Move(move.normalized * Time.deltaTime * 20);

            if (Input.GetMouseButtonDown(0)) {
                CmdFire();
            }
        }

        [Command]
        void CmdFire(){
            GameObject prefab = Resources.Load<GameObject>("Actor/Other/Bullet");
            GameObject bullet = GameObject.Instantiate<GameObject>(prefab,transform.position,transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * 10;
            NetworkServer.Spawn(bullet);
        }
	}
}