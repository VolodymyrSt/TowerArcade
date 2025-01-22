using DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Scriptable Object/WaveConfigs")]
    public class WaveSO : ScriptableObject
    {
        public List<EnemyData> EnemyConfiguration = new List<EnemyData>();

        public float TimeBetweenEnemySpawn;
        private bool _isWaveEnded;

        public IEnumerator StartWave(DIContainer container, Vector3 startPosition, Vector3 destination)
        {
            _isWaveEnded = false;

            yield return new WaitForSecondsRealtime(TimeBetweenEnemySpawn);

            for (int i = 0; i < EnemyConfiguration.Count; i++)
            {
                for(int j = 0; j < EnemyConfiguration[i].NumberOfEnemies; j++)
                {
                    EnemyFactoryHandler factoryHandler = container.Resolve<EnemyFactoryHandler>();
                    factoryHandler.GetEnemyFactoryByType(container, EnemyConfiguration[i].FactoryType).SpawnEnemy(startPosition, destination);
                    yield return new WaitForSecondsRealtime(TimeBetweenEnemySpawn);
                }
            }
            _isWaveEnded = true;
        }

        public bool IsWaveEnded() => _isWaveEnded;
    }

    [Serializable]
    public struct EnemyData { 
        public int NumberOfEnemies;
        public EnemyFactoryType FactoryType;
    }

}
