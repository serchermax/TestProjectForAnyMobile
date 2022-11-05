using UnityEngine;

namespace Gameplay.Behaviours
{
    public class WarriorBehaviourMove : IWarriorBehaviour
    {
        private WarriorBehaviourController _warriorBehaviourController;
        private ITargetContainer<WarriorCore> _targetContainer;
        private WarriorCore _warriorCore;
        private IMove _move;
        private IRotate _rotate;

        private WarriorCore _target;

        public WarriorBehaviourMove(WarriorBehaviourController warriorBehaviourController
            , WarriorCore warriorCore, IMove move, IRotate rotate, ITargetContainer<WarriorCore> targetContainer)
        {
            _warriorBehaviourController = warriorBehaviourController;
            _warriorCore = warriorCore;
            _move = move; 
            _rotate = rotate;
            _targetContainer = targetContainer;
        }

        public void Enter()
        {
            if (_targetContainer.Target == null) Return();
            else _target = _targetContainer.Target;
        }

        public void Exit()
        {
            _move.Stop();
            _target = null;
        }

        public void Update()
        {
            if (_target == null) Return();
            else 
            {
                if (_target.isAttack)
                    if (_target.Target != _warriorCore)
                    {                       
                        Return();
                        return;
                    }

                if (Vector3.Distance(_warriorCore.transform.position, _target.transform.position) > _warriorCore.AttackDistantion + _target.Size)
                {
                    _move.MoveForward();
                    _rotate.RotateTo(_target.transform.position);
                }
                else _warriorBehaviourController.SetBehaviourAttack();                
            }
        }

        private void Return() => _warriorBehaviourController.SetBehaviourFindTarget();
    }
}