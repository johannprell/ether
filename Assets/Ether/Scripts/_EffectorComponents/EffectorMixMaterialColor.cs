using UnityEngine;

namespace Ether
{
    public class EffectorMixMaterialColor : MonoBehaviour, IEffector
    {
        public Material material;
        public Color colorA;
        public Color colorB;
        [SerializeField]
        private Color outputColor;
    
        private void Start()
        {
            outputColor = new Color();
        }
        
        public void PerformEffect(float value)
        {
            outputColor = Color.Lerp(colorA, colorB, value);
            material.color = outputColor;
        }
    }
}
