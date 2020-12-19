using UnityEngine;

namespace Ether
{
    public class LocalCarrier : MonoBehaviour, ICarrier
    {
        [Range(0f, 1f)]
        public float value;

        public string Signature => gameObject.name;
        public float Value => Mathf.Clamp(value, 0f, 1f);
    }
}
