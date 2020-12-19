using UnityEngine;

namespace Ether
{
    public class EffectorPosition : MonoBehaviour, IEffector
    {
        public Vector3 amplitude;
        private Vector3 _defaultPosition;
        private Vector3 _effectedPosition;

        private void Start()
        {
            _defaultPosition = transform.localPosition;
            _effectedPosition = new Vector3();
        }

        public void PerformEffect(float value)
        {
            _effectedPosition.x = _defaultPosition.x + amplitude.x * value;
            _effectedPosition.y = _defaultPosition.y + amplitude.y * value;
            _effectedPosition.z = _defaultPosition.z + amplitude.z * value;
            transform.localPosition = _effectedPosition;
        }
    }
}
