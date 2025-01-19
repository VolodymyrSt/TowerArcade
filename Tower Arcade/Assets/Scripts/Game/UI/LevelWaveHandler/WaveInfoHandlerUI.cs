using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class WaveInfoHandlerUI : MonoBehaviour
    {
        private const string WAVE_INFO_ACTIVATE = "WainInfoActivate";

        [SerializeField] private GameObject _waveInfoRoot;

        [SerializeField] private TextMeshProUGUI _waveInfoText;
        [SerializeField] private TextMeshProUGUI _timerToNextWaveText;
        [SerializeField] private Button _skipWaveButton;

        private LevelSystemSO _levelSystem;
        private Animator _animator;

        public void Initialize(LevelSystemSO levelSystem, EventBus eventBus)
        {
            _levelSystem = levelSystem;

            _animator = GetComponent<Animator>();

            eventBus.SubscribeEvent<OnLevelSystemStartedSignal>(ShowWaveInfo);
            eventBus.SubscribeEvent<OnWaveEndedSignal>(ShowCurrentWaveInfo);
            eventBus.SubscribeEvent<OnSkipWaveButtonShowedSignal>(ShowSkipWaveButton);
            eventBus.SubscribeEvent<OnSkipWaveButtonHidSignal>(HideSkipWaveButton);

            _skipWaveButton.onClick.AddListener(() =>
            {
                eventBus.Invoke(new OnWaveSkippedSignal());
            });

            _waveInfoRoot.SetActive(false);
            _skipWaveButton.gameObject.SetActive(false);
        }

        private void Update()
        {
            _timerToNextWaveText.text = _levelSystem.GetCurrentTimeToNextWave().ToString($"0:00");
        }

        private void ShowWaveInfo(OnLevelSystemStartedSignal signal)
        {
            _waveInfoRoot.SetActive(true);

            ChangeWaveInfoConfiguration(1); //level start with wave number 1

            _animator.SetTrigger(WAVE_INFO_ACTIVATE);
        }

        private void ShowCurrentWaveInfo(OnWaveEndedSignal signal)
        {
            ChangeWaveInfoConfiguration(signal.CurrentWaveIndex);
        }

        private void ChangeWaveInfoConfiguration(int currentWaveIndex)
        {
            _waveInfoText.text = $"Wave {currentWaveIndex}";
            _timerToNextWaveText.text = _levelSystem.GetCurrentTimeToNextWave().ToString($"0:00");
        }

        private void ShowSkipWaveButton(OnSkipWaveButtonShowedSignal signal)
        {
            _skipWaveButton.gameObject.SetActive(true);
        }

        private void HideSkipWaveButton(OnSkipWaveButtonHidSignal signal)
        {
            _skipWaveButton.gameObject.SetActive(false);
        }
    }
}
