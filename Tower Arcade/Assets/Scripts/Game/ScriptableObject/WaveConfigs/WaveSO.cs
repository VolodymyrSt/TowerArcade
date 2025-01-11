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
        public List<EnemyData> enemyData = new List<EnemyData>();

        public float TimeBetweenEnemySpawn;

        private bool _isWaveEnded = false;
        public IEnumerator StartWave(DIContainer container, FactoryHandler factoryHandler, Vector3 startPosition, Vector3 destination)
        {
            yield return new WaitForSecondsRealtime(TimeBetweenEnemySpawn);

            for (int i = 0; i < enemyData.Count; i++)
            {
                for(int j = 0; j < enemyData[i].NumberOfEnemies; j++)
                {
                    factoryHandler.GetFactoryByType(container, enemyData[i].FactoryType).SpawnEnemy(startPosition, destination);
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
        public FactoryType FactoryType;
    }

}
