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
		void OnEnable () {
            if(FrameCount == 0)
                StartCoroutine(StepActiveWithClock(Delay, Duration, timeScale));
            else StartCoroutine(StepActiveWithClock(Delay, FrameCount, timeScale));
        }

        IEnumerator StepActiveWithClock(float delay, float clock,float scale) {
            yield return new WaitForSeconds(delay);
            GameTimeStepMgr.It.StepGameWithTime(clock, scale);
        }

        IEnumerator StepActiveWithFrame(float delay, int frame, float scale)
        {
            yield return new WaitForSeconds(delay);
            GameTimeStepMgr.It.StepGameWithFrames((uint)frame, timeScale);
        }
	}
}