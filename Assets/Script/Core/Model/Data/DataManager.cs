﻿using System.Collections.Generic;
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
    public class DataManager : ICore
    {
        public static DataManager Instance;
        private Dictionary<string, List<IData>> bytesDataDic;//数据

        public void ICroeInit()
        {
            Instance = this;
            bytesDataDic = new Dictionary<string, List<IData>>();
            Debug.Log("数据初始化完毕");
        }

        //初始化数据
        public void InitData<T>(string fileName) where T : IData
        {
            RawFileOperationHandle handle = YooAssetLoadExpsion.YooaddetLoadRawFileAsync(fileName);
            byte[] fileData = handle.GetRawFileData();
            List<IData> itemDetailsList = BinaryAnalysis.GetData<T>(fileData);
            if (bytesDataDic.ContainsKey(typeof(T).FullName))
                bytesDataDic[typeof(T).FullName] = itemDetailsList;
            bytesDataDic.Add(typeof(T).FullName, itemDetailsList);
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


        //获取数据
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
    }
}
