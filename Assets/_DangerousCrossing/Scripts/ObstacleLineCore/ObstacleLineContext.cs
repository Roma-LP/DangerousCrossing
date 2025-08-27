using System.Collections.Generic;
using UnityEngine;

namespace _DangerousCrossing.Scripts.ObstacleLineCore
{
    public class ObstacleLineContext : MonoBehaviour
    {
        [SerializeField] private List<ObstacleLineReferencesAndSettings> _obstacleLineReferencesAndSettings;

        private ObstacleLineSpawner[] _spawners; 

        public void Init()
        {
            _spawners = new ObstacleLineSpawner[_obstacleLineReferencesAndSettings.Count];
            
            for (var i = 0; i < _obstacleLineReferencesAndSettings.Count; i++)
            {
                _spawners[i] = new ObstacleLineSpawner(_obstacleLineReferencesAndSettings[i]);
            }
        }

        public void Launch()
        {
            for (var i = 0; i < _spawners.Length; i++)
            {
                _spawners[i].LaunchLine();
            }
        }
    }
}