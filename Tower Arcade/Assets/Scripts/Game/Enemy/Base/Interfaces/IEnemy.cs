using UnityEngine;

namespace Game {
    public interface IEnemy
    {
        public void Initialize();
        public void ApplyDamage(float damage, LevelCurencyHandler levelCurency);
        public void SetTargetDestination(Vector3 destination);
        public void SetStartPosition(Transform parent);
    }
}
