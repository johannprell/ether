using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ether
{
    /**
     * Functions much as a standard CarrierReceiver,
     * but value is used to drive components that implement IAutomated
     */
    public class AutomationCarrierReceiver : MonoBehaviour
    {
        [Header("Config")]
        public ReceiverMode mode;
        public SignatureToken signature;

        [Header("Value")] [Range(0f, 1f)]
        public float value;
        
        private LocalCarrier _localCarrier;
        private Func<float> _read;
        private List<IAutomated> _automationObjects;
        
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

            _automationObjects = new List<IAutomated>();
            GetComponents(_automationObjects);
        }

        void Update()
        {
            value = _read();
            foreach (var obj in _automationObjects)
            {
                obj.Automate(value);
            }
        }
        
        private float ReadFromStream()
        {
            return Ether.Read(signature);
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
}
