using Cinemachine;
using UnityEngine;

namespace _DangerousCrossing.Scripts.GameSystems
{
    public class CamerasContext : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cmPlayerVirtualCamera;
        
        public CinemachineVirtualCamera PlayerCamera => _cmPlayerVirtualCamera;

        public void SetFollowCamera(Transform followTarget)
        {
            _cmPlayerVirtualCamera.Follow = followTarget;
        }
    }
}