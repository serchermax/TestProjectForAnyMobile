using UnityEngine;
using Gameplay.Behaviours;
using Gameplay.UI;

namespace Gameplay
{
    [RequireComponent(typeof(MoveModule))]
    public class WarriorCore : PoolObject
    {           
        [Header("Warrior Parameters")]
        [SerializeField] private float _health;
        [SerializeField] private float _damage;
        [SerializeField] private float _attackRate;
        [SerializeField] private float _attackDistantion;
        [SerializeField] private float _objectSize;

        [Header("Links")]
        [SerializeField] private HealthBar _healthBar;

        [Header("Debug")]
        [SerializeField] private bool _debug;

        private MoveModule _moveModule;     
        private WarriorsContainer _warriorsContainer;
        private WarriorHealth _warriorHealth;

        public WarriorCore Target => WarriorBehaviourController.GetBehaviour<WarriorBehaviourFindTarget>().Target;
        public bool isAttack => WarriorBehaviourController.GetBehaviour<WarriorBehaviourAttack>().IsAttack;

        public IHealth Health => _warriorHealth;
        public IHealthEvents HealthEvents => _warriorHealth;

        public float Damage => _damage;
        public float AttackRate => _attackRate;
        public float AttackDistantion => _attackDistantion;
        public float Size => _objectSize;

        public WarriorBehaviourController WarriorBehaviourController { get; private set; }

        public void FirstInitialize(WarriorsContainer warriorsManager)
        {
            _moveModule = GetComponent<MoveModule>();
            _warriorsContainer = warriorsManager;

            WarriorBehaviourController = new WarriorBehaviourController(this, _warriorsContainer, _moveModule, _moveModule);
            _warriorHealth = new WarriorHealth(_health);

            if (_healthBar) _healthBar.Initialize(_warriorHealth, _warriorHealth);
            _warriorHealth.OnDied += BackToPool;
            OnTackedFromPool += ReInitialize;
        }

        private void OnDestroy()
        {
            if (_warriorHealth != null) _warriorHealth.OnDied -= BackToPool;
            OnTackedFromPool -= ReInitialize;
        }

        private void ReInitialize()
        {
            _warriorHealth.Initialize(_health);
        }

        private void Update()
        {
            if (WarriorBehaviourController != null) WarriorBehaviourController.Update();
        }

        public void ChangeParameters(float newHealth, float newDamage)
        {
            _health = newHealth;
            _damage = newDamage;

            _warriorHealth.Initialize(_health);
            WarriorBehaviourController.GetBehaviour<WarriorBehaviourAttack>().ChangeParameters(this);
        }

        #region Debug
        private void OnDrawGizmos()
        {
            if (!_debug) return;
            Vector3 pos;

            Gizmos.color = Color.red;
            pos = transform.position;
            pos.y += 0.5f;
            Gizmos.DrawWireSphere(pos, _attackDistantion);

            Gizmos.color = Color.blue;
            pos = transform.position;
            pos.y += 0.5f;
            Gizmos.DrawWireSphere(pos, _objectSize);
        }
        #endregion
    }
}