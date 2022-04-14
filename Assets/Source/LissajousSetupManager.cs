using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        [Header("Settings")] public List<ParamTypeToSettings> settings;

        private Dictionary<LissajousParamType, ParamTypeToSettings> TypeToSettings;        

        private void Start()
        {
            TypeToSettings = settings.ToDictionary(k => k.Type, v => v);
            
            foreach (var slider in sliders)
            {
                var settings = TypeToSettings[slider.lissajousParamType];
                slider.SetRange(settings.Range);
                slider.ValueChanged += tuple => trailController.SetValue(tuple.param, tuple.value);
                slider.SetValue(settings.DefaultValue);
            }
        }
    }
}