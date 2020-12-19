using UnityEngine;
using UnityEngine.UI;

namespace Ether.Demo
{
    public class SetCarrierValueFromSlider : MonoBehaviour
    {
        public Slider slider;
        public StreamedCarrier carrier;

        void Update()
        {
            carrier.value = slider.value;
        }
    }
}
