using DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LevelSystem", menuName = "Scriptable Object/LevelSystemConfig")]
    public class LevelSystemSO : ScriptableObject
    {
        public List<WaveSO> Waves = new List<WaveSO>();

        public float MaxTimeToNextWave;
        private float _currentTimeToNextWave;

        private bool _isLevelActive;
        private bool _isSkipButtonPressed;

        private Transform _enemyContainer;
        private EventBus _eventBus;

        public IEnumerator StartLevelSystem(DIContainer container, CoroutineUsager coroutine, Transform parent, Vector3 destination)
        {
            _eventBus = container.Resolve<EventBus>();

            _eventBus.SubscribeEvent<OnWaveSkippedSignal>(SkipWave);
            _eventBus.SubscribeEvent<OnGameEndedSignal>(StopLevelSystem);

            _enemyContainer = parent;

            _isLevelActive = true;

            _currentTimeToNextWave = MaxTimeToNextWave;
            _isSkipButtonPressed = false;

            for (int i = 0; i < Waves.Count; i++)
            {
                //if (!_isLevelActive) break;

                coroutine.StartCoroutine(Waves[i].StartWave(container, parent, destination));
                yield return new WaitUntil(() => Waves[i].IsWaveEnded());

                if (i == Waves.Count - 1) break;

                _eventBus.Invoke(new OnSkipWaveButtonShowedSignal());

                while (_currentTimeToNextWave > 0 && !_isSkipButtonPressed)
                {
                    _currentTimeToNextWave -= Time.deltaTime;
                    yield return null;
                }

                _eventBus.Invoke(new OnSkipWaveButtonHidSignal());
                _eventBus.Invoke(new OnWaveEndedSignal(i + 2)); //to end up with wave 2

                _currentTimeToNextWave = MaxTimeToNextWave;
                _isSkipButtonPressed = false;
            }
        }

        public int GetWavesCount() => Waves.Count;

        public float GetCurrentTimeToNextWave() => _currentTimeToNextWave;
        private void SkipWave(OnWaveSkippedSignal signal) => _isSkipButtonPressed = true;
        private void StopLevelSystem(OnGameEndedSignal signal)
        {
            _isLevelActive = false;

            ClearAllEnemies();
        }

        private void ClearAllEnemies()
        {
            foreach (Transform child in _enemyContainer.transform)
            {
                if (child.TryGetComponent(out Enemy enemy))
                {
                    enemy.DestroySelf();
                }
            }
        }
    }
}
