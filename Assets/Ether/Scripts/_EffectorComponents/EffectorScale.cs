using UnityEngine;

namespace Ether
{
    public class EffectorScale : MonoBehaviour, IEffector
    {
        public Vector3 amplitude;
        private Vector3 _defaultScale;
        private Vector3 _effectedScale;

        private void Start()
        {
            _defaultScale = transform.localScale;
            _effectedScale = new Vector3();
        }

        public void PerformEffect(float value)
        {
            _effectedScale.x = _defaultScale.x + amplitude.x * value;
            _effectedScale.y = _defaultScale.y + amplitude.y * value;
            _effectedScale.z = _defaultScale.z + amplitude.z * value;
            transform.localScale = _effectedScale;
        }
    }
}
