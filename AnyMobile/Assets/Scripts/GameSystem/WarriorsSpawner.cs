using UnityEngine;
using System;

namespace Gameplay
{
    public class WarriorsSpawner : MonoBehaviour
    {
        [Header("Required Links")]
        [SerializeField] private WarriorCore _warriorPrefab;
        [SerializeField] protected WarriorsContainer _warriorsContainer;

        [Header("Settings")]
        [SerializeField] protected Zone _spawnZone;

        [Header("Debug")]
        [SerializeField] private bool _debug;

        protected PoolMono<WarriorCore> _poolMono;

        public event Action<WarriorCore> OnWarriorSpawned;

        private void Start()
        {
            _warriorsContainer = _warriorsContainer ? _warriorsContainer : FindObjectOfType<WarriorsContainer>();
            _poolMono = new PoolMono<WarriorCore>(_warriorPrefab, 0, transform);
        }

        public void SpawnWarrior()
        {
            WarriorCore temp = CreateWarrior();
            OnWarriorSpawned?.Invoke(temp);
        }

        protected virtual WarriorCore CreateWarrior()
        {
            WarriorCore temp = _poolMono.GetObjectFromPool(out bool isNew);
            if (isNew) temp.FirstInitialize(_warriorsContainer);
            temp.transform.position = _spawnZone.GetRandomPosition();
            return temp;
        }

        #region Debug
        private void OnDrawGizmosSelected()
        {
            if (!_debug) return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(_spawnZone.position, _spawnZone.scale);
        }
        #endregion
    }
}