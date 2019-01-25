using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;
using TOOL;

namespace GameEffect
{
    public class E_CameraVibration : MonoBehaviour
    {
        public Vector3 Direction = Vector3.zero;
        public float Duration = 0.3f;
        public int Index = 0;

        private void OnEnable()
        {
            EventMachine.SendEvent(EventID.Event_Effect_CameraVibration, Duration, Direction, Index);
        }
    }
}