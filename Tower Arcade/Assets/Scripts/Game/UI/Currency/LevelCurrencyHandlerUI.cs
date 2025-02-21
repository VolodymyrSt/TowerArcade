using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Game
{
    public class LevelCurrencyHandlerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _soulCountText;

        [SerializeField] private RectTransform _currencyChangeRoot;
        [SerializeField] private TextMeshProUGUI _currencyChangeAmountText;

        private LevelCurencyHandler _curencyHandler;

        private void Start()
        {
            _curencyHandler = LevelDI.Resolve<LevelCurencyHandler>();

            LevelDI.Resolve<EventBus>().SubscribeEvent<OnCurrencyCountChangedSignal>(UpdateCurrencyCount);

            _soulCountText.text = _curencyHandler.GetStartedCurrencyCount().ToString();

            HideCurrencyChangeDisplay();
        }

        private void UpdateCurrencyCount(OnCurrencyCountChangedSignal signal)
        {
            _soulCountText.text = _curencyHandler.GetCurrentCurrencyCount().ToString();

            DisplayCurrencyChange(signal);

            _currencyChangeRoot.DOScale(1f, 0.5f)
                .SetEase(Ease.InOutCubic)
                .Play()
                .OnComplete(() => {
                    StartCoroutine(HideCurrencyChangeDisplayAfterDelay());
                });
        }

        private void HideCurrencyChangeDisplay()
        {
            _currencyChangeRoot.localScale = Vector3.zero;
            _currencyChangeRoot.gameObject.SetActive(false);
        }
        
        private IEnumerator HideCurrencyChangeDisplayAfterDelay()
        {
            yield return new WaitForSecondsRealtime(1f);

            HideCurrencyChangeDisplay();
        }

        private void DisplayCurrencyChange(OnCurrencyCountChangedSignal signal)
        {
            _currencyChangeRoot.gameObject.SetActive(true);

            char operationSign = ' ';

            if (signal != null && signal.AmoutOfCurrency > 0)
            {
                operationSign = '+';
            }

            _currencyChangeAmountText.text = operationSign + signal.AmoutOfCurrency.ToString();
        }
    }
}
