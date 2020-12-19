using UnityEngine;

namespace Ether
{
    /**
     * Acts like a Carrier, but is not broadcast to Ether.
     * A Receiver can be set to read a LocalCarrier that is attached to same GameObject.
     */
    public class LocalCarrier : MonoBehaviour, ICarrier
    {
        [Range(0f, 1f)]
        public float value;

        public string Signature => gameObject.name;
        public float Value => Mathf.Clamp(value, 0f, 1f);
    }
}
