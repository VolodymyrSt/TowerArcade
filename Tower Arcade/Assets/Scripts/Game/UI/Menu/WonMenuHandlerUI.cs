using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class WonMenuHandlerUI : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField] private RectTransform _movableRoot;

        [Header("Buttons")]
        [SerializeField] private Button _goToMenuButton;
        [SerializeField] private Button _exitButton;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _coinsAmountText;
        
        [Header("Stars")]
        [SerializeField] private RectTransform _starRoot;

        private HealthBarHandlerUI _healthBarHandler;

        private void Start()
        {
            _healthBarHandler = LevelRegistrator.Resolve<HealthBarHandlerUI>();
            SceneLoader sceneLoader = LevelRegistrator.Resolve<SceneLoader>();
            TimeHandler timeHandler = LevelRegistrator.Resolve<TimeHandler>();
            LevelConfigurationSO levelConfiguration = LevelRegistrator.Resolve<LevelConfigurationSO>();

            LevelRegistrator.Resolve<EventBus>().SubscribeEvent<OnGameWonSignal>(ShowWonMenu);

            InitButtons(sceneLoader, timeHandler);

            _coinsAmountText.text = levelConfiguration.GetVictoryCoins().ToString();

            transform.gameObject.SetActive(false);
        }

        private void InitButtons(SceneLoader sceneLoader, TimeHandler timeHandler)
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

        private void ShowWonMenu(OnGameWonSignal signal)
        {
            float duraction = 1f;

            transform.gameObject.SetActive(true);

            CountFilledStars();

            _movableRoot.DOAnchorPosY(0, duraction)
                .SetEase(Ease.Linear)
                .Play();
        }

        private void CountFilledStars()
        {
            var stars = GetStars();

            if (_healthBarHandler.GetCurrentHealth() >= 90f)
            {
                FillStars(stars, 0);
            }
            else if (_healthBarHandler.GetCurrentHealth() <= 75f && _healthBarHandler.GetCurrentHealth() >= 40f)
            {
                FillStars(stars, 1);
            }
            else
            {
                FillStars(stars, 2);
            }
        }

        private void FillStars(List<StarSwitcher> stars, int countOfUnfilledStars)
        {
            for (int i = 0; i < stars.Count - countOfUnfilledStars; i++)
            {
                stars[i].SetStar(true);
            }
        }

        private List<StarSwitcher> GetStars()
        {
            List <StarSwitcher> starList = new List<StarSwitcher>();

            foreach (Transform star in _starRoot)
            {
                if (star.TryGetComponent(out StarSwitcher starSwitcher))
                {
                    starList.Add(starSwitcher);
                }
            }

            return starList;
        }
    }
}
