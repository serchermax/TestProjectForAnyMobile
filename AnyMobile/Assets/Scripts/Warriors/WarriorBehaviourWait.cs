using UnityEngine;

namespace Gameplay.Behaviours
{
    public class WarriorBehaviourWait : IWarriorBehaviour
    {
        private float _waitTime = 1f;
        private float _timer;

        private WarriorBehaviourController _warriorBehaviourController;
        private IMove _move;

        public WarriorBehaviourWait(WarriorBehaviourController warriorBehaviourController, IMove move)
        {
            _warriorBehaviourController = warriorBehaviourController;
            _move = move;
        }

        public void Enter()
        {
            _move.Stop();
        }

        public void Exit() { _timer = 0; }

        public void Update()
        {
            if (_timer < _waitTime) _timer += Time.deltaTime;
            else
            {
                _warriorBehaviourController.SetBehaviourFindTarget();
            }
        }
    }
}