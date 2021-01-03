using UnityEngine;
using UnityEngine.UI;
using Ether;

public class SetCarrierFromSlider : MonoBehaviour
{
    public Slider slider;
    public Carrier carrier;
    void Update()
    {
        carrier.value = slider.value;
    }
}
