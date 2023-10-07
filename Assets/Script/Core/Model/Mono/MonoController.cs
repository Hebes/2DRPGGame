using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class MonoController : MonoBehaviour
    {
        private event UnityAction AwakeEvent;
        private event UnityAction StartEvent;
        private event UnityAction UpdateEvent;
        private event UnityAction FixedUpdateEvent;

        private void Awake()
        {
            AwakeEvent?.Invoke();
        }
        private void Start()
        {
            StartEvent?.Invoke();
        }
        private void Update()
        {
            UpdateEvent?.Invoke();
        }
        private void FixedUpdate()
        {
            FixedUpdateEvent?.Invoke();
        }

        public void AwakeAddEvent(UnityAction  unityAction)
        {
            AwakeEvent += unityAction;
        }
        public void AwakeRemoveEvent(UnityAction unityAction)
        {
            AwakeEvent -= unityAction;
        }


        public void StartAddEvent(UnityAction unityAction)
        {
            StartEvent += unityAction;
        }
        public void StartRemoveEvent(UnityAction unityAction)
        {
            StartEvent -= unityAction;
        }

        public void UpdateAddEvent(UnityAction unityAction)
        {
            UpdateEvent += unityAction;
        }
        public void UpdateRemoveEvent(UnityAction unityAction)
        {
            UpdateEvent -= unityAction;
        }

        public void OnAddFixedUpdateEvent(UnityAction unityAction)
        {
            FixedUpdateEvent += unityAction;
        }
        public void OnRemoveFixedUpdateEvent(UnityAction unityAction)
        {
            FixedUpdateEvent -= unityAction;
        }
    }
}
