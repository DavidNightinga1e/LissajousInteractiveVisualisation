using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lissajous
{
    [RequireComponent(typeof(LissajousParamSlider))]
    public class LissajousParamSlider : MonoBehaviour
    {
        public LissajousParamType lissajousParamType;
        
        private Slider _slider;
        private TextMeshProUGUI _valueText;

        public event Action<(LissajousParamType param, float value)> ValueChanged;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _valueText = GetComponentInChildren<TextMeshProUGUI>();

            _slider.onValueChanged.AddListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float v)
        {
            ValueChanged?.Invoke((lissajousParamType, v));
            _valueText.text = $"{v:0.00}";
            if (lissajousParamType is LissajousParamType.Phi || lissajousParamType is LissajousParamType.Psi)
                _valueText.text += "π";
        }

        public void SetRange(Vector2 range)
        {
            _slider.minValue = range.x;
            _slider.maxValue = range.y;
        }

        public void SetValue(float value)
        {
            _slider.value = value;
        }
    }
}