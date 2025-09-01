using System;
using DG.Tweening;
using UnityEngine;

namespace _DangerousCrossing.Scripts.Utilities
{
    public class ObjectFloatRotateAnimation : MonoBehaviour
    {
        [Header("Object for animation")]
        [SerializeField] private Transform _targetObject;

        [Header("Animation settings")]
        [SerializeField] private FloatAnimationSettings _floatSettings;
        [SerializeField] private RotationAnimationSettings _rotationSettings;

        [Header("Auto-start")]
        [SerializeField] private bool _isPlayOnStart = true;

        private Vector3 _originalPosition;
        private Sequence _animationSequence;

        private void Start()
        {
            if (_targetObject == null)
                _targetObject = transform;

            _originalPosition = _targetObject.position;

            if (_isPlayOnStart)
                PlayAnimation();
        }

        private bool IsSequenceExistAndActive()
        {
            return _animationSequence != null && _animationSequence.IsActive();
        }

        private void KillAnimation()
        {
            if (IsSequenceExistAndActive())
            {
                _animationSequence.Kill();
            }
        }

        private void OnDestroy()
        {
            StopAnimation();
        }

        public void PlayAnimation()
        {
            StopAnimation();

            _animationSequence = DOTween.Sequence();

            _animationSequence.Join(_targetObject
                .DOMoveY(_originalPosition.y + _floatSettings.floatHeight, _floatSettings.floatDuration)
                .SetEase(_floatSettings.floatEase)
                .SetLoops(_floatSettings.floatLoop ? -1 : 0, LoopType.Yoyo)
                .SetLink(gameObject));

            _animationSequence.Join(_targetObject
                .DORotate(_rotationSettings.rotationAxis * 360f, _rotationSettings.rotationDuration,
                    _rotationSettings.rotateMode)
                .SetEase(_rotationSettings.rotationEase)
                .SetLoops(_rotationSettings.rotationLoop ? -1 : 0, LoopType.Restart)
                .SetLink(gameObject));

            _animationSequence.Play();
        }

        public void StopAnimation()
        {
            KillAnimation();

            if (_targetObject != null)
            {
                _targetObject.position = _originalPosition;
            }
        }


        public void PauseAnimation()
        {
            if (IsSequenceExistAndActive())
            {
                _animationSequence.Pause();
            }
        }

        public void ResumeAnimation()
        {
            if (IsSequenceExistAndActive())
            {
                _animationSequence.Play();
            }
        }
    }

    [Serializable]
    public class FloatAnimationSettings
    {
        [Header("Floating")] public float floatHeight = 0.5f;
        public float floatDuration = 2f;
        public Ease floatEase = Ease.InOutSine;
        public bool floatLoop = true;
    }

    [Serializable]
    public class RotationAnimationSettings
    {
        [Header("Rotation")] public Vector3 rotationAxis = Vector3.up;
        public float rotationDuration = 5f;
        public Ease rotationEase = Ease.Linear;
        public bool rotationLoop = true;
        public RotateMode rotateMode = RotateMode.LocalAxisAdd;
    }
}