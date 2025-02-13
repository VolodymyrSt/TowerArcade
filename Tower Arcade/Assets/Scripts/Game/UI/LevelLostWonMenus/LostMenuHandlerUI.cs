using DG.Tweening;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LostMenuHandlerUI : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField] private RectTransform _movableRoot;

        [Header("Buttons")]
        [SerializeField] private Button _goToMenuButton;
        [SerializeField] private Button _exitButton;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _coinsAmountText;

        //Dependencies
        private LevelSoundHandler _soundHandler;
        private LevelConfigurationSO _levelConfigurationSO;
        private CoinBalanceUI _coinBalanceUI;
        private SaveData _saveData;
        private SaveSystem _saveSystem;

        private void Start()
        {
            SceneLoader sceneLoader = LevelRegistrator.Resolve<SceneLoader>();
            LevelRegistrator.Resolve<EventBus>().SubscribeEvent<OnGameEndedSignal>(ShowLostMenu);

            _levelConfigurationSO = LevelRegistrator.Resolve<LevelConfigurationSO>();
            _soundHandler = LevelRegistrator.Resolve<LevelSoundHandler>();
            _coinBalanceUI = LevelRegistrator.Resolve<CoinBalanceUI>();
            _saveData = LevelRegistrator.Resolve<SaveData>();
            _saveSystem = LevelRegistrator.Resolve<SaveSystem>();

            InitButtons(sceneLoader);

            _coinsAmountText.text = _levelConfigurationSO.GetLostCoins().ToString();

            transform.gameObject.SetActive(false);
        }

        private void InitButtons(SceneLoader sceneLoader)
        {
            _goToMenuButton.onClick.AddListener(() =>
            {
                sceneLoader.LoadWithLoadingScene(SceneLoader.Scene.Menu);
            });

            _exitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        private void ShowLostMenu(OnGameEndedSignal signal)
        {
            float duraction = 1f;

            transform.gameObject.SetActive(true);


            _movableRoot.DOAnchorPosY(0, duraction)
                .SetEase(Ease.Linear)
                .Play();

            _soundHandler.PlaySound(ClipName.Lost);

            _coinBalanceUI.IncreaseCoinBalace(_levelConfigurationSO.GetLostCoins());

            _saveData.CoinCurrency = _coinBalanceUI.GetCoinBalance();
            _saveSystem.Save(_saveData);
        }
    }
}
