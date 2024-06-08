using UnityEngine;
using UnityEngine.Events;

namespace Core.EventHandlers
{
    [RequireComponent(typeof(Collider))]
    public class OnTriggerListener : MonoBehaviour
    {
        public UnityEvent<GameObject> OnEnter;
        public UnityEvent<GameObject> OnExit;
        public UnityEvent<GameObject> OnStay;

        private void OnTriggerEnter(Collider other)
        {
            OnEnter?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit?.Invoke(other.gameObject);
        }

        private void OnTriggerStay(Collider other)
        {
            OnStay?.Invoke(other.gameObject);
        }
    }
}