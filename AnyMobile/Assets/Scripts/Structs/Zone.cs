using UnityEngine;

namespace Gameplay
{
    [System.Serializable]
    public struct Zone
    {
        public Vector3 position;
        public Vector3 scale;
        public Vector3 GetRandomPosition()
        {
            return position
                + new Vector3(Random.Range(-scale.x / 2f, scale.x / 2f)
                , Random.Range(-scale.y / 2f, scale.y / 2f)
                , Random.Range(-scale.z / 2f, scale.z / 2f));
        }
    }
}