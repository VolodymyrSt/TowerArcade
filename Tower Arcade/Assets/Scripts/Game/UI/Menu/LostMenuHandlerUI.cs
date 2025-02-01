using DG.Tweening;
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

        private void Start()
        {
            SceneLoader sceneLoader = LevelRegistrator.Resolve<SceneLoader>();
            LevelConfigurationSO levelConfiguration = LevelRegistrator.Resolve<LevelConfigurationSO>();

            LevelRegistrator.Resolve<EventBus>().SubscribeEvent<OnGameEndedSignal>(ShowLostMenu);

            InitButtons(sceneLoader);

            _coinsAmountText.text = levelConfiguration.GetLostCoins().ToString();

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
        }
    }
}
