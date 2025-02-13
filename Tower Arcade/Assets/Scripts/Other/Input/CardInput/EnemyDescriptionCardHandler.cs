using UnityEngine;

namespace Game
{
    public class EnemyDescriptionCardHandler : IUpdatable
    {
        private EnemyDescriptionCardUI _enemyCardUI;
        private Camera _camera;

        public EnemyDescriptionCardHandler(EnemyDescriptionCardUI descriptionCardUI)
        {
            _enemyCardUI = descriptionCardUI;
            _camera = Camera.main;
        }

        public void Tick() => FillEnemyCard();

        private void FillEnemyCard()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue)) {
                if (hit.transform.TryGetComponent(out IEnemyDescription enemyDescription)) {
                    _enemyCardUI.ShowCard();
                    Fill(enemyDescription);
                }
                else 
                {
                    _enemyCardUI.HideCard();
                }
            }
            else return;
        }

        private void Fill(IEnemyDescription enemyDescription)
        {
            _enemyCardUI.SetCardEnemyHealth(enemyDescription.GetCurrentHealth());
            _enemyCardUI.SetCardEnemyName(enemyDescription.GetEnemyName());
            _enemyCardUI.SetCardEnemyDescription(enemyDescription.GetEnemyDescription());
            _enemyCardUI.SetCardEnemyType(enemyDescription.GetEnemyType());
            _enemyCardUI.SetCardEnemyRank(enemyDescription.GetEnemyRank());
            _enemyCardUI.SetCardEnemySoulCost(enemyDescription.GetSoulCost());
        }
    }
}
