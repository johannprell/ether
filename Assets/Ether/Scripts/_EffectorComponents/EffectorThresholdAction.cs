using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ether
{
    public class EffectorThresholdAction : MonoBehaviour, IEffector
    {
        [Range(-10f, 10f)]
        public float inputValue; // Only for inspector readability, can be optimized out.
        [Range(-10f, 10f)]
        public float threshold;
        public ThresholdMode mode;
        public UnityEvent onCrossThreshold;
    
        private float _value;
        private const float ChangeTolerance = 0.001f;
        private const float ThresholdTolerance = 0.01f;
        private bool _isAboveThreshold;
        
        private Func<float, float> _performFunc;
        private Dictionary<ThresholdMode, Func<float, float>> _evaluateMode;
        
        void Start()
        {
            _evaluateMode = new Dictionary<ThresholdMode, Func<float, float>>
            {
                {ThresholdMode.CrossUp, EvaluateUpMode},
                {ThresholdMode.CrossDown, EvaluateDownMode},
                {ThresholdMode.Always, EvaluateAlwaysMode}
            };
            _performFunc = InitFunc;
        }

        public void PerformEffect(float value)
        {
            inputValue = value; // Only for inspector readability, can be optimized out.
            _value = _performFunc(value);
        }

        private float InitFunc(float value)
        {
            _isAboveThreshold = value >= threshold;
            _performFunc = EvaluateFunc;
            return value;
        }

        private float EvaluateFunc(float value)
        {
            return Math.Abs(_value - value) > ChangeTolerance ? _evaluateMode[mode](value) : value;
        }

        private float EvaluateUpMode(float value)
        {
            if (_isAboveThreshold)
            {
                _isAboveThreshold = IsAboveThresholdTolerance(value);
            }
            else if (IsAboveThresholdTolerance(value))
            {
                onCrossThreshold.Invoke();
                _isAboveThreshold = true;
            }
            return value;
        }

        private float EvaluateDownMode(float value)
        {
            if (!_isAboveThreshold)
            {
                _isAboveThreshold = !IsBelowThresholdTolerance(value);
            }
            else if (IsBelowThresholdTolerance(value))
            {
                onCrossThreshold.Invoke();
                _isAboveThreshold = false;
            }
            return value;
        }

        private float EvaluateAlwaysMode(float value)
        {
            var isCrossing = false;
            if (!_isAboveThreshold && IsAboveThresholdTolerance(value))
            {
                isCrossing = true;
                _isAboveThreshold = true;
            } 
            else if (_isAboveThreshold && IsBelowThresholdTolerance(value))
            {
                isCrossing = true;
                _isAboveThreshold = false;
            }
            if(isCrossing) onCrossThreshold.Invoke();
            return value;
        }

        private bool IsAboveThresholdTolerance(float value)
        {
            return value >= threshold - ThresholdTolerance;
        }

        private bool IsBelowThresholdTolerance(float value)
        {
            return value <= threshold + ThresholdTolerance;
        }
        

        public enum ThresholdMode
        {
            CrossUp,
            CrossDown,
            Always
        }
    }
}
