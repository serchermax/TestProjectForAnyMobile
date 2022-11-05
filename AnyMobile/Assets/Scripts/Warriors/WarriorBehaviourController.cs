using System;
using System.Collections.Generic;
using UnityEngine;
using Gameplay.Behaviours;

namespace Gameplay
{
    public class WarriorBehaviourController 
    {
        private Dictionary<Type, IWarriorBehaviour> behaviourMap;
        private IWarriorBehaviour behaviourCurrent;

        private IMove _move;
        private IRotate _rotate;
        private WarriorCore _warriorCore;
        private WarriorsContainer _warriorsContainer;

        public WarriorBehaviourController(WarriorCore warriorCore, WarriorsContainer warriorsContainer
            , IMove move, IRotate rotate)
        {
            _move = move;
            _rotate = rotate;
            _warriorCore = warriorCore;
            _warriorsContainer = warriorsContainer;
            Initialize();
        }

        private void Initialize()
        {
            InitializeBehaviours();
            SetBehaviourByDefault();
        }

        private void InitializeBehaviours()
        {
            behaviourMap = new Dictionary<Type, IWarriorBehaviour>();

            behaviourMap[typeof(WarriorBehaviourWait)]
                = new WarriorBehaviourWait(this, _move);

            behaviourMap[typeof(WarriorBehaviourFindTarget)]
                = new WarriorBehaviourFindTarget(this, _warriorsContainer, _warriorCore);
            
            behaviourMap[typeof(WarriorBehaviourMove)]
                = new WarriorBehaviourMove(this, _warriorCore, _move, _rotate, GetBehaviour<WarriorBehaviourFindTarget>());

            behaviourMap[typeof(WarriorBehaviourAttack)]
                = new WarriorBehaviourAttack(this, _warriorCore, _rotate, GetBehaviour<WarriorBehaviourFindTarget>());
        }

        private void SetBehaviour(IWarriorBehaviour newBahaviour)
        {
            if (behaviourCurrent != null) behaviourCurrent.Exit();

            behaviourCurrent = newBahaviour;
            behaviourCurrent.Enter();
        }

        public T GetBehaviour<T>() where T : IWarriorBehaviour
        {
            return (T)behaviourMap[typeof(T)];
        }

        public void Update()
        {
            if (behaviourCurrent != null) behaviourCurrent.Update();
        }

        public bool TrySetBehaviourByType<T>() where T : IWarriorBehaviour
        {
            if (behaviourMap.ContainsKey(typeof(T))) SetBehaviour(behaviourMap[typeof(T)]);
            else
            {
                Debug.LogError("WarriorBehaviourController in " + this + " can't set Behaviour of Type " + typeof(T));
                return false;
            }
            return true;
        }
        public void SetBehaviourByDefault() => SetBehaviourWait();
        public void SetBehaviourWait() => SetBehaviour(GetBehaviour<WarriorBehaviourWait>());
        public void SetBehaviourFindTarget() => SetBehaviour(GetBehaviour<WarriorBehaviourFindTarget>());
        public void SetBehaviourMove() => SetBehaviour(GetBehaviour<WarriorBehaviourMove>());
        public void SetBehaviourAttack() => SetBehaviour(GetBehaviour<WarriorBehaviourAttack>());
    }
}