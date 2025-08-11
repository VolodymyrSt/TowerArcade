using UnityEngine;

namespace Game {
    public interface IEnemy
    {
        public void Initialize();
        public void ApplyDamage(float damage, LevelCurencyHandler levelCurencyHandler);
        public void SetTargetDestination(Vector3 destination);
        void WarpAgent(Vector3 warpPosition);
    }
}
