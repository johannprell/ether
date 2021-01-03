using System;
using UnityEngine;

namespace Ether
{
    /***
     * Implements IAutomated to enable sine wave to be responsive to incoming carrier signal.
     */
    public class ModulatorSineAutomated : MonoBehaviour, IModulator, IAutomated
    {
        [Header("Sine")]
        public float amplitude = 1f;
        public float frequency = 1f;
        
        [Header("Automation")]
        public float amplitudeAutomationFactor;
        public float frequencyAutomationFactor;

        private float _frequency;
        
        private float _baseAmplitude;
        private float _baseFrequency;
        
        private float _phase = 0.0f;

        private const float Tau = 2f * Mathf.PI;
        private const float Tolerance = 0.01f;

        private void Start()
        {
            _baseAmplitude = amplitude;
            _baseFrequency = frequency;
        }

        private void Update()
        {
            if (Math.Abs(frequency - _frequency) > Tolerance)
            {
                CalculateNewFreq();
            }
        }
        
        public float GetOutput(float input)
        {
            return Mathf.Sin(Time.time * _frequency + _phase) * amplitude + input;
        }

        public void Automate(float value)
        {
            amplitude = _baseAmplitude + (value * amplitudeAutomationFactor);
            frequency = _baseFrequency + (value * frequencyAutomationFactor);
        }
        
        private void CalculateNewFreq() {
            var curr = (Time.time * _frequency + _phase) % Tau;
            var next = (Time.time * frequency) % Tau;
            _phase = curr - next;
            _frequency = frequency;
        }
    }    
}
