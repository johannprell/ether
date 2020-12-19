using UnityEngine;

namespace Ether
{
    public class ModulatorMapToCurve : MonoBehaviour, IModulator
    {
        public AnimationCurve curve;

        public float GetOutput(float input)
        {
            return curve.Evaluate(input);
        }
    }
}
