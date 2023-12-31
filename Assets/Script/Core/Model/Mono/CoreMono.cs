﻿using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using Time = UnityEngine.Time;

namespace Core
{
    public class CoreMono : ICore
    {
        public static CoreMono Instance;
        private MonoController monoController;
        private GameObject monoTemp;
        public void ICroeInit()
        {
            Instance = this;
            monoTemp = new GameObject("Mono");
            monoController = monoTemp.AddComponent<MonoController>();
            GameObject.DontDestroyOnLoad(monoTemp);
            Debug.Log("初始化Mono完毕!");
        }


        private float m_Time = 0f;

        public void AwakeAddEvent(UnityAction unityAction)
        {
            monoController.AwakeAddEvent(unityAction);
        }
        public void AwakeRemoveEvent(UnityAction unityAction)
        {
            monoController.AwakeRemoveEvent(unityAction);
        }

        public void StartAddEvent(UnityAction unityAction)
        {
            monoController.StartAddEvent(unityAction);
        }
        public void StartRemoveEvent(UnityAction unityAction)
        {
            monoController.StartRemoveEvent(unityAction);
        }

        public void UpdateAddEvent(UnityAction unityAction)
        {
            monoController.UpdateAddEvent(unityAction);
        }
        public void UpdateRemoveEvent(UnityAction unityAction)
        {
            monoController.UpdateRemoveEvent(unityAction);
        }

        public void OnAddFixedUpdateEvent(UnityAction unityAction)
        {
            monoController.OnAddFixedUpdateEvent(unityAction);
        }
        public void OnRemoveFixedUpdateEvent(UnityAction unityAction)
        {
            monoController.OnRemoveFixedUpdateEvent(unityAction);
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            return monoController.StartCoroutine(routine);
        }
        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return monoController.StartCoroutine(methodName, value);
        }
        public Coroutine StartCoroutine(string methodName)
        {
            return monoController.StartCoroutine(methodName);
        }
        public void MonoStopCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            monoController.StopCoroutine(methodName);
        }
        public void MonoStopCoroutine(IEnumerator routine)
        {
            monoController.StopCoroutine(routine);
        }

        public void MonoStopCoroutine(Coroutine routine)
        {
            monoController.StopCoroutine(routine);
        }

        public void Pause()
        {
            m_Time = Time.timeScale;
            Time.timeScale = 0f;//会影响UpData的Time.DataTime,但是Update函数仍在执行 和 FixedUpdate
        }
        public void UnPause(float m_Time)
        {
            Time.timeScale = m_Time;
            this.m_Time = Time.timeScale;
        }
    }
}
