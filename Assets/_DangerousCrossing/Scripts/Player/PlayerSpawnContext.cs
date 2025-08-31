using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerSpawnContext : MonoBehaviour
    {
        [SerializeField] private PlayerPerson playerPerson;
        [SerializeField] private Transform _spawnPoint;
        
        private PlayerPerson _playerPersonInstance;
        
        public PlayerPerson PlayerPersonInstance  => _playerPersonInstance;

        public void Init(IInputReader input, Transform cameraTransform)
        {
            _playerPersonInstance = Instantiate(playerPerson, _spawnPoint.position, Quaternion.identity);
            _playerPersonInstance.Init(input, cameraTransform);
            
            //ResetPlayerOnStartPoint();
        }

        public void ResetPlayerOnStartPoint()
        {
            _playerPersonInstance.transform.position = _spawnPoint.position;
            _playerPersonInstance.OnSpawnedPlayer();
        }
    }
}