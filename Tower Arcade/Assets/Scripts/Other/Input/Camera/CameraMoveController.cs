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
        [SerializeField, Range(0f, 50f)] private float _maxSensivity = 10f;
        private float _currentSensivity = 10f;
        [SerializeField] private float _smoothness = 1f;

        [Space(10f)]
        [SerializeField] private Vector2 _limitX = new Vector2(0f, 40f);
        [SerializeField] private Vector2 _limitZ = new Vector2(-20f, 30f);

        private Camera _camera;

        private bool _isPaused = false;
        private bool _isEnable = false;

        private void Start()
        {
            _camera = GetComponentInChildren<Camera>();

            LevelDI.Resolve<EventBus>().SubscribeEvent<OnGamePausedSignal>(DisableMovement);
            LevelDI.Resolve<EventBus>().SubscribeEvent<OnGameWonSignal>((OnGameWonSignal signal) => _isEnable = true);
            LevelDI.Resolve<EventBus>().SubscribeEvent<OnGameEndedSignal>((OnGameEndedSignal signal) => _isEnable = true);
        }

        private void LateUpdate()
        {
            Vector2 moveInput = Mouse.current.leftButton.isPressed ? _gameInput.GetCameraMoveVectorNormalized() : Vector2.zero;

            if (_isPaused || _isEnable) moveInput = Vector2.zero;

            MoveCamera(moveInput);
        }

        private void MoveCamera(Vector2 inputDelta)
        {
            inputDelta = inputDelta * _camera.orthographicSize;

            float xPosition = -inputDelta.x * _currentSensivity * Time.deltaTime;
            float yPosition = -inputDelta.y * _currentSensivity * Time.deltaTime;

            Vector3 newPivotPosition = _pivot.transform.position + new Vector3(xPosition, 0f, yPosition);

            newPivotPosition.x = Mathf.Clamp(newPivotPosition.x, _limitX.x, _limitX.y);
            newPivotPosition.z = Mathf.Clamp(newPivotPosition.z, _limitZ.x, _limitZ.y);

            _pivot.transform.position = Vector3.Lerp(_pivot.transform.position, newPivotPosition, _smoothness * Time.deltaTime);
        }

        public void InitMouseSensivity(SaveData saveData,SaveSystem saveSystem)
        {
            if (saveData.MouseSensivity == 0)
                _currentSensivity = saveSystem.Load().MouseSensivity;
            else
                return;
        }

        public void ChangeSensivity(float value, SaveSystem saveSystem, SaveData saveData)
        {
            _currentSensivity = value;
            saveData.MouseSensivity = _currentSensivity;
            saveSystem.Save(saveData);
        }

        public float GetMaxSensivity() => _maxSensivity;

        public void DisableMovement(OnGamePausedSignal signal) => _isPaused = signal.OnGamePaused;
    }
}
