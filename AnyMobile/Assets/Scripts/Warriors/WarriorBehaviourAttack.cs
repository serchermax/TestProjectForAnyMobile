using UnityEngine;

namespace Gameplay.Behaviours
{
    public class WarriorBehaviourAttack : IWarriorBehaviour
    {
        public bool IsAttack { get; private set; }

        private WarriorBehaviourController _warriorBehaviourController;
        private ITargetContainer<WarriorCore> _targetContainer;
        private WarriorCore _warriorCore;
        private IRotate _rotate;

        private float _damage;
        private float _attackRate;

        private float _timer;

        private WarriorCore _target;

        public WarriorBehaviourAttack(WarriorBehaviourController warriorBehaviourController
                , WarriorCore warriorCore, IRotate rotate, ITargetContainer<WarriorCore> targetContainer)
        {
            _warriorBehaviourController = warriorBehaviourController;
            _warriorCore = warriorCore;
            _rotate = rotate;
            _targetContainer = targetContainer;

            ChangeParameters(warriorCore);
        }

        public void Enter()
        {
            if (_targetContainer.Target == null) Return();
            else
            {
                _target = _targetContainer.Target;
                IsAttack = true;
                _timer = 0;                
            }
        }

        public void Exit()
        {
            IsAttack = false;
            _target = null;
            _timer = 0;
        }

        public void Update()
        {
            if (_target == null || _target.Target != _warriorCore) Return();
            else
            {
                if (Vector3.Distance(_warriorCore.transform.position, _target.transform.position) <= _warriorCore.AttackDistantion + _target.Size)
                {
                    _rotate.RotateTo(_target.transform.position);
                    Damage();
                }
                else Return();
            }
        }

        public void ChangeParameters(WarriorCore warriorCore)
        {
            _damage = warriorCore.Damage;
            _attackRate = warriorCore.AttackRate;
        }

        private void Damage()
        {
            if (_timer < _attackRate) _timer += Time.deltaTime;
            else
            {
                _target.Health.Damage(_damage);
                _timer = 0;
            }
        }

        private void Return() => _warriorBehaviourController.SetBehaviourFindTarget();
    }
}