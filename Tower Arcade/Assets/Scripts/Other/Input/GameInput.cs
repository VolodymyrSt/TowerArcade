using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class GameInput : MonoBehaviour
    {
        private InputActions _action;

        private void Awake()
        {
            _action = new InputActions();
            _action.Enable();
        }

        public Vector2 GetScrollVectorNormalized()
        {
            return _action.Player.Zoom.ReadValue<Vector2>().normalized;
        }

        public Vector2 GetCameraMoveVectorNormalized()
        {
            return _action.Player.MoveCamera.ReadValue<Vector2>().normalized;
        }

        private void OnDestroy()
        {
            _action.Disable();
        }
    }
}
