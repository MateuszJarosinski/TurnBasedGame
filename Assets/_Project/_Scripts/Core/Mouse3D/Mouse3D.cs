using Core.Singleton;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Mouse3D
{
    public class Mouse3D : MonoBehaviourSingleton<Mouse3D>
    {
        [SerializeField] private LayerMask mouseColliderLayerMask;
        [SerializeField] private Transform mouseTransform;
        [SerializeField] private float followSpeed = 20f;

        private void Update()
        {
            if (mouseTransform != null)
            {
                mouseTransform.position = Vector3.Lerp(mouseTransform.position, GetMouseWorldPosition(), Time.deltaTime * followSpeed);
            }
        }

        public static Vector3 GetMouseWorldPosition()
        {
            if (Instance == null)
            {
                Debug.LogError("Mouse3D Object does not exist!");
            }

            return Instance.GetMouseWorldPosition_Instance();
        }

        private Vector3 GetMouseWorldPosition_Instance()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, mouseColliderLayerMask))
            {
                return raycastHit.point;
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}
