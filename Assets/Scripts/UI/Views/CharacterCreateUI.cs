using System;
using Character.Data;
using Spawner.Data;
using UI.Interfaces;
using UniRx;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class CharacterCreateUI : BaseUI
    {
        [SerializeField] private Button _start;
        [SerializeField] private SliderWithIndicator _powerSlider;
        [SerializeField] private SliderWithIndicator _enduranceSlider;
        [SerializeField] private SliderWithIndicator _intelligenceSlider;
        [SerializeField] private SliderWithIndicator _speedSlider;

        private EntityManager _entityManager;
        private IUIService _uiService;

        [Inject]
        private void Construct(EntityManager entityManager, IUIService uiService)
        {
            _entityManager = entityManager;
            _uiService = uiService;
        }

        private void Start()
        {
            _start
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    var entity = _entityManager.CreateEntity(typeof(CharacterSpawnRequest));
                    _entityManager.SetComponentData(entity, new CharacterSpawnRequest() {IsLocalPlayer = true});
                    Dispose();
                })
                .AddTo(this);

            _powerSlider
                .OnValueChangedAsObservable()
                .Subscribe(value =>
                {

                })
                .AddTo(this);
        }
    }
}