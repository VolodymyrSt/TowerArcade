using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelSystemActivatorUI : MonoBehaviour
    {
        private const string START_LEVEL_SYSTEM = "StartLevelSystem";

        [SerializeField] private GameObject _levelSystemActivatorRoot;

        [SerializeField] private Button _levelSystemActivatorButton;
        [SerializeField] private TextMeshProUGUI _levelDifficulty;
        [SerializeField] private TextMeshProUGUI _wavesCount;
        [SerializeField] private TextMeshProUGUI _levelDescription;

        private EventBus _eventBus;
        private Animator _animator;

        public void Initialize(LevelSystemSO levelSystem, EventBus eventBus)
        {
            _levelSystemActivatorRoot.gameObject.SetActive(true);

            _eventBus = eventBus;
            _animator = GetComponent<Animator>();

            LevelConfiguration(levelSystem);

            _levelSystemActivatorButton.onClick.AddListener(() => {
                TriggerHideAnimation();
            });
        }

        private void LevelConfiguration(LevelSystemSO levelSystem)
        {
            _levelDifficulty.text = $"Difficulty: {levelSystem.GetLevelDifficulty()}";
            _wavesCount.text = $"Waves: {levelSystem.GetWavesCount().ToString()}";
            _levelDescription.text = levelSystem.GetLevelDescrition();
        }

        private void TriggerHideAnimation()
        {
            _levelSystemActivatorButton.onClick.RemoveAllListeners();
            _animator.SetTrigger(START_LEVEL_SYSTEM);
        }

        private void HideRoot() //animation event
        {
            _eventBus.Invoke(new OnLevelSystemStartedSignal());
            _levelSystemActivatorRoot.gameObject.SetActive(false);
        }
    }
}
