using System.Collections.Generic;
using UnityEngine;

namespace Ether
{
    public class TriggerAllPlayed : MonoBehaviour
    {
        private List<IPlayed> _allPlayed;
        
        private void Start()
        {
            _allPlayed = new List<IPlayed>();
            GetComponents(_allPlayed);
        }

        public void PlayAll()
        {
            foreach (var played in _allPlayed)
            {
                played.Play();
            }
        }
    }
}
