using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _DangerousCrossing.Scripts.Person
{
    public class HealthBarUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fillImage;
        [SerializeField] private Canvas _rootCanvas;

        [Header("Colors")]
        [SerializeField] private Color _fullHealthColor = Color.green;
        [SerializeField] private Color _lowHealthColor = Color.red;

        [Header("Animation")]
        [SerializeField] private float _updateDuration = 0.3f;
        
        private Transform _cameraTransform;

        public void Init(Transform cameraTransform)
        {
            _cameraTransform = cameraTransform;
            UpdateColor(1f, 0);
        }

        public void OnLateUpdate()
        {
            _rootCanvas.transform.rotation = _cameraTransform.rotation;
        }

        public void SetHealth(float currentHealth, float maxHealth, bool isNeedUpdateDuration = true)
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            float normalized = currentHealth / maxHealth;
            
            float updateDuration = isNeedUpdateDuration ? _updateDuration : 0;
            
            _slider.DOValue(normalized, updateDuration).SetEase(Ease.OutQuad).SetLink(gameObject);
            
            UpdateColor(normalized, updateDuration);
        }

        private void UpdateColor(float normalizedValue, float updateDuration)
        {
            Color targetColor = Color.Lerp(_lowHealthColor, _fullHealthColor, normalizedValue);
            _fillImage.DOColor(targetColor, updateDuration).SetEase(Ease.OutQuad).SetLink(gameObject);;
        }
    }
}