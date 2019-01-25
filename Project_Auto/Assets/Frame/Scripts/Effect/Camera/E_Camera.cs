using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;
using TOOL;


namespace GameEffect {

    [ExecuteInEditMode]
    public class E_Camera : U3DSingleton<E_Camera>
    {
        public List<AnimationCurve> m_CameraVibrationCurves;
        Animator mAnimator = null;

        void Awake() {
            EventMachine.Register(EventID.Event_Effect_CameraVibration, OnCameraVibration);
			EventMachine.Register(EventID.Event_Effect_CameraAnimation, OnCameraAnimation);
        }

        void OnDestroy() {
            EventMachine.Unregister(EventID.Event_Effect_CameraVibration, OnCameraVibration);
			EventMachine.Unregister(EventID.Event_Effect_CameraAnimation, OnCameraAnimation);
        }
        void Update()
        {
            /*
            if (Input.GetKeyDown(KeyCode.Space))
                EventMachine.SendEvent(EventID.Event_Effect_CameraVibration,0.3f, Vector3.forward);
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                EventMachine.SendEvent(EventID.Event_Effect_CameraVibration, 0.31f, Vector3.up);
                EventMachine.SendEvent(EventID.Event_Effect_CameraVibration, 0.27f, Vector3.left);
            }
            */

            UpdateVibration();
        }

        void OnCameraVibration(params object[] args) {
            Vibration vib = new Vibration();
            vib.duration = (float)args[0];
            vib.direction = (Vector3)args[1];
			if(args.Length == 3)
                vib.curve = m_CameraVibrationCurves[(int)args[2]];
			else
                vib.curve = m_CameraVibrationCurves[0];
            mVibrations.Add(vib);
        }

		void OnCameraAnimation(params object[] args){
			string animName = (string)args [0];
            if (mAnimator == null) mAnimator = GetComponent<Animator>();
            mAnimator.Play(animName);
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
        List<Vibration> mVibrations = new List<Vibration>();

        void UpdateVibration()
        {
            Vector3 vec = Vector3.zero;
            foreach (Vibration vib in mVibrations) {
                vec += vib.update();
            }

            vibration_X = vec.x; vibration_Y = vec.y; vibration_Z = vec.z;

            for (int i = mVibrations.Count - 1; i >= 0; i--) {
                if (mVibrations[i].IsEnd) mVibrations.Remove(mVibrations[i]);
            }
        }

        #region Graphics

        private Material material;

        [Header("vibration Settings")]
        [Space()]
        [Range(-1, 1)]
        public float vibration_X;
        [Range(-1, 1)]
        public float vibration_Y;
        [Range(-1, 1)]
        public float vibration_Z;

        [Header("Effect Settings")]
        [Space()]
        [Range(0, 1)]
        public float Inverse;
        [Range(0, 1)]
        public float WhiteBlack;
        void Start()
        {
            if (material == null)
            {
                material = new Material(Shader.Find("Hidden/Camera Vibration Shader"));
                material.hideFlags = HideFlags.HideAndDontSave;
            }
        }

        void OnEnable()
        {
            if (material == null)
            {
                material = new Material(Shader.Find("Hidden/Camera Vibration Shader"));
                material.hideFlags = HideFlags.HideAndDontSave;
            }
        }


        void OnDisable()
        {
            if (material != null)
            {
                DestroyImmediate(material);
            }
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (material == null)
            {
                Graphics.Blit(source, destination);
            }
            else
            {
                material.SetFloat("_Vibration_X", vibration_X);
                material.SetFloat("_Vibration_Y", vibration_Y);
                material.SetFloat("_Vibration_Z", vibration_Z);
                material.SetFloat("_Inverse", Inverse);
                material.SetFloat("_WhiteBlack", WhiteBlack);
                Graphics.Blit(source, destination, material);
            }
        }
        #endregion
    }
}

