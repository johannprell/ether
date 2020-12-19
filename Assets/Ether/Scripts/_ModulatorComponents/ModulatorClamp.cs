using UnityEngine;

namespace Ether
{
    public class ModulatorClamp : MonoBehaviour, IModulator
    {
        public float min = 0f;
        public float max = 1f;

        public float GetOutput(float input)
        {
            return Mathf.Clamp(input, min, max);
        }
    }
}
