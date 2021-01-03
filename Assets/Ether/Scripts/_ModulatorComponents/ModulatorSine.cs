using System;
using UnityEngine;

namespace Ether
{
    public class ModulatorSine : MonoBehaviour, IModulator
    {
        public float amplitude = 1f;
        public float frequency = 1f;

        private float _amplitude;
        private float _frequency;
        
        private float _phase = 0.0f;
        
        private const float Tau = 2f * Mathf.PI;
        private const float Tolerance = 0.01f;
        
        private void Start()
        {
            _amplitude = amplitude;
            _frequency = frequency;
        }

        private void Update()
        {
            if (Math.Abs(frequency - _frequency) > Tolerance)
            {
                CalculateNewFreq();
            }
        }
        
        private void CalculateNewFreq() {
            var curr = (Time.time * _frequency + _phase) % Tau;
            var next = (Time.time * frequency) % Tau;
            _phase = curr - next;
            _frequency = frequency;
        }
        
        public float GetOutput(float input)
        {
            return Mathf.Sin(Time.time * _frequency + _phase) * _amplitude + input;
        }
    }
}
