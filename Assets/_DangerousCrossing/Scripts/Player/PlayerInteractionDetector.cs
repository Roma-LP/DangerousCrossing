using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerInteractionDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
}