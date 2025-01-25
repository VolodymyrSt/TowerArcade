using TMPro;
using UnityEngine;

namespace Game
{
    public class LevelCurrencyHandlerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _soulCountText;

        private LevelCurencyHandler _curencyHandler;

        private void Start()
        {
            _curencyHandler = LevelRegistrator.Resolve<LevelCurencyHandler>();

            LevelRegistrator.Resolve<EventBus>().SubscribeEvent<OnCurrencyCountChangedSignal>(ChangeCurrencyCount);

            _soulCountText.text = _curencyHandler.GetStartedCurrencyCount().ToString();
        }

        private void ChangeCurrencyCount(OnCurrencyCountChangedSignal signal)
        {
            _soulCountText.text = _curencyHandler.GetCurrentCurrencyCount().ToString();
        }
    }
}
