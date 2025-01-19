using UnityEngine;

namespace Game
{
    public class SkipBottonAnimationHandler : MonoBehaviour
    {
        private const string SHOW = "Show";

        [SerializeField] private Animator _animator;
        private void OnValidate()
        {
            _animator ??= GetComponent<Animator>();
        }

        private void OnEnable() => _animator.SetTrigger(SHOW);
    }
}
