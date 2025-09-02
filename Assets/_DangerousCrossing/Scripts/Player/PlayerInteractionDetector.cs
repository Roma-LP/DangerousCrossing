using System;
using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerInteractionDetector : MonoBehaviour
    {
        public event Action OnEnemyDetected;

        private void OnTriggerEnter(Collider other)
        {
            GameObject otherGameObject = other.gameObject;

            if (otherGameObject.TryGetComponent(out IDamageable damageable))
            {
                if (damageable.CurrentHealth == 0)
                    return;
               
                OnEnemyDetected?.Invoke();
            }
            else
            {
                if (otherGameObject.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }
}