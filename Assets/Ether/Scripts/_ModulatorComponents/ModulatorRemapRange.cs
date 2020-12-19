using UnityEngine;

namespace Ether
{
    public class ModulatorRemapRange : MonoBehaviour, IModulator
    {
        public Vector2 rangeA;
        public Vector2 rangeB;
        
        public float GetOutput(float input)
        {
           return rangeB.x + (input-rangeA.x)*(rangeB.y-rangeB.x)/(rangeA.y-rangeA.x);
        }
    }
}
