using UnityEngine;

namespace Gameplay
{
    [RequireComponent(typeof(Rigidbody))]
    public class MoveModule : MonoBehaviour, IMove, IRotate
    {
        [Header("Move Parameters")]
        [SerializeField] private float _defaultSpeed;
        [SerializeField] private float _rotateSpeed;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void MoveForward() => MoveTo(transform.position + transform.forward);
        public void MoveForward(float speed) => MoveTo(transform.position + transform.forward, speed);

        public void MoveTo(Vector3 target) => MoveTo(target, _defaultSpeed);
        public void MoveTo(Vector3 target, float speed)
        {
            Vector3 direction = (target - transform.position).normalized;
            _rigidbody.AddForce(direction * Time.deltaTime * speed, ForceMode.Force);
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
        }
        public void RotateTo(Vector3 target)
        {
            Quaternion unitRot = Quaternion.LookRotation(target - transform.position);
            _rigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, unitRot, Time.deltaTime * _rotateSpeed));
        }        
    }
}
