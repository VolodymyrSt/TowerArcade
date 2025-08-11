using DI;
using Sound;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class MenuSettingHandlerUI : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _openSettingMenuButton;
        [SerializeField] private Button _closeSettingMenuButton;
        [SerializeField] private Button _quitButton;

        [Header("Sliders")]
        [SerializeField] private Slider _soundSlider;

        [Header("Root")]
        [SerializeField] private GameObject _settingMenuRoot;

        private SceneLoader _sceneLoader;
        private SoundHandler _soundHandler;

        private void Awake()
        {
            _sceneLoader = MenuDI.Resolve<SceneLoader>();
            _soundHandler = MenuDI.Resolve<SoundHandler>();
        }

        private void Start()
        {
            InitButtons(_sceneLoader, _soundHandler);
            HideSettingMenu();
        }

        public void InitSliders(float maxVoluem) => 
            _soundSlider.maxValue = maxVoluem;

        private void InitButtons(SceneLoader sceneLoader, SoundHandler soundHandler)
        {
            _openSettingMenuButton.gameObject.SetActive(true);
            _closeSettingMenuButton.gameObject.SetActive(true);
            _quitButton.gameObject.SetActive(true);

            _openSettingMenuButton.onClick.AddListener(() =>
            {
                soundHandler.PlaySound(ClipName.Click);

                ShowSettingMenu();
                HideOpenSettingMenuButton();
            });

            _closeSettingMenuButton.onClick.AddListener(() =>
            {
                soundHandler.PlaySound(ClipName.Click);

                HideSettingMenu();
                ShowOpenSettingMenuButton();
            });

            _quitButton.onClick.AddListener(() => Application.Quit());
        }

        private void ShowSettingMenu() => 
            _settingMenuRoot.SetActive(true);
        private void HideSettingMenu() => 
            _settingMenuRoot.SetActive(false);

        private void ShowOpenSettingMenuButton() => 
            _openSettingMenuButton.gameObject.SetActive(true);
        private void HideOpenSettingMenuButton() => 
            _openSettingMenuButton.gameObject.SetActive(false);

        public void SetSoundSliderValue(float value) => 
            _soundSlider.value = value;

        public float GetSoundSliderValue() =>
            _soundSlider.value;
    }
}
