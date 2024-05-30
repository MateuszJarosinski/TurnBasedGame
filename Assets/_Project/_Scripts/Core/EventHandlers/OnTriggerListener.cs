using UnityEngine;
using UnityEngine.Events;

namespace Core.EventHandlers
{
    [RequireComponent(typeof(Collider))]
    public class OnTriggerListener : MonoBehaviour
    {
        public UnityEvent OnEnter;
        public UnityEvent OnExit;
        public UnityEvent OnStay;

        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke();
        }

        private void OnTriggerStay(Collider other)
        {
            OnStay?.Invoke();
        }
    }
}