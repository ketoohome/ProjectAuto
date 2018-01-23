using UnityEngine;
using System.Collections;

namespace GameCommon {

	/// <summary>
	/// 公共调用类型
	/// </summary>
	public class Common 
    {

		// 不同平台的streaming资源路径
		public static readonly string PathURL =
			#if UNITY_STANDALONE_WIN || UNITY_EDITOR
			Application.dataPath + "/StreamingAssets/";
			#elif UNITY_ANDROID
			Application.streamingAssetsPath;
			//"jar:file://" + Application.dataPath + "!/assets/";
			#elif UNITY_IPHONE
			Application.dataPath + "/Raw/";
			#else
			string.Empty;
			#endif

       
        /// <summary>
        /// 存储本地数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        static public void Save(string key, object value)
        {
            if (value.GetType() == typeof(int))
            {
                PlayerPrefs.SetInt(key, (int)value);
            }
            else if (value.GetType() == typeof(float))
            {
                PlayerPrefs.SetFloat(key, (float)value);
            }
            else if (value.GetType() == typeof(string))
            {
                PlayerPrefs.SetString(key, (string)value);
            }
        }

        // 获取本地数据
        static public int GetInt(string key)
        {
            if (!PlayerPrefs.HasKey(key)) Debuger.LogWarning("Key为" + key + "的数据没用存储在本地！");
            return PlayerPrefs.GetInt(key);
        }
        //
        static public float GetFloat(string key)
        {
            if (!PlayerPrefs.HasKey(key)) Debuger.LogWarning("Key为" + key + "的数据没用存储在本地！");
            return PlayerPrefs.GetFloat(key);
        }
        //
        static public string GetString(string key)
        {
            if (!PlayerPrefs.HasKey(key)) Debuger.LogWarning("Key为" + key + "的数据没用存储在本地！");
            return PlayerPrefs.GetString(key);
        }
        // 删除本地数据
        static public void DeleteKey(string key)
        {
            if (!PlayerPrefs.HasKey(key)) Debuger.LogWarning("Key为" + key + "的数据没用存储在本地！");
            PlayerPrefs.DeleteKey(key);
        }
        // 删除本地所有数据
        static public void DeleteAllKey(string key)
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
