using System.Collections.Generic;
using UnityEngine;
using YooAsset;

/*--------脚本描述-----------
				
电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
    数据加载管理

-----------------------*/

namespace Core
{
    public class CoreData : ICore
    {
        public static CoreData Instance;
        private Dictionary<string, List<IData>> bytesDataDic;//数据

        public void ICroeInit()
        {
            Instance = this;
            bytesDataDic = new Dictionary<string, List<IData>>();
            Debug.Log("数据初始化完毕");
        }

        public void InitData<T>() where T : IData
        {
            RawFileOperationHandle handle = YooAssetLoadExpsion.YooaddetLoadRawFileAsync(typeof(T).FullName);
            byte[] fileData = handle.GetRawFileData();
            List<IData> itemDetailsList = BinaryAnalysis.GetData<T>(fileData);
            if (bytesDataDic.ContainsKey(typeof(T).FullName))
                bytesDataDic[typeof(T).FullName] = itemDetailsList;
            bytesDataDic.Add(typeof(T).FullName, itemDetailsList);
        }

        public void InitData<T>(string filePath) where T : IData
        {
            byte[] fileData = CoreLoadRes.Instance.Load<TextAsset>(filePath).bytes;
            List<IData> itemDetailsList = BinaryAnalysis.GetData<T>(fileData);
            if (bytesDataDic.ContainsKey(typeof(T).FullName))
                bytesDataDic[typeof(T).FullName] = itemDetailsList;
            bytesDataDic.Add(typeof(T).FullName, itemDetailsList);
        }


        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetDataOne<T>(int id) where T : class, IData
        {
            if (!bytesDataDic.ContainsKey(typeof(T).FullName))
            {
                Debug.Log($"未能找到数据请先初始化{typeof(T).FullName}");
                return null;
            }

            IData data = bytesDataDic[typeof(T).FullName].Find(data => { return data.GetId() == id; });
            return data == null ? null : data as T;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<IData> GetDataList<T>() where T : class, IData
        {
            if (!bytesDataDic.ContainsKey(typeof(T).FullName))
            {
                Debug.Log($"未能找到数据请先初始化{typeof(T).FullName}");
                return null;
            }

            List<IData> dataListTemp = bytesDataDic[typeof(T).FullName];
            return dataListTemp;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetDataListAsT<T>() where T : class, IData
        {
            List<T> dataListTemp = new List<T>();
            if (!bytesDataDic.ContainsKey(typeof(T).FullName))
            {
                Debug.Log($"未能找到数据请先初始化{typeof(T).FullName}");
                return null;
            }
            foreach (IData item in bytesDataDic[typeof(T).FullName])
                dataListTemp.Add(item as T);
            return dataListTemp;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void RemoveData(string dataKey)
        {
            if (bytesDataDic.TryGetValue(dataKey, out List<IData> datas))
                bytesDataDic[dataKey] = null;
        }
    }
}
