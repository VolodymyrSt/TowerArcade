using UnityEngine;

namespace Game
{
    public class CameraZoomController : MonoBehaviour
    {
        [Header("Controllers:")]
        [SerializeField] private Camera _camera;
        [SerializeField] private GameInput _gameInput;

        [Header("Settings:")]
        [SerializeField] private float _min = 4;
        [SerializeField] private float _max = 10;
        [SerializeField] private float _zoomSpeed = 50;
        [SerializeField] private float _smoothness = 1;

        private float _orthoSize;
        private float _velocity;

        private bool _isPaused = false;
        private bool _isEnable = false;

        private void Start() 
        {
            _orthoSize = _camera.orthographicSize;

            LevelDI.Resolve<EventBus>().SubscribeEvent<OnGamePausedSignal>(IsGamePaused);
            LevelDI.Resolve<EventBus>().SubscribeEvent<OnGameWonSignal>((OnGameWonSignal signal) => _isEnable = true);
            LevelDI.Resolve<EventBus>().SubscribeEvent<OnGameEndedSignal>((OnGameEndedSignal signal) => _isEnable = true);
        }


        private void LateUpdate()
        {
            var scrollDelta = _gameInput.GetScrollVectorNormalized().y;

            if (_isPaused || _isEnable) scrollDelta = 0;

            PerformZoom(scrollDelta);
        }

        private void PerformZoom(float scrollDelta)
        {
            _orthoSize = Mathf.Clamp(_orthoSize - scrollDelta * _zoomSpeed * Time.deltaTime, _min, _max);

            var newOrthoSize = Mathf.SmoothDamp(_camera.orthographicSize, _orthoSize, ref _velocity, _smoothness);

            _camera.orthographicSize = newOrthoSize;
        }

        public void IsGamePaused(OnGamePausedSignal signal) => _isPaused = signal.OnGamePaused;
    }
}
