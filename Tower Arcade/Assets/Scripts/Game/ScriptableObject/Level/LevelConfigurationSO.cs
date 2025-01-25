using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LevelConfiguration", menuName = "Scriptable Object/LevelConfiguration")]
    public class LevelConfigurationSO : ScriptableObject
    {
        [Header("LevelsDescription")]
        public Difficulty LevelDifficulty;
        public string LevelDescription;

        [Header("Soul")]
        public float StartedCurrencyCount;

        public string GetLevelDifficulty() => LevelDifficulty.ToString();
        public string GetLevelDescrition() => LevelDescription;
    }

    public enum Difficulty
    {
        Simple, Normal, Middle, Hard, ExtraHard, Insane, Expert, God
    }
}
