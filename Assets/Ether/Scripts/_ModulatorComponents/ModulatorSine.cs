using UnityEngine;

namespace Ether
{
    public class ModulatorSine : MonoBehaviour, IModulator
    {
        public float amplitude = 1f;
        public float frequency = 1f;

        private float _amplitude;
        private float _frequency;
        
        private void Start()
        {
            _amplitude = amplitude;
            _frequency = frequency;
        }
        
        public float GetOutput(float input)
        {
            return Mathf.Sin(Time.time * _frequency) * _amplitude + input;
        }
    }
}
