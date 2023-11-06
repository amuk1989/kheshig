using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class SliderWithIndicator: MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _text;

        public float Value => _slider.value;
        
        private void Start()
        {
            _slider
                .OnValueChangedAsObservable()
                .Subscribe(value => _text.text = value.ToString("F0"));
        }

        public void SetValue(float value)
        {
            _slider.value = value;
        }

        public IObservable<float> OnValueChangedAsObservable() => _slider.OnValueChangedAsObservable();
    }
}