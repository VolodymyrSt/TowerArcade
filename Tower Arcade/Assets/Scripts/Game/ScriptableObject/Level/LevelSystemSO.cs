using DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LevelSystem", menuName = "Scriptable Object/LevelSystemConfig")]
    public class LevelSystemSO : ScriptableObject, IUpdatable
    {
        public List<WaveSO> Waves = new();

        public float MaxTimeToNextWave;
        private float _currentTimeToNextWave;

        private bool _isSkipButtonPressed;
        private bool _isGameEnded;

        private bool _isLastWave = false;

        private Transform _enemyContainer;

        //Dependencies
        private EventBus _eventBus;
        private HealthBarHandlerUI _healthBarHandler;
        private LevelCurencyHandler _levelCurencyHandler;

        public IEnumerator StartLevelSystem(DIContainer container, CoroutineUsager coroutine, Transform parent, Vector3 destination)
        {
            _eventBus = container.Resolve<EventBus>();
            _healthBarHandler = container.Resolve<HealthBarHandlerUI>();
            _levelCurencyHandler = container.Resolve<LevelCurencyHandler>();

            _eventBus.SubscribeEvent<OnWaveSkippedSignal>(SkipWave);
            _eventBus.SubscribeEvent<OnGameEndedSignal>(StopLevelSystem);

            _enemyContainer = parent;

            _currentTimeToNextWave = MaxTimeToNextWave;

            _isSkipButtonPressed = false;
            _isGameEnded = false;
            _isLastWave = false;

            for (int i = 0; i < Waves.Count; i++)
            {
                if (_isGameEnded) break;

                coroutine.StartCoroutine(Waves[i].StartWave(container, parent, destination));
                yield return new WaitUntil(() => Waves[i].IsWaveEnded());

                if (i == Waves.Count - 1) break;

                _eventBus.Invoke(new OnSkipWaveButtonShowedSignal());

                while (_currentTimeToNextWave > 0 && !_isSkipButtonPressed)
                {
                    _currentTimeToNextWave -= Time.deltaTime;
                    yield return null;
                }

                _levelCurencyHandler.AddCurrencyCount(Waves[i].AmountOfSoulsAfterFinish);

                _eventBus.Invoke(new OnSkipWaveButtonHidSignal());
                _eventBus.Invoke(new OnWaveEndedSignal(i + 2)); //to end up with wave 2

                _currentTimeToNextWave = MaxTimeToNextWave;
                _isSkipButtonPressed = false;
            }

            _isLastWave = true;
        }

        public void Tick()
        {
            if (_isLastWave)
            {
                if (_enemyContainer.childCount == 0 && _healthBarHandler.GetCurrentHealth() > 0)
                {
                    _eventBus.Invoke<OnGameWonSignal>(new OnGameWonSignal());
                    _isLastWave = false;
                }
            }
        }

        public int GetWavesCount() => Waves.Count;

        public float GetCurrentTimeToNextWave() => _currentTimeToNextWave;
        private void SkipWave(OnWaveSkippedSignal signal) => _isSkipButtonPressed = true;
        private void StopLevelSystem(OnGameEndedSignal signal)
        {
            _isGameEnded = true;

            _eventBus?.UnSubscribeEvent<OnWaveSkippedSignal>(SkipWave);
            _eventBus?.UnSubscribeEvent<OnGameEndedSignal>(StopLevelSystem);

            ClearAllEnemies();
        }

        private void ClearAllEnemies()
        {
            foreach (Transform child in _enemyContainer.transform)
            {
                if (child.TryGetComponent(out Enemy enemy))
                    enemy.DestroySelf();
            }
        }
    }
}
