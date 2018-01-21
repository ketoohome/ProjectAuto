using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;
using TOOL;

namespace GameEffect {
    public class E_Camera : U3DSingleton<E_Camera>
    {
        public List<AnimationCurve> m_CameraVibrationCurves;

		Animator m_Animator;

        void Awake() {
			m_Animator = GetComponent<Animator> ();

            EventMachine.Register(EventID.Event_Effect_CameraVibration, OnCameraVibration);
			EventMachine.Register(EventID.Event_Effect_CameraAnimation, OnCameraAnimation);
        }

        void OnDestroy() {
            EventMachine.Unregister(EventID.Event_Effect_CameraVibration, OnCameraVibration);
			EventMachine.Unregister(EventID.Event_Effect_CameraAnimation, OnCameraAnimation);
        }
        void Update()
        {
            UpdateVibration();
        }

        void OnCameraVibration(params object[] args) {
            m_Vibration = new Vibration();
            m_Vibration.duration = (float)args[0];
            m_Vibration.direction = (Vector3)args[1];
			if(args.Length == 3) 
				m_Vibration.curve = m_CameraVibrationCurves[(int)args[2]];
			else  
				m_Vibration.curve = m_CameraVibrationCurves[0];
        }

		void OnCameraAnimation(params object[] args){
			string animName = (string)args [0];
			m_Animator.Play (animName);
		}

        class Vibration {
            public float duration = 0.5f;
            public Vector3 direction = Vector3.up;
            public AnimationCurve curve;

            float clock = 0;
            public Vector3 update() {
                clock += Time.deltaTime;
                return direction*curve.Evaluate(clock/duration);
            }

            public bool IsEnd {
                get { return clock/duration > 1; }
            }
        }
        Vibration m_Vibration = null;

        void UpdateVibration()
        {
            if (m_Vibration == null) return;
            Camera.main.transform.localPosition = m_Vibration.update();
            if (m_Vibration.IsEnd) m_Vibration = null;
        }
    }
}

