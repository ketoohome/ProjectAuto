using UnityEngine;
using System.Collections;
using TOOL;
using GameCommon;

namespace GameEffect
{
	public class E_StepFrames : MonoBehaviour {

		public float Delay = 0;		  // 延迟
		public int FrameCount = 10;   // 持续帧数
		public float Duration = 0.1f; // 持续时间
		public float timeScale = 0.2f;// 时间速度倍数

		// Use this for initialization
		void Awake () {
			ClockMachine.It.CreateClock("Step"+gameObject.GetInstanceID(),Delay,StepActive);
		}
		void StepActive(){
            if (FrameCount != 0)
                GameTimeStepMgr.It.StepGameWithFrames((uint)FrameCount, timeScale);
            else
                GameTimeStepMgr.It.StepGameWithTime(Duration, timeScale);
			Destroy(this);
		}
	}
}