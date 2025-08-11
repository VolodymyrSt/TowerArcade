using DI;
using Sound;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game
{
    public class LevelEntranceController : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Button _enterButton;
        [SerializeField] private TextMeshProUGUI _levelNumber;

        [SerializeField] private Image _lockImage;

        private bool _isLocked = true;

        private SaveData _saveData;
        private SaveSystem _saveSystem;

        private SoundHandler _menuSoundHandler;
        private MassegeHandlerUI _masegeHandler;

        public void Init(SceneLoader sceneLoader, SaveData saveData, SaveSystem saveSystem, SoundHandler menuSoundHandler, MassegeHandlerUI massegeHandler)
        {
            _saveData = saveData;
            _saveSystem = saveSystem;

            _menuSoundHandler = menuSoundHandler;
            _masegeHandler = massegeHandler;

            if (saveData.LevelEntances.TryGetValue(_levelNumber.text, out bool value))
            {
                _isLocked = value;
            }
            else
            {
                saveData.LevelEntances[_levelNumber.text] = _isLocked;
                saveSystem.Save(saveData);
            }

            if (!_isLocked)
            {
                UnlockLevel();
                ShowLockImage(false);
            }
            else
            {
                LockLevel();
                ShowLockImage(true);
            }

            _enterButton.onClick.AddListener(() => Enter(sceneLoader));
        }

        public void OnPointerEnter(PointerEventData eventData) => 
            _menuSoundHandler.PlaySound(ClipName.Selected);

        private void Enter(SceneLoader sceneLoader)
        {
            if (_isLocked)
            {
                _masegeHandler.ShowMassege("Open previous entrance first");
                return;
            }

            FindFirstObjectByType<GameEntryPoint>().GetRootContainer().RegisterInstance(this);

            sceneLoader.LoadWithLoadingScene($"Level {_levelNumber.text.ToString()}");
        }

        public void UnlockLevel()
        {
            _isLocked = false;

            _saveData.LevelEntances[_levelNumber.text] = false;
            _saveSystem.Save(_saveData);
        }

        public void ShowLockImage(bool value)
        {
            if (value)
                _lockImage.gameObject.SetActive(true);
            else
                _lockImage.gameObject.SetActive(false);
        }
        
        private void LockLevel() => _isLocked = true;

        public bool IsLocked() => _isLocked;

        public int GetEntranceIndex() => Int32.Parse(_levelNumber.text);
    }
}
