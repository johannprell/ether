using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ether
{
    public class CarrierReceiver : MonoBehaviour
    {
        [Header("Config")]
        public ReceiverMode mode;
        public SignatureToken signature;
        
        [Header("Value")]
        [Range(0f, 1f)]
        public float value;
        [Range(-10f, 10f)]
        public float modulatedValue;
        private float _modulatedValue;
        private const float ValueChangeTolerance = 0.001f;

        private LocalCarrier _localCarrier;
        private Func<float> _read;
        private List<IModulator> _modifiers;
        private List<IEffector> _effectors;

        void Start()
        {
            switch (mode)
            {
                case ReceiverMode.ReadFromStream:
                    _read = ReadFromStream;
                    break;
                case ReceiverMode.ReadLocal:
                    _localCarrier = GetComponent<LocalCarrier>();
                    _read = ReadLocal;
                    break;
                case ReceiverMode.ReadSelf:
                    _read = ReadSelf;
                    break;
                default:
                    break;
            }
            _modifiers = new List<IModulator>();
            GetComponents(_modifiers);
            _effectors = new List<IEffector>();
            GetComponents(_effectors);
        }

        void Update()
        {
            /**
             * Ether convention:
             * - Pre-modulated carrier value range 0 <-> 1
             * - Post-modulated value range -10 <-> 10
             * This way all effectors can work with an expected input range,
             * while modulators have reasonable flexibility beyond original carrier range.
             * Like analog signals can be over-driven for flavor, up to a certain point.
             */
            
            value = _read();
            modulatedValue = value;
            
            foreach (var modifier in _modifiers)
            {
                modulatedValue = modifier.GetOutput(modulatedValue);
            }
            modulatedValue = Mathf.Clamp(modulatedValue, -10f, 10f);
            
            /**
             * Only run effector loop if modulated value has changed within tolerance.
             * Implemented here so effectors don't need to implement this common optimisation.
             * Effectors that DO want to use input value continuously even when not changed can cache it
             * for use in Update() or similar.
             */
            if (Math.Abs(modulatedValue - _modulatedValue) < ValueChangeTolerance) return;
            
            foreach (var effector in _effectors)
            {
                effector.PerformEffect(modulatedValue);
            }
            
            _modulatedValue = modulatedValue;
        }

        private float ReadFromStream()
        {
            return CarrierStream.Read(signature);
        }

        private float ReadLocal()
        {
            return _localCarrier.value;
        }

        private float ReadSelf()
        {
            return value;
        }
    }

    public enum ReceiverMode
    {
        ReadFromStream,
        ReadLocal,
        ReadSelf
    }
}
