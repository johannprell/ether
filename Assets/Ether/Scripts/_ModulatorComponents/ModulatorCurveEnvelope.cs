using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ether
{
    public class ModulatorCurveEnvelope : MonoBehaviour, IModulator, IPlayed
    {
        [Header("State")]
        public bool isPlaying;
        [Range(0f, 1f)]
        public float delta;
        
        [Header("Config")]
        public PlaybackMode playbackMode;
        public bool doPlayAtStart;
        public AnimationCurve curve;
        public float duration = 1f;
        public float amplitude = 1f;
        
        private float _startTime;
        private float _value;
        private bool _isReverse;

        private Dictionary<PlaybackMode, Func<float, float>> _getValueByMode;

        private void Start()
        {
            if(doPlayAtStart) Play();
            _getValueByMode = new Dictionary<PlaybackMode, Func<float, float>>
            {
                {PlaybackMode.OneShot, GetOneShotOutput},
                {PlaybackMode.OneShotReverse, GetOneShotReverseOutput},
                {PlaybackMode.Loop, GetLoopedOutput},
                {PlaybackMode.PingPong, GetPingPongOutput}
            };
        }

        public float GetOutput(float input)
        {
            return isPlaying ? _getValueByMode[playbackMode](input) : input;
        }

        private float GetOneShotOutput(float input)
        {
            delta = GetDelta();
            var value = curve.Evaluate(delta) * amplitude + input;
            if (delta >= 1f) isPlaying = false;
            return value;
        }

        private float GetOneShotReverseOutput(float input)
        {
            delta = 1f - GetDelta();
            var value = curve.Evaluate(delta) * amplitude + input;
            if (delta <= 0f) isPlaying = false;
            return value;
        }

        private float GetLoopedOutput(float input)
        {
            var value = GetOneShotOutput(input);
            if (!isPlaying) Play();
            return value;
        }

        private float GetPingPongOutput(float input)
        {
            var value = _isReverse ? GetOneShotReverseOutput(input) : GetOneShotOutput(input);
            if (!isPlaying)
            {
                _isReverse = !_isReverse;
                Play();
            }
            return value;
        }
                
        public void Play()
        {
            isPlaying = true;
            _startTime = Time.time;
        }
        
        // Delta is a 0 <-> 1 value representing progress between start and stop
        private float GetDelta()
        {
            var elapsed = Time.time - _startTime;
            return elapsed / duration;
        }
        
        public enum PlaybackMode
        {
            OneShot,
            OneShotReverse,
            Loop,
            PingPong
        }
    }
}
