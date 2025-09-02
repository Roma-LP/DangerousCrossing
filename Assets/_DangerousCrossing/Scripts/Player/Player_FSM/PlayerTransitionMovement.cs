using System.Collections;
using _DangerousCrossing.Scripts.Interfaces;
using _DangerousCrossing.Scripts.StateMachineCore;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Player.Player_FSM
{
    public class PlayerTransitionMovement : FSMTransition
    {
        [SerializeField] private PlayerPerson _playerPerson;
        [SerializeField] private int _maxScanTargets = 5;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _scanInterval = 1f; 
        [ShowInInspector, ReadOnly] private float _scanRadius;
        
        private Collider[] _scanBuffer;
        private Coroutine _coroutineAttack;
        private WaitForSeconds _waitScanInterval;
        
        private void Start()
        {
            _scanRadius = _playerPerson.PlayerPersonConfig.AttackDistance;
            _waitScanInterval =  new WaitForSeconds(_scanInterval);
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();

            _scanBuffer = new Collider[_maxScanTargets];
            if (_coroutineAttack == null)
                _coroutineAttack = StartCoroutine(nameof(TryAttack));
        }

        private void OnDisable()
        {
            StopCoroutine(_coroutineAttack);
            _coroutineAttack = null;
        }
        
        private IEnumerator TryAttack()
        {
            while (true)
            {
                if (IsIDamageableNear() == false)
                {
                    SetNeedTransit();
                }
                
                yield return _waitScanInterval;
            }
        }

        private bool IsIDamageableNear()
        {
            int hits = Physics.OverlapSphereNonAlloc(transform.position, _scanRadius, _scanBuffer, _targetLayer);

            for (int i = 0; i < hits; i++)
            {
                if (_scanBuffer[i].TryGetComponent(out IDamageable iDamageable))
                {
                    if (iDamageable.CurrentHealth > 0)
                        return true;
                }
            }

            return false;
        }
    }
}