using Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LevelSettingHandlerUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _openSettingMenuButton;
        [SerializeField] private Button _closeSettingMenuButton;
        [SerializeField] private Button _goToMenuButton;

        [Header("Sliders")]
        [SerializeField] private Slider _soundSlider;
        [SerializeField] private Slider _mouseSensivitySlider;

        [Header("Root")]
        [SerializeField] private GameObject _settingMenuRoot;

        private SceneLoader _sceneLoader;
        private TimeHandler _timeHandler;
        private SoundHandler _soundHandler;

        private void Awake()
        {
            _sceneLoader = LevelDI.Resolve<SceneLoader>();
            _timeHandler = LevelDI.Resolve<TimeHandler>();
            _soundHandler = LevelDI.Resolve<SoundHandler>();
        }
        private void Start()
        {
            InitButtons(_sceneLoader, _timeHandler, _soundHandler);
            HideSettingMenu();
        }

        public void InitSliders(float maxSensivity, float maxVoluem)
        {
            _mouseSensivitySlider.maxValue = maxSensivity;
            _soundSlider.maxValue = maxVoluem;
        }
        
        private void InitButtons(SceneLoader sceneLoader, TimeHandler timeHandler, SoundHandler soundHandler)
        {
            _openSettingMenuButton.gameObject.SetActive(true);
            _closeSettingMenuButton.gameObject.SetActive(true);
            _goToMenuButton.gameObject.SetActive(true);

            _openSettingMenuButton.onClick.AddListener(() =>
            {
                ShowSettingMenu();
                HideOpenSettingMenuButton();

                soundHandler.PlaySound(ClipName.Click);

                timeHandler.StopTime();
            });

            _closeSettingMenuButton.onClick.AddListener(() =>
            {
                HideSettingMenu();
                ShowOpenSettingMenuButton();

                timeHandler.ResumeTime();

                soundHandler.PlaySound(ClipName.Click);
            });

            _goToMenuButton.onClick.AddListener(() => {
                sceneLoader.LoadWithLoadingScene(SceneLoader.Scene.Menu);
                timeHandler.ResumeTime();
            });

        }

        private void ShowSettingMenu() => _settingMenuRoot.SetActive(true);
        private void HideSettingMenu() => _settingMenuRoot.SetActive(false);

        private void ShowOpenSettingMenuButton() => _openSettingMenuButton.gameObject.SetActive(true);
        private void HideOpenSettingMenuButton() => _openSettingMenuButton.gameObject.SetActive(false);

        public void SetSoundSliderValue(float value) => _soundSlider.value = value;
        public void SetMouseSensivitySliderValue(float value) => _mouseSensivitySlider.value = value;

        public float GetSoundSliderValue() => _soundSlider.value;
        public float GetMouseSensivitySliderValue() => _mouseSensivitySlider.value;
    }
}
