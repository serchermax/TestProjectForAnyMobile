using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace Gameplay.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Text _text;

        private IHealthEvents _healthEvents;
        private IHealth _health;

        public void Initialize(IHealthEvents healthEvents, IHealth health)
        {
            _healthEvents = healthEvents;
            _health = health;

            _healthBar.fillAmount = 1;
            _healthEvents.OnHealthChanged += UpdateHealthBar;
        }

        private void OnDestroy()
        {
            if (_healthEvents != null) _healthEvents.OnHealthChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            if (_health != null)
            {
                _healthBar.fillAmount = _health.Health / _health.MaxHealth;

                StringBuilder text = new StringBuilder();
                text.Append(_health.Health.ToString("0.0"));
                text.Append("/");
                text.Append(_health.MaxHealth.ToString("0.0"));
                _text.text = text.ToString();
            }
        }
    }
}