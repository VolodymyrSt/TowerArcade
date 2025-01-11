using DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "WaveSpawnerSystem", menuName = "Scriptable Object/WaveSpawnerSystem")]
    public class WaveSpawnerSystemSO : ScriptableObject
    {
        public List<WaveSO> Waves = new List<WaveSO>();

        public float MaxTimeToNextWave;
        private float _currentTimeToNextWave;

        public IEnumerator StartWaveSpawnerSystem(DIContainer container, FactoryHandler factoryHandler, CoroutineUsager coroutine, Vector3 startPosition, Vector3 destination)
        {
            for (int i = 0; i < Waves.Count; i++)
            {
                _currentTimeToNextWave = MaxTimeToNextWave;

                coroutine.StartCoroutine(Waves[i].StartWave(container, factoryHandler, startPosition, destination));
                yield return new WaitUntil(() => Waves[i].IsWaveEnded() == true);

                while(_currentTimeToNextWave > 0)
                {
                    _currentTimeToNextWave -= Time.deltaTime;
                    yield return null;
                }

                _currentTimeToNextWave = MaxTimeToNextWave;
            }
        }
    }
}
