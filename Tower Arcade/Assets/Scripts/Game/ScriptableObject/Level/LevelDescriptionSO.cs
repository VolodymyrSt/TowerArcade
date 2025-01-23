using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "LevelDescription", menuName = "Scriptable Object/LevelDescription")]
    public class LevelDescriptionSO : ScriptableObject
    {
        [Header("LevelsDescription")]
        public Difficulty LevelDifficulty;
        public string LevelDescription;

        public string GetLevelDifficulty() => LevelDifficulty.ToString();
        public string GetLevelDescrition() => LevelDescription;
    }

    public enum Difficulty
    {
        Simple, Normal, Middle, Hard, ExtraHard, Insane, Expert, God
    }
}
