using UnityEngine;

namespace Ether
{
    /**
     * Different from EffectorPosition - effects continuous movement
     * that reacts to both carrier input and automation.
     */
    public class EffectorMovement : MonoBehaviour, IEffector
    {
        [Header("Config")]
        public Vector3 direction;
        public float speedFactor = 1f;

        private float _carrierValue;
        private Transform _t;

        private Vector3 _currentPosition;
        
        private void Start()
        {
            direction.Normalize();
            _currentPosition = new Vector3();
            _t = transform;
        }

        private void Update()
        {
            _currentPosition = _t.position;
            _currentPosition += direction * (_carrierValue * speedFactor * Time.deltaTime * 10f);
            _t.position = _currentPosition;
        }

        public void PerformEffect(float value)
        {
            _carrierValue = value;
        }
    }
}
