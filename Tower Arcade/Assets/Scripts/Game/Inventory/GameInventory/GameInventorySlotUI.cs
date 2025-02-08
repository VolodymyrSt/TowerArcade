using DG.Tweening;
using DI;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Game
{
    public class GameInventorySlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("UI")]
        [SerializeField] private RectTransform _slotRoot;
        [Space(10f)]
        [SerializeField] private TextMeshProUGUI _towerName;
        [SerializeField] private Image _towerImage;
        [SerializeField] private TextMeshProUGUI _towerSoulCost;

        [Header("Background")]
        [SerializeField] private Image _backgraundImage;
        [SerializeField] private Sprite _selectedBackgraundSprite;
        [SerializeField] private Sprite _unselectedBackgraundSprite;

        //Dependencies
        private DIContainer _container;
        private TowerPlacementBlocksHolder _towerPlacementBlocksHolder;
        private TowerFactoryHandler _towerFactoryHandler;
        private LevelCurencyHandler _levelCurencyHandler;
        private GameInventoryHandler _gameInventoryHandler;
        private TowerDescriptionCardHandler _towerDescriptionCardHandler;
        private EffectPerformer _effectPerformer;
        private MassegeHandlerUI _masegeHandler;
        private SoundHandler _soundHandler;

        private TowerSO _tower;
        private Camera _camera;

        public void InitializeSlot(DIContainer container, TowerSO tower, GameInventoryHandler gameInventoryHandler)
        {
            _camera = Camera.main;

            _container = container;
            _gameInventoryHandler = gameInventoryHandler;
            _tower = tower;

            _towerFactoryHandler = container.Resolve<TowerFactoryHandler>();
            _towerPlacementBlocksHolder = container.Resolve<TowerPlacementBlocksHolder>();
            _levelCurencyHandler = container.Resolve<LevelCurencyHandler>();
            _towerDescriptionCardHandler = container.Resolve<TowerDescriptionCardHandler>();
            _effectPerformer = container.Resolve<EffectPerformer>();
            _masegeHandler = container.Resolve<MassegeHandlerUI>();
            _soundHandler = container.Resolve<SoundHandler>();

            _towerName.text = _tower.TowerName;
            _towerImage.sprite = _tower.TowerSprite;
            _towerSoulCost.text = _tower.SoulCost.ToString();

            UnSelect();
        }

        private void Update()
        {
            if (_gameInventoryHandler.IsSlotActive(this) && Mouse.current.leftButton.isPressed)
            {
                HandleTowerPlacement();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _soundHandler.PlaySound(ClipName.SoftClick);

            if (_gameInventoryHandler.IsSlotActive(this))
            {
                _gameInventoryHandler.ClearActiveSlot();
            }
            else
            {
                if (_levelCurencyHandler.GetCurrentCurrencyCount() >= _tower.SoulCost)
                {
                    _gameInventoryHandler.SetActiveSlot(this);
                }
                else
                {
                    _masegeHandler.ShowMassege("Dont have enough soul");
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _slotRoot.DOScale(1.1f, 0.2f)
                .SetEase(Ease.Linear)
                .Play();

            _soundHandler.PlaySound(ClipName.Selected);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _slotRoot.DOScale(1f, 0.2f)
                .SetEase(Ease.InBack)
                .Play();
        }

        private void HandleTowerPlacement()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue))
            {
                if (hitInfo.transform.TryGetComponent(out TowerPlacementBlock towerPlacementBlock))
                {
                    if (towerPlacementBlock.IsOccupied()) 
                        return;

                    PlaceTowerOnBlock(towerPlacementBlock, _levelCurencyHandler, _towerDescriptionCardHandler);
                }
                else
                {
                    _gameInventoryHandler.ClearActiveSlot();
                }
            }
        }
        private void PlaceTowerOnBlock(TowerPlacementBlock towerPlacementBlock, LevelCurencyHandler levelCurencyHandler,TowerDescriptionCardHandler towerDescriptionCardHandler)
        {
            _towerFactoryHandler.GetTowerFactoryByType(_container, _tower.TowerType)
                        .SpawnTower(towerPlacementBlock.GetPlacePivot(), towerPlacementBlock, levelCurencyHandler, towerDescriptionCardHandler);

            _effectPerformer.PlayTowerInstalledEffect(towerPlacementBlock.GetPlacePivot().position);

            _gameInventoryHandler.ClearActiveSlot();

            towerPlacementBlock.SetOccupied(true);

            _levelCurencyHandler.SubtactCurrencyCount(_tower.SoulCost);

            _soundHandler.PlaySound(ClipName.SoftClick);
        }

        public void SetActive(bool isActive)
        {
            if (isActive)
            {
                _towerPlacementBlocksHolder.TuggleHighlight();
            }
            else
            {
                _towerPlacementBlocksHolder.UnTuggleHighlight();
            }
        }

        public void Select() => _backgraundImage.sprite = _selectedBackgraundSprite;
        public void UnSelect() => _backgraundImage.sprite = _unselectedBackgraundSprite;
    }
}
