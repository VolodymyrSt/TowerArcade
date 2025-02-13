using DI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Scriptable Object/WaveConfigs")]
    public class WaveSO : ScriptableObject
    {
        public List<EnemyData> EnemyConfiguration = new List<EnemyData>();

        public float TimeBetweenEnemySpawn;

        private bool _isWaveEnded;
        private bool _isGameEnded;

        public int AmountOfSoulsAfterFinish;

        public IEnumerator StartWave(DIContainer container, Transform parent, Vector3 destination)
        {
            container.Resolve<EventBus>().SubscribeEvent<OnGameEndedSignal>(StopEnemySpawn);

            _isWaveEnded = false;
            _isGameEnded = false;

            yield return new WaitForSecondsRealtime(TimeBetweenEnemySpawn);

            for (int i = 0; i < EnemyConfiguration.Count; i++)
            {
                if (_isGameEnded) break;

                for(int j = 0; j < EnemyConfiguration[i].NumberOfEnemies; j++)
                {
                    if (_isGameEnded) break;

                    EnemyFactoryHandler factoryHandler = container.Resolve<EnemyFactoryHandler>();
                    factoryHandler.GetEnemyFactoryByType(container, EnemyConfiguration[i].FactoryType).SpawnEnemy(parent, destination);
                    yield return new WaitForSecondsRealtime(TimeBetweenEnemySpawn);
                }
            }
            _isWaveEnded = true;
        }

        private void StopEnemySpawn(OnGameEndedSignal signal) => _isGameEnded = true;

        public bool IsWaveEnded() => _isWaveEnded;
    }

    [Serializable]
    public struct EnemyData { 
        public int NumberOfEnemies;
        public EnemyFactoryType FactoryType;
    }
}
