using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public interface IHealth
    {
        public float MaxHealth { get; }
        public float Health { get; }

        public void Damage(float damage);
        public void Heal(float heal);
    }

    public interface IHealthEvents
    {
        public event Action OnHealthChanged;
        public event Action OnDied;
    }
}