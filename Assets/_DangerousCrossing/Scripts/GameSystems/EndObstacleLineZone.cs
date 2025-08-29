using System;
using _DangerousCrossing.Scripts.Player;
using UnityEngine;

namespace _DangerousCrossing.Scripts.GameSystems
{
    [RequireComponent(typeof(BoxCollider))]
    public class EndObstacleLineZone : MonoBehaviour
    {
        public event Action OnPlayerInEndZone;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerPerson playerPerson))
            {
                OnPlayerInEndZone?.Invoke();
            }
        }
    }
}