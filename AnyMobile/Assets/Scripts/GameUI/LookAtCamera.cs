using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class LookAtCamera : MonoBehaviour
    {
        private Transform _camera;
        private void Awake()
        {
            _camera = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(new Vector3(transform.position.x, _camera.position.y, _camera.position.z));
            transform.Rotate(0, -180, 0);
        }
    }
}
