using Sound;
using TMPro;
using UnityEngine;

namespace Game
{
    public class WaveAnnouncementHandlerUI : MonoBehaviour
    {
        private const string WAVE_ANNOUNCE = "WaveAnnounce";

        [Header("UI")]
        [SerializeField] private GameObject _waveAnnouncementRoot;
        [SerializeField] private TextMeshProUGUI _waveAnnouncementText;

        private LevelSystemSO _levelSystem;
        private EventBus _eventBus;
        private LevelSoundHandler _soundHandler;
        private Animator _animator;

        private void Start()
        {
            _levelSystem = LevelRegistrator.Resolve<LevelSystemSO>();
            _eventBus = LevelRegistrator.Resolve<EventBus>();
            _soundHandler = LevelRegistrator.Resolve<LevelSoundHandler>();

            _animator = GetComponentInChildren<Animator>();

            _eventBus.SubscribeEvent<OnLevelSystemStartedSignal>(ShowWaveAnnouncement);
            _eventBus.SubscribeEvent<OnWaveEndedSignal>(ShowCurrentWaveAnnouncement);
            _eventBus.SubscribeEvent<OnGameEndedSignal>(EndGame);

            _waveAnnouncementRoot.SetActive(false);
        }

        private void ShowWaveAnnouncement(OnLevelSystemStartedSignal signal)
        {
            _soundHandler.PlaySound(ClipName.WaveAnnounce);

            ChangeWaveAnnouncementConfiguration(1); //level start with wave number 1
        }

        private void ShowCurrentWaveAnnouncement(OnWaveEndedSignal signal)
        {
            _soundHandler.PlaySound(ClipName.WaveAnnounce);

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

        private void EndGame(OnGameEndedSignal signal)
        {
            _eventBus.UnSubscribeEvent<OnWaveEndedSignal>(ShowCurrentWaveAnnouncement);
        }
    }
}
