using UnityEngine;

namespace Ether
{
    public class StreamedCarrier : MonoBehaviour, ICarrier
    {
        public SignatureToken signatureToken;
        [Range(0f, 1f)]
        public float value;

        public string Signature => signatureToken.name;
        public float Value => Mathf.Clamp(value, 0f, 1f);

        private void Start()
        {
            if (!signatureToken) return;
            CarrierStream.RegisterCarrier(this);
        }
        private void OnDisable()
        {
            CarrierStream.UnregisterCarrier(signatureToken.name);
        }
    }
}
