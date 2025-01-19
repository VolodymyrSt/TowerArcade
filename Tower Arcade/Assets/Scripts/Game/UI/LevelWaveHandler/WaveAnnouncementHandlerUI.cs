using TMPro;
using UnityEngine;

namespace Game
{
    public class WaveAnnouncementHandlerUI : MonoBehaviour
    {
        private const string WAVE_ANNOUNCE = "WaveAnnounce";

        [SerializeField] private GameObject _waveAnnouncementRoot;
        [SerializeField] private TextMeshProUGUI _waveAnnouncementText;

        private LevelSystemSO _levelSystem;
        private Animator _animator;

        public void Initialize(LevelSystemSO levelSystem, EventBus eventBus)
        {
            _levelSystem = levelSystem;

            _animator = GetComponentInChildren<Animator>();

            eventBus.SubscribeEvent<OnLevelSystemStartedSignal>(ShowWaveAnnouncement);
            eventBus.SubscribeEvent<OnWaveEndedSignal>(ShowCurrentWaveAnnouncement);

            _waveAnnouncementRoot.SetActive(false);
        }

        private void ShowWaveAnnouncement(OnLevelSystemStartedSignal signal)
        {
            ChangeWaveAnnouncementConfiguration(1); //level start with wave number 1
        }

        private void ShowCurrentWaveAnnouncement(OnWaveEndedSignal signal)
        {
            ChangeWaveAnnouncementConfiguration(signal.CurrentWaveIndex);
        }

        private void ChangeWaveAnnouncementConfiguration(int currentWaveIndex)
        {
            _waveAnnouncementRoot.SetActive(true);
            _waveAnnouncementText.text = $"Wave {currentWaveIndex}";
            _animator.SetTrigger(WAVE_ANNOUNCE);
        }

        private void DisableWaveAnnouncement() //animation event
        {
            _waveAnnouncementRoot.SetActive(false);
            _animator.ResetTrigger(WAVE_ANNOUNCE);
        }
    }
}
