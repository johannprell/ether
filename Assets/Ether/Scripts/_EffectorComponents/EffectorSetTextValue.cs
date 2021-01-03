using UnityEngine;
using UnityEngine.UI;
using Ether;

public class EffectorSetTextValue : MonoBehaviour, IEffector
{
    public Text target;

    public void PerformEffect(float value)
    {
        target.text = value.ToString("0.00");
    }
}
