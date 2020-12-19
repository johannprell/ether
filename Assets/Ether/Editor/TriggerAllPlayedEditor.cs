using UnityEngine;
using UnityEditor;

namespace Ether
{
    [CustomEditor(typeof(TriggerAllPlayed))]
    public class TriggerAllPlayedEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var trigger = target as TriggerAllPlayed;
            if (GUILayout.Button("Play all"))
            {
                trigger.PlayAll();
            }
        }
    }
}
