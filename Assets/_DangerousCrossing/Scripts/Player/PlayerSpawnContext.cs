using _DangerousCrossing.Scripts.Interfaces;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player
{
    public class PlayerSpawnContext : MonoBehaviour
    {
        [SerializeField] private PlayerUnit _playerUnit;
        [SerializeField] private Transform _spawnPoint;
        
        private PlayerUnit _playerUnitInstance;
        
        public PlayerUnit PlayerUnitInstance  => _playerUnitInstance;

        public void Init(IInputReader input)
        {
            _playerUnitInstance = Instantiate(_playerUnit, _spawnPoint.position, Quaternion.identity);
            _playerUnitInstance.Init(input);
        }
    }
}