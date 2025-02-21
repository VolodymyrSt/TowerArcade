using UnityEngine;
using UnityEngine.UI;

namespace Game 
{
    public class HealthBarHandlerUI : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Slider _healthSlider;

        [Header("Propeties")]
        [SerializeField, Range(0, 100)] private float _maxHealth;
        private float _currentHealth;

        private EventBus _eventBus;

        private void Start()
        {
            _healthSlider.maxValue = _maxHealth;

            _currentHealth = _maxHealth;

            _eventBus = LevelDI.Resolve<EventBus>();
        }

        public void ChangeHealth(float value)
        {
            _currentHealth -= value;

            if (_currentHealth <= 0)
            {
                _eventBus.Invoke<OnGameEndedSignal>(new OnGameEndedSignal());
            }

            _healthSlider.value = _currentHealth;
        }

        public float GetCurrentHealth() => _currentHealth;
    }
}
