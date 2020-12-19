using System.Collections.Generic;
using UnityEngine;
// using Spicy.Log;

namespace Ether
{
    public class CarrierStream : MonoBehaviour
    {
        private static CarrierStream _instance;
        private static Dictionary<string, StreamedCarrier> _carriers;

        private void Awake()
        {
            if (!_instance)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }
            _carriers = new Dictionary<string, StreamedCarrier>();
        }

        public static float Read(string signature)
        {
            return _carriers.ContainsKey(signature) ? _carriers[signature].value : 0f;
        }

        public static float Read(SignatureToken signature)
        {
            return Read(signature.name);
        }

        public static void RegisterCarrier(StreamedCarrier carrier)
        {
            if (carrier.signatureToken == null)
            {
                // TODO new logging implementation
                // SpicyLog.Error("Can't register streamed carrier signal without Signature Token");
            }
            else if (_carriers.ContainsKey(carrier.signatureToken.name))
            {
                // SpicyLog.Error("Already registered carrier signal for signature: " + carrier.signatureToken.name);
            }
            else
            {
                _carriers.Add(carrier.signatureToken.name, carrier);
            }
        }

        public static void UnregisterCarrier(string signature)
        {
            if (_carriers[signature] == null)
            {
                // SpicyLog.Error("Can't unregister Current, none found for Signature: " + signature);
            }
            else
            {
                _carriers.Remove(signature);
            }
        }
    }
}
