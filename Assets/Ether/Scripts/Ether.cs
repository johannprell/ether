using System.Collections.Generic;
using UnityEngine;
// using Spicy.Log;

namespace Ether
{
    /**
     * Global Ether class - home of all broadcast Carriers in Scene.
     */
    public class Ether : MonoBehaviour
    {
        private static Ether _instance;
        private static Dictionary<string, Carrier> _carriers;

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
            _carriers = new Dictionary<string, Carrier>();
        }

        private static float Read(string signature)
        {
            return _carriers.ContainsKey(signature) ? _carriers[signature].value : 0f;
        }

        public static float Read(SignatureToken signature)
        {
            return Read(signature.name);
        }

        public static void RegisterCarrier(Carrier carrier)
        {
            if (carrier.signatureToken == null)
            {
                // TODO new logging implementation
                // SpicyLog.Error("Can't register Carrier signal without Signature Token");
            }
            else if (_carriers.ContainsKey(carrier.signatureToken.name))
            {
                // SpicyLog.Error("Already registered Carrier signal for signature: " + carrier.signatureToken.name);
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
                // SpicyLog.Error("Can't unregister Carrier, none found for Signature: " + signature);
            }
            else
            {
                _carriers.Remove(signature);
            }
        }
    }
}
