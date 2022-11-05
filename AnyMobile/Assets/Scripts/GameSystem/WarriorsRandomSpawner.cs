using UnityEngine;

namespace Gameplay
{
    public class WarriorsRandomSpawner : WarriorsSpawner
    {
        [Header("Warriors Random Parameters")]
        [SerializeField] private float _minHealth;
        [SerializeField] private float _maxHealth;
        [Space]
        [SerializeField] private float _minDamage;
        [SerializeField] private float _maxDamage;

        private float _randomHealth => Random.Range(_minHealth, _maxHealth + 1);
        private float _randomDamage => Random.Range(_minDamage, _maxDamage + 1);

        protected override WarriorCore CreateWarrior()
        {
            WarriorCore temp = _poolMono.GetObjectFromPool(out bool isNew);
            if (isNew) temp.FirstInitialize(_warriorsContainer);

            temp.ChangeParameters(_randomHealth, _randomDamage);

            temp.transform.position = _spawnZone.GetRandomPosition();
            return temp;
        }
    }
}