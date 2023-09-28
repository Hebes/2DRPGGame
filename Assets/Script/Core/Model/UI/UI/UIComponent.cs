using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;//Object并非C#基础中的Object，而是 UnityEngine.Object


/*--------脚本描述-----------

电子邮箱：
    1607388033@qq.com
作者:
    暗沉
描述:
    UI脚本主要类

-----------------------*/

namespace Core
{
    /// <summary> 使其能在Inspector面板显示，并且可以被赋予相应值 </summary>
    [Serializable]
    public class UIData
    {
        public string key;
        public Object gameObject;
    }



    /// <summary> 继承IComparer对比器，Ordinal会使用序号排序规则比较字符串，因为是byte级别的比较，所以准确性和性能都不错 </summary>
    public class UIDataComparer : IComparer<UIData>
    {
        public int Compare(UIData x, UIData y)
        {
            return string.Compare(x.key, y.key, StringComparison.Ordinal);
        }
    }



    public class UIComponent : MonoBehaviour, ISerializationCallbackReceiver
    {
        public List<UIData> data = new List<UIData>();
        private readonly Dictionary<string, Object> dict = new Dictionary<string, Object>();



        //数据操作
        public void Add(string key, Object obj)
        {
            UnityEditor.SerializedObject serializedObject = new UnityEditor.SerializedObject(this);
            //根据PropertyPath读取数据
            //如果不知道具体的格式，可以右键用文本编辑器打开一个prefab文件（如Bundles/UI目录中的几个）
            //因为这几个prefab挂载了ReferenceCollector，所以搜索data就能找到存储的数据
            UnityEditor.SerializedProperty dataProperty = serializedObject.FindProperty("data");
            int i;
            //遍历data，看添加的数据是否存在相同key
            for (i = 0; i < data.Count; i++)
            {
                if (data[i].key == key)
                {
                    break;
                }
            }
            //不等于data.Count意为已经存在于data List中，直接赋值即可
            if (i != data.Count)
            {
                //根据i的值获取dataProperty，也就是data中的对应ReferenceCollectorData，不过在这里，是对Property进行的读取，有点类似json或者xml的节点
                UnityEditor.SerializedProperty element = dataProperty.GetArrayElementAtIndex(i);
                //对对应节点进行赋值，值为gameobject相对应的fileID
                //fileID独一无二，单对单关系，其他挂载在这个gameobject上的script或组件会保存相对应的fileID
                element.FindPropertyRelative("gameObject").objectReferenceValue = obj;
            }
            else
            {
                //等于则说明key在data中无对应元素，所以得向其插入新的元素
                dataProperty.InsertArrayElementAtIndex(i);
                UnityEditor.SerializedProperty element = dataProperty.GetArrayElementAtIndex(i);
                element.FindPropertyRelative("key").stringValue = key;
                element.FindPropertyRelative("gameObject").objectReferenceValue = obj;
            }
            //应用与更新
            UnityEditor.EditorUtility.SetDirty(this);
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }
        public void Remove(string key)
        {
            UnityEditor.SerializedObject serializedObject = new UnityEditor.SerializedObject(this);
            UnityEditor.SerializedProperty dataProperty = serializedObject.FindProperty("data");
            int i;
            for (i = 0; i < data.Count; i++)
            {
                if (data[i].key == key)
                    break;
            }
            if (i != data.Count)
            {
                dataProperty.DeleteArrayElementAtIndex(i);
            }
            UnityEditor.EditorUtility.SetDirty(this);
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }
        public void Clear()
        {
            UnityEditor.SerializedObject serializedObject = new UnityEditor.SerializedObject(this);
            //根据PropertyPath读取prefab文件中的数据
            //如果不知道具体的格式，可以直接右键用文本编辑器打开，搜索data就能找到
            var dataProperty = serializedObject.FindProperty("data");
            dataProperty.ClearArray();//从数组中删除所有元素。
            UnityEditor.EditorUtility.SetDirty(this);//将目标对象标记为脏对象。
            serializedObject.ApplyModifiedProperties();//应用修改后的属性
            serializedObject.UpdateIfRequiredOrScript();//更新序列化对象的表示，仅当对象自上次调用Update后被修改或它是一个脚本时才更新。
        }
        public void Sort()
        {
            UnityEditor.SerializedObject serializedObject = new UnityEditor.SerializedObject(this);
            data.Sort(new UIDataComparer());
            UnityEditor.EditorUtility.SetDirty(this);
            serializedObject.ApplyModifiedProperties();
            serializedObject.UpdateIfRequiredOrScript();
        }



        //获取组件
        public T Get<T>(string key) where T : class
        {
            if (dict.TryGetValue(key, out Object dictGo))
                return dictGo as T;
            return null;
        }
        public T GetComponent<T>(string key) where T : Component
        {
            if (dict.TryGetValue(key, out Object dictGo))
                return (dictGo as GameObject).GetComponent<T>();
            return null;
        }
        public Object GetObject(string key)
        {
            if (dict.TryGetValue(key, out Object dictGo))
                return dictGo;
            return null;
        }



        //数据设置
        public void OnBeforeSerialize()
        {
            //UnityEngine.Debug.Log("实现这个方法在Unity序列化你的对象之前接收回调。");
        }
        public void OnAfterDeserialize()
        {
            //UnityEngine.Debug.Log("实现这个方法在Unity反序列化你的对象后接收回调。");
            dict.Clear();
            foreach (UIData uiData in data)
            {
                if (!dict.ContainsKey(uiData.key))
                    dict.Add(uiData.key, uiData.gameObject);
            }
        }
    }
}