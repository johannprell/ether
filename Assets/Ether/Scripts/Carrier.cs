using UnityEngine;

namespace Ether
{
    /**
     * A Carrier is a signal variable that is broadcast to the Ether.
     * A Receiver can read Carrier elsewhere, identified by SignatureToken.
     * A Modulator can make changes to Carrier.
     * An Effector can apply an effect when a Carrier signal is at a certain value, or crosses a certain threshold.
     */
    public class Carrier : MonoBehaviour, ICarrier
    {
        public SignatureToken signatureToken;
        [Range(0f, 1f)]
        public float value;

        public string Signature => signatureToken.name;
        public float Value => Mathf.Clamp(value, 0f, 1f);

        private void Start()
        {
            if (!signatureToken) return;
            Ether.RegisterCarrier(this);
        }
        
        private void OnDisable()
        {
            Ether.UnregisterCarrier(signatureToken.name);
        }
    }
}
