using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class CameraMoveController : MonoBehaviour
    {
        [Header("Controllers:")]
        [SerializeField] private GameObject _pivot;
        [SerializeField] private GameInput _gameInput;

        [Header("Settings:")]
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _smoothness = 1f;

        [Space(10f)]
        [SerializeField] private Vector2 _limitX = new Vector2(0f, 40f);
        [SerializeField] private Vector2 _limitZ = new Vector2(-20f, 30f);

        private Camera _camera;

        private void Start() => _camera = GetComponentInChildren<Camera>();

        private void LateUpdate()
        {
            Vector2 moveInput = Mouse.current.leftButton.isPressed ? _gameInput.GetCameraMoveVectorNormalized() : Vector2.zero;
            MoveCamera(moveInput);
        }

        private void MoveCamera(Vector2 inputDelta)
        {
            inputDelta = inputDelta * _camera.orthographicSize;

            float xPosition = -inputDelta.x * _moveSpeed * Time.deltaTime;
            float yPosition = -inputDelta.y * _moveSpeed * Time.deltaTime;

            Vector3 newPivotPosition = _pivot.transform.position + new Vector3(xPosition, 0f, yPosition);

            newPivotPosition.x = Mathf.Clamp(newPivotPosition.x, _limitX.x, _limitX.y);
            newPivotPosition.z = Mathf.Clamp(newPivotPosition.z, _limitZ.x, _limitZ.y);

            _pivot.transform.position = Vector3.Lerp(_pivot.transform.position, newPivotPosition, _smoothness * Time.deltaTime);
        }
    }
}
