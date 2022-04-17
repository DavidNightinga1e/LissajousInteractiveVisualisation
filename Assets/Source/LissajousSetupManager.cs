using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Lissajous
{
    public class LissajousSetupManager : MonoBehaviour
    {
        [Serializable]
        public struct ParamTypeToSettings
        {
            public LissajousParamType Type;
            public Vector2 Range;
            public float DefaultValue;
        }
        
        [Header("References")]
        public List<LissajousParamSlider> sliders;
        public TrailController trailController;
        public Slider speedSlider;
        public TextMeshProUGUI speedSliderText;

        [Header("Settings")] public List<ParamTypeToSettings> settings;

        private Dictionary<LissajousParamType, ParamTypeToSettings> _typeToSettings;        

        private void Start()
        {
            speedSlider.maxValue = 0.1f;
            speedSlider.maxValue = 10f;
            speedSlider.onValueChanged.AddListener(f =>
            {
                trailController.SetSpeed(f);
                speedSliderText.text = $"{f:0.0}";
            });
            
            _typeToSettings = settings.ToDictionary(k => k.Type, v => v);
            
            foreach (var slider in sliders)
            {
                var s = _typeToSettings[slider.lissajousParamType];
                slider.SetRange(s.Range);
                slider.ValueChanged += tuple => trailController.SetValue(tuple.param, tuple.value);
                slider.SetValue(s.DefaultValue);
            }
        }
    }
}