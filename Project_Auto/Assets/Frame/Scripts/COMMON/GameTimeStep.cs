using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TOOL;

namespace GameCommon
{
    /// <summary>
    /// 用于控制时间
    /// </summary>
    public class GameTimeStepMgr : Singleton<GameTimeStepMgr>
    {
        // 定时器列表
        Dictionary<uint, Step> m_Steps = new Dictionary<uint, Step>();

        static uint counter = 0;

        // 如果时间为0的话，代表当前时间更改没有时间限制，直到强制时间恢复正常
        public void StepGameWithTime(float clock, float timescale)
        {
            counter++;
            timescale = Mathf.Max(timescale, 0.01f);
            Time.timeScale = timescale;

            EventMachine.SendEvent(EventID.Event_Time_ScaleChange);
            m_Steps.Add(counter, new Step(counter, clock, timescale));
        }

        // 如果帧数为0的话，代表当前更改没有帧数限制，直到强制时间恢复正常
        public void StepGameWithFrames(uint frames, float timescale)
        {
            counter++;
            timescale = Mathf.Max(timescale, 0);
            Time.timeScale = timescale;

			EventMachine.SendEvent(EventID.Event_Time_ScaleChange);
            m_Steps.Add(counter, new Step(counter, frames, timescale));
        }

        bool m_isPause = false;
        /// <summary>
        /// 暂停游戏
        /// </summary>
        public bool IsPause {
            get { return m_isPause; }
            set { 
                if (value == m_isPause) {
                    Debuger.LogWarning("时间暂停状态已经设置为：" + m_isPause);
                    return;
                }
                m_isPause = value;
                // 暂停游戏
                if (m_isPause)
                {
                    Time.timeScale = 0;
					EventMachine.SendEvent(EventID.Event_Time_ScaleChange);
                }
                // 解除暂停，如果当前有帧延迟队列中仍然有为处理对象，则跳过
                else if (m_Steps.Count == 0)
                {
                    Time.timeScale = 1;
					EventMachine.SendEvent(EventID.Event_Time_ScaleChange);
                }
                else {
                    int num = m_Steps.Count;
                    uint[] strKey = new uint[num];
                    m_Steps.Keys.CopyTo(strKey,0);
                    Time.timeScale = m_Steps[strKey[num - 1]].m_timescale;
                }
            }
        }

        // 移除时间限制
        public void RemoveStepGame(uint id) {
            if (m_Steps.ContainsKey(id)) m_Steps.Remove(id);
            if (m_Steps.Count == 0 && !m_isPause)
            {
                Time.timeScale = 1;
				EventMachine.SendEvent(EventID.Event_Time_ScaleChange);
            }
            else if (m_Steps.Count > 0 && !m_isPause)
            {
                int num = m_Steps.Count;
                uint[] strKey = new uint[num];
                m_Steps.Keys.CopyTo(strKey, 0);
                Time.timeScale = m_Steps[strKey[num - 1]].m_timescale;
				EventMachine.SendEvent(EventID.Event_Time_ScaleChange);
            }
        }

        // 强制时间恢复正常
        public void NormalGameTimeScale() {
            m_Steps.Clear();
            Time.timeScale = 1;
            m_isPause = false;
			EventMachine.SendEvent(EventID.Event_Time_ScaleChange);
        }

        /// <summary>
        /// 时间块
        /// </summary>
        class Step
        {
            public uint m_id = 0;
            public float m_clock = 0;
            public uint m_frames = 0;
            public float m_timescale = 1;

            public Step(uint id, float clock, float timescale)
            {
                m_id = id;
                m_clock = clock;
                m_timescale = timescale;
                if(clock > 0) ClockMachine.It.CreateClock(clock * timescale, End);
            }

            public Step(uint id, uint frames, float timescale)
            {
                m_id = id;
                m_frames = frames;
                if (frames > 0) CounterMachine.It.CreateCounter("StepGameEnd_"+ m_id, (int)frames, End);
            }

            void End()
            {
                GameTimeStepMgr.It.RemoveStepGame(m_id);
            }
        }

        /// <summary>
        /// 根据更新的帧数设置时间帧延迟
        /// </summary>
        /// <param name="count"></param>
        /// <param name="timescale"></param>
        public static void TimeDelayWithFrame(uint count, float timescale)
        {
            GameTimeStepMgr.It.StepGameWithFrames(count, timescale);
        }

        /// <summary>
        /// 根据事件来设置时间帧延迟
        /// </summary>
        /// <param name="clock"></param>
        /// <param name="timescale"></param>
        public static void TimeDelay(float clock, float timescale)
        {
            GameTimeStepMgr.It.StepGameWithTime(clock, timescale);
        }

        /// <summary>
        /// 设置时间速度
        /// </summary>
        /// <param name="timescale"></param>
        public static void TimeDelay(float timescale)
        {
            GameTimeStepMgr.It.StepGameWithTime(0, timescale);
        }

        /// <summary>
        /// 帧延迟结束
        /// </summary>
        public static void StempGameEnd()
        {
            GameTimeStepMgr.It.NormalGameTimeScale();
        }
    }
}