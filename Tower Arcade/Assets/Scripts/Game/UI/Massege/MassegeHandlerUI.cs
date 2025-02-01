using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Game
{
    public class MassegeHandlerUI : MonoBehaviour
    {  
        [SerializeField] private RectTransform _root;
        [SerializeField] private TextMeshProUGUI _messege;
        [SerializeField] private float _showDuration = 0.5f;
        [SerializeField] private float _hideDuration = 0f;

        private void Awake() => ResetMassegeScale();

        public void ShowMassege(string text)
        {
            _messege.text = text;

            _root.DOScale(1, _showDuration)
                  .SetEase(Ease.OutBounce)
                  .Play()
                  .OnComplete(async () =>
                  {
                      await Task.Delay((int)(_hideDuration * 1000));

                      ResetMassegeScale();
                  });
        }

        private void ResetMassegeScale()
        {
            _root.localScale = Vector3.zero;
        }
    }
}
