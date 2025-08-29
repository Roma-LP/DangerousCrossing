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

        public void Init(IInputReader input)
        {
            SpawnPlayer();
            _playerPersonInstance.Init(input);
        }

        public void SpawnPlayer()
        {
            if (_playerPersonInstance == null)
            {
                _playerPersonInstance = Instantiate(playerPerson, _spawnPoint.position, Quaternion.identity);
            }
            
            _playerPersonInstance.transform.position = _spawnPoint.position;
            _playerPersonInstance.SpawnPlayer();
        }
    }
}