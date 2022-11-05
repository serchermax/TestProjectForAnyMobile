using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class WarriorsContainer : MonoBehaviour
    {
        [Header("Required Links")]
        [SerializeField] private WarriorsSpawner _warriorsSpawner;

        private List<WarriorCore> _warriorsList;

        private void Start()
        {
            _warriorsList = new List<WarriorCore>();
            _warriorsSpawner.OnWarriorSpawned += OnWarriorSpawned;
        }
        private void OnDestroy() => _warriorsSpawner.OnWarriorSpawned -= OnWarriorSpawned;

        public bool TryGetFreeWarrior(WarriorCore origin, out WarriorCore warrior)
        {
            warrior = null;

            for (int i = 0; i < _warriorsList.Count; i++)
            {
                if (origin != _warriorsList[i])
                {
                    if (_warriorsList[i].Target == origin || !_warriorsList[i].isAttack)
                    {
                        warrior = _warriorsList[i];
                        return true;
                    }
                }
            }
            return false;
        }

        public bool TryGetFreeWarriors(WarriorCore origin, out WarriorCore[] warriors)
        {
            Stack<WarriorCore> frees = new Stack<WarriorCore>();

            for (int i = 0; i < _warriorsList.Count; i++)
            {
                if (origin != _warriorsList[i])
                {
                    if (_warriorsList[i].Target == origin || !_warriorsList[i].isAttack)
                    {
                        frees.Push(_warriorsList[i]);
                    }
                }
            }
            warriors = frees.ToArray();
            return warriors.Length > 0;
        }

        public bool TryGetFreeNearestWarrior(WarriorCore origin, out WarriorCore warrior)
        {
            warrior = null;
            float distance;
            float nearestDistance;

            if (TryGetFreeWarriors(origin, out WarriorCore[] warriors))
            {
                warrior = warriors[0];
                nearestDistance = Vector3.Distance(origin.transform.position, warrior.transform.position);

                for (int i = 1; i < warriors.Length; i++)
                {
                    distance = Vector3.Distance(origin.transform.position, warriors[i].transform.position);

                    if (distance < nearestDistance)
                    {
                        warrior = warriors[i];
                        nearestDistance = distance;
                    }
                }               
            }

            return warrior != null;
        }

        private void OnWarriorSpawned(WarriorCore warrior)
        {
            _warriorsList.Add(warrior);
            warrior.OnBackToPool += () => RemoveWarriorFromList(warrior);
        }
        private void RemoveWarriorFromList(WarriorCore warrior)
        {
            if (_warriorsList.Contains(warrior)) _warriorsList.Remove(warrior);
        }
    }
}