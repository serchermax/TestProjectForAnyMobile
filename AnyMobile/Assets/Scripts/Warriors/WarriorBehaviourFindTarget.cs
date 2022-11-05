namespace Gameplay.Behaviours
{
    public class WarriorBehaviourFindTarget : IWarriorBehaviour, ITargetContainer<WarriorCore>
    {
        public WarriorCore Target { get; private set; }

        private WarriorBehaviourController _warriorBehaviourController;
        private WarriorsContainer _warriorsContainer;
        private WarriorCore _warriorCore;

        public WarriorBehaviourFindTarget(WarriorBehaviourController warriorBehaviourController
            , WarriorsContainer warriorsContainer, WarriorCore warriorCore)
        {
            _warriorBehaviourController = warriorBehaviourController;
            _warriorsContainer = warriorsContainer;
            _warriorCore = warriorCore;
        }

        public void Enter()
        {
            ReleaseTarget();
            if (_warriorsContainer.TryGetFreeNearestWarrior(_warriorCore, out WarriorCore warrior))
            {
                Target = warrior;
                Target.OnBackToPool += ReleaseTarget;
                _warriorBehaviourController.SetBehaviourMove();                
            }
            else _warriorBehaviourController.SetBehaviourWait();
        }

        public void Exit() { }
        public void Update() { }

        public void ReleaseTarget()
        {
            if (!Target) return;

            Target.OnBackToPool -= ReleaseTarget;
            Target = null;
        }
    }
}