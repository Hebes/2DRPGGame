using System.Collections.Generic;

/*--------脚本描述-----------
				
电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
    数据获取帮助类

-----------------------*/

namespace Core
{
    public static class DataExpansion
    {
        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        public static void InitData<T>(string filePath) where T : IData
        {
            CoreData.Instance.InitData<T>(filePath);
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetDataOne<T>(this int id) where T : class, IData
        {
            return CoreData.Instance.GetDataOne<T>(id);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<IData> GetDataList<T>(this object obj) where T : class, IData
        {
            return CoreData.Instance.GetDataList<T>();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> GetDataList<T>() where T : class, IData
        {
            List<T> list = new List<T>();
            List<IData> tempList = CoreData.Instance.GetDataList<T>();
            for (int i = 0; i < tempList.Count; i++)
                list.Add(tempList[i] as T);
            return list;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<T> GetData<T>(this object obj) where T : class, IData
        {
            List<T> list = new List<T>();
            if (CoreData.Instance == null)
                Debug.Log($"是空的");
            List<IData> tempList = CoreData.Instance.GetDataList<T>();
            if (tempList == null || tempList.Count == 0)
                Debug.Error($"请先初始化数据{typeof(T).FullName}");
            for (int i = 0; i < tempList.Count; i++)
                list.Add(tempList[i] as T);
            return list;
        }
    }
}
