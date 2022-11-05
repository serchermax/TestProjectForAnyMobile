using UnityEngine;

namespace Gameplay
{
    public interface IMove
    {
        public void MoveForward();
        public void MoveForward(float speed);
        public void MoveTo(Vector3 target);
        public void MoveTo(Vector3 target, float speed);
        public void Stop();
    }
}
