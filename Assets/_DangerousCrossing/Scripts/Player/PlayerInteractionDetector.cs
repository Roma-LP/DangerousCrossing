using System;
using System.Collections.Generic;
using _DangerousCrossing.Scripts.Enemy;
using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerInteractionDetector : MonoBehaviour
    {
        private readonly HashSet<EnemyPerson> _detectedEnemies = new();
    
        public int EnemyCountDetected => _detectedEnemies.Count;
        public event Action<int> OnEnemyDetected; 

        private void OnTriggerEnter(Collider other)
        {
            GameObject otherGameObject = other.gameObject;
            
            if (otherGameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        
            if (otherGameObject.TryGetComponent(out EnemyPerson enemyPerson))
            {
                HandleEnemyEnter(enemyPerson);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            GameObject otherGameObject = other.gameObject;
        
            if (otherGameObject.TryGetComponent(out EnemyPerson enemyPerson))
            {
                HandleEnemyExit(enemyPerson);
            }
        }

        private void HandleEnemyEnter(EnemyPerson enemyPerson)
        {
            if (_detectedEnemies.Add(enemyPerson))
            {
                OnEnemyDetected?.Invoke(EnemyCountDetected);
            }
        }

        private void HandleEnemyExit(EnemyPerson enemyPerson)
        {
            if (_detectedEnemies.Remove(enemyPerson))
            {
                OnEnemyDetected?.Invoke(EnemyCountDetected);
            }
        }
        
        private void OnDestroy()
        {
            _detectedEnemies.Clear();
            OnEnemyDetected = null;
        }
    }
}