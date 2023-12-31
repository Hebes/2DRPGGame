﻿using System.Collections.Generic;
using UnityEngine;

/*--------脚本描述-----------
				
电子邮箱：
	1607388033@qq.com
作者:
	暗沉
描述:
    UI管理类

-----------------------*/

namespace Core
{
    public class CroeUI : ICore
    {
        public static CroeUI Instance;

        public Transform CanvasTransfrom = null;                        //UI根节点    
        public Camera UICamera = null;                                  //UI摄像机
        public Camera MainCamera = null;                                //主摄像机

        private Dictionary<string, UIBase> _DicALLUIForms;          //缓存所有UI窗体
        private Dictionary<string, UIBase> _DicCurrentShowUIForms;  //当前显示的UI窗体
        private Stack<UIBase> _StaCurrentUIForms;                   //定义“栈”集合,存储显示当前所有[反向切换]的窗体类型
        private Transform Normal = null;                            //全屏幕显示的节点
        private Transform Fixed = null;                             //固定显示的节点
        private Transform PopUp = null;                             //弹出节点
        private Transform Mobile = null;                            //独立的窗口可移动的
        private Transform Fade = null;                              //渐变过度窗体

        public void ICroeInit()
        {
            Instance = this;
            _DicALLUIForms = new Dictionary<string, UIBase>();
            _DicCurrentShowUIForms = new Dictionary<string, UIBase>();
            _StaCurrentUIForms = new Stack<UIBase>();
            InitRoot();
            Debug.Log("UI管理初始化完毕");
        }


        //初始化
        private void InitRoot()
        {
            GameObject gameObjectTemp = CoreLoadRes.Instance.Load<GameObject>("UIPanel/Global/UI");
            GameObject CanvasGoInstantiate = GameObject.Instantiate(gameObjectTemp);
            //实例化
            CanvasTransfrom = CanvasGoInstantiate.transform;
            GameObject.DontDestroyOnLoad(CanvasTransfrom);
            //获取子节点
            Normal = CanvasTransfrom.GetChildComponent<Transform>(EUIType.Normal.ToString());
            Fixed = CanvasTransfrom.GetChildComponent<Transform>(EUIType.Fixed.ToString());
            PopUp = CanvasTransfrom.GetChildComponent<Transform>(EUIType.PopUp.ToString());
            Mobile = CanvasTransfrom.GetChildComponent<Transform>(EUIType.Mobile.ToString());
            Fade = CanvasTransfrom.GetChildComponent<Transform>(EUIType.Fade.ToString());
            UICamera = CanvasTransfrom.GetChildComponent<Camera>("UICamera");//UI相机要添加到主相机的Stack中
            MainCamera = CanvasTransfrom.GetChildComponent<Camera>("MainCamera");
        }

        //界面增删改查方法
        public T ShwoUIPanel<T>(string uiFormName) where T : UIBase, new()
        {
            //是否存在UI类
            _DicALLUIForms.TryGetValue(uiFormName, out UIBase uIBase);
            T t = uIBase == null ? LoadUIPanel<T>(uiFormName) : uIBase as T;//UI类
            CoreMono.Instance.UpdateAddEvent(t.UIUpdate);
            //是否清空“栈集合”中得数据
            if (t.IsClearStack)
                ClearStackArray();

            //根据不同的UI窗体的显示模式，分别作不同的加载处理
            switch (t.mode)
            {
                case EUIMode.Normal: LoadUIToCurrentCache<T>(uiFormName); break; //“普通显示”窗口模式
                case EUIMode.ReverseChange: PushUIFormToStack(uiFormName); break; //需要“反向切换”窗口模式
                case EUIMode.HideOther: EnterUIFormsAndHideOther(uiFormName); break;//“隐藏其他”窗口模式
            }
            return t;
        }
        public T GetUIPanl<T>(string uiFormName) where T : UIBase
        {
            _DicALLUIForms.TryGetValue(uiFormName, out UIBase baseUiForm);
            return baseUiForm as T;
        }
        public void CloseUIForms(string uiFormName)
        {
            //“所有UI窗体”集合中，如果没有记录，则直接返回
            if (_DicALLUIForms.TryGetValue(uiFormName, out UIBase baseUiForm) == false)
                return;
            //DLog.Log($"当前的UI窗体的显示类型是:{baseUiForm.mode}");
            //DLog.Log(LogCoLor.Blue, $"当前的UI窗体的显示类型是:{baseUiForm.mode}");
            //根据窗体不同的显示类型，分别作不同的关闭处理
            CoreMono.Instance.UpdateRemoveEvent(baseUiForm.UIUpdate);
            switch (baseUiForm.mode)
            {
                case EUIMode.Normal: ExitUIForms(uiFormName); break;//普通窗体的关闭
                case EUIMode.ReverseChange: PopUIFroms(); break;//反向切换窗体的关闭
                case EUIMode.HideOther: ExitUIFormsAndDisplayOther(uiFormName); break;//隐藏其他窗体关闭
            }
        }
        public void RemoveUIFroms(string uiFormName)
        {
            //“所有UI窗体”集合中，如果没有记录，则直接返回
            _DicALLUIForms.TryGetValue(uiFormName, out UIBase baseUIForm);
            if (baseUIForm == null) return;
            CoreMono.Instance.UpdateRemoveEvent(baseUIForm.UIUpdate);
            baseUIForm.UIOnDestroy();
        }



        //显示界面
        private T LoadUIPanel<T>(string uiFormName) where T : UIBase, new()
        {
            T t = new T();
            //创建的UI克隆体预设
            GameObject uiPanel = CoreLoadRes.Instance.Load<GameObject>(uiFormName);
            if (uiPanel == null) Debug.Error($"加载预制体失败,请检查资源路径{uiFormName}");

            GameObject uiPanelInstantiate = GameObject.Instantiate(uiPanel);
            t.panelGameObject = uiPanelInstantiate;
            t.UIName = uiFormName;
            t.UIAwake();
            

            //设置父物体
            switch (t.type)
            {
                case EUIType.Normal: uiPanelInstantiate.transform.SetParent(Normal, false); break;//普通窗体节点
                case EUIType.Fixed: uiPanelInstantiate.transform.SetParent(Fixed, false); break;//固定窗体节点
                case EUIType.Mobile: uiPanelInstantiate.transform.SetParent(Mobile, false); break;//独立的窗口可移动的
                case EUIType.PopUp: uiPanelInstantiate.transform.SetParent(PopUp, false); break;//弹出窗体节点
                case EUIType.Fade: uiPanelInstantiate.transform.SetParent(Fade, false); break;//渐变过度窗体
            }

            //把克隆体，加入到“所有UI窗体”（缓存）集合中。
            _DicALLUIForms.Add(uiFormName, t);
            return t;
        }

        /// <summary>
        /// 把当前UI加载到“当前UI”集合中
        /// </summary>
        /// <param name="uiFormName">窗体预设的名称</param>
	    private void LoadUIToCurrentCache<T>(string uiFormName) where T : UIBase
        {
            //如果“正在显示”的集合中，存在整个UI窗体，则直接返回
            if (_DicCurrentShowUIForms.TryGetValue(uiFormName, out UIBase baseUiForm))
                return;
            //把当前窗体，加载到“正在显示”集合中
            if (_DicALLUIForms.TryGetValue(uiFormName, out UIBase baseUIFormFromAllCache))
            {
                _DicCurrentShowUIForms.Add(uiFormName, baseUIFormFromAllCache as T);
                baseUIFormFromAllCache.UIOnEnable();//显示当前窗体的UIOnEnable函数
            }
        }

        /// <summary>
        /// UI窗体入栈
        /// </summary>
        /// <param name="uiFormName">窗体的名称</param>
        private void PushUIFormToStack(string uiFormName)
        {
            if (_StaCurrentUIForms.Count > 0)//判断“栈”集合中，是否有其他的窗体，有则“冻结”处理。
            {
                UIBase topUIForm = _StaCurrentUIForms.Peek();
                topUIForm.Freeze(); //栈顶元素作冻结处理 冻结状态（即：窗体显示在其他窗体下面）
            }

            if (_DicALLUIForms.TryGetValue(uiFormName, out UIBase baseUIForm))//判断“UI所有窗体”集合是否有指定的UI窗体，有则处理。
            {
                baseUIForm.UIOnEnable();//当前窗口显示状态
                _StaCurrentUIForms.Push(baseUIForm);//把指定的UI窗体，入栈操作。
            }
            else
            {
                Debug.Log("baseUIForm==null,Please Check, 参数 uiFormName=" + uiFormName);
            }
        }

        /// <summary>
        /// 打开UI，且隐藏其他UI
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void EnterUIFormsAndHideOther(string strUIName)
        {
            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;

            if (_DicCurrentShowUIForms.TryGetValue(strUIName, out UIBase baseUIForm) == true)
                return;

            //把“正在显示集合”与“栈集合”中所有窗体都隐藏。
            foreach (UIBase baseUI in _DicCurrentShowUIForms.Values)
                baseUI.UIOnDisable();
            foreach (UIBase staUI in _StaCurrentUIForms)
                staUI.UIOnDisable();

            //把当前窗体加入到“正在显示窗体”集合中，且做显示处理。
            if (_DicALLUIForms.TryGetValue(strUIName, out UIBase baseUIFormFromALL))
            {
                _DicCurrentShowUIForms.Add(strUIName, baseUIFormFromALL);
                baseUIFormFromALL.UIOnEnable();//窗体显示
            }
        }



        //界面关闭
        /// <summary>
        /// 退出指定UI窗体
        /// </summary>
        /// <param name="strUIFormName"></param>
        private void ExitUIForms(string strUIFormName)
        {
            //"正在显示集合"中如果没有记录，则直接返回。
            if (_DicCurrentShowUIForms.TryGetValue(strUIFormName, out UIBase baseUIForm) == false)
                return;
            //指定窗体，标记为“隐藏状态”，且从"正在显示集合"中移除。
            baseUIForm.UIOnDisable();
            _DicCurrentShowUIForms.Remove(strUIFormName);
        }

        /// <summary>
        /// （“反向切换”属性）窗体的出栈逻辑
        /// </summary>
        private void PopUIFroms()
        {
            if (_StaCurrentUIForms.Count >= 2)
            {
                UIBase topUIForms = _StaCurrentUIForms.Pop();//出栈处理
                topUIForms.UIOnDisable();//做隐藏处理
                UIBase nextUIForms = _StaCurrentUIForms.Peek();//出栈后，下一个窗体做“重新显示”处理。
                nextUIForms.UIOnEnable();
            }
            else if (_StaCurrentUIForms.Count == 1)
            {
                UIBase topUIForms = _StaCurrentUIForms.Pop();//出栈处理
                topUIForms.UIOnDisable(); //做隐藏处理
            }
        }

        /// <summary>
        /// (“隐藏其他”属性)关闭窗体，且显示其他窗体
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void ExitUIFormsAndDisplayOther(string strUIName)
        {
            if (_DicCurrentShowUIForms.TryGetValue(strUIName, out UIBase baseUIForm) == false)
                return;

            //当前窗体隐藏状态，且“正在显示”集合中，移除本窗体
            baseUIForm.UIOnDisable();
            _DicCurrentShowUIForms.Remove(strUIName);

            //把“正在显示集合”与“栈集合”中所有窗体都定义重新显示状态。
            foreach (UIBase baseUI in _DicCurrentShowUIForms.Values)
                baseUI.UIOnEnable();
            foreach (UIBase staUI in _StaCurrentUIForms)
                staUI.UIOnEnable();
        }


        //其他
        /// <summary>
        /// 是否清空“栈集合”中得数据
        /// </summary>
        /// <returns></returns>
        private bool ClearStackArray()
        {
            if (_StaCurrentUIForms != null && _StaCurrentUIForms.Count >= 1)
            {
                _StaCurrentUIForms.Clear();//清空栈集合
                return true;
            }
            return false;
        }
    }
}
