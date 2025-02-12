using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Values")]
        [SerializeField] private float _scaleValue = 1.1f;
        [SerializeField] private float _normalValue = 1f;
        [SerializeField] private float _duration = 0.3f;

        [Header("Ease")]
        [SerializeField] private Ease _ease;

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(_scaleValue, _duration)
                .SetEase(_ease)
                .Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(_normalValue, _duration)
                .SetEase(_ease)
                .Play();
        }
    }
}
