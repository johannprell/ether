using UnityEngine;

namespace Ether
{
    public class ModulatorAdd : MonoBehaviour, IModulator
    {
        public float amount;
        
        public float GetOutput(float input)
        {
            return amount + input;
        }
    }
}
