using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DangerousCrossing.Scripts.UI
{
    public class CanvasResizer : MonoBehaviour
    {
        public event Action Risezed;
        public bool Resized => _resized;

        private bool _resized = false;
        private const float BaseCanvasHeight = 1920f;
        private const float BaseCanvasWidth = 1080f;
        private const float BaseScreenRatio = 16 / 9f;

        private void Start()
        {
            SetupCanvas();
        }

        private void SetupCanvas()
        {
            GetComponent<CanvasScaler>().referenceResolution = new Vector2(
                BaseCanvasWidth,
                BaseCanvasHeight * Mathf.Max(1f, Screen.height / (float)Screen.width / BaseScreenRatio));

            transform.DOScale(0.01f, 0);

            _resized = true;

            Risezed?.Invoke();
        }
    }
}