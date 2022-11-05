using System;

namespace Gameplay
{
    public class WarriorHealth : IHealth, IHealthEvents
    {
        public WarriorHealth(float health)
        {
            Initialize(health);
        }

        public void Initialize(float health)
        {
            Health = health;
            MaxHealth = health;
            OnHealthChanged?.Invoke();
        }

        public event Action OnHealthChanged;
        public event Action OnDied;

        public float MaxHealth { get; private set; }
        public float Health { get; private set; }

 
        public void Damage(float damage)
        {
            Health -= damage;
            OnHealthChanged?.Invoke();

            if (Health <= 0)
            {
                OnDied?.Invoke();
            }
        }

        public void Heal(float heal)
        {
            Health += heal;
            OnHealthChanged?.Invoke();

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
    }
}