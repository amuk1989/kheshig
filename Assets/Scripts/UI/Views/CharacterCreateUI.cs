﻿using System;
using Character.Data;
using Spawner.Data;
using UI.Interfaces;
using UniRx;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class CharacterCreateUI : BaseUI
    {
        [SerializeField] private Button _start;
        [SerializeField] private CharacterPointsSlider powerCharacterPointsSlider;
        [SerializeField] private CharacterPointsSlider enduranceCharacterPointsSlider;
        [SerializeField] private CharacterPointsSlider intelligenceCharacterPointsSlider;
        [SerializeField] private CharacterPointsSlider speedCharacterPointsSlider;

        private EntityManager _entityManager;
        private IUIService _uiService;
        private int _points = 10;

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
                    
                    _uiService.DestroyWindow<CharacterCreateUI>();
                })
                .AddTo(this);

            SettingSliders(enduranceCharacterPointsSlider);
            SettingSliders(intelligenceCharacterPointsSlider);
            SettingSliders(powerCharacterPointsSlider);
            SettingSliders(speedCharacterPointsSlider);
        }

        private float GetFreePointsCount()
        {
            return math.max(0, powerCharacterPointsSlider.Value + enduranceCharacterPointsSlider.Value + 
                               intelligenceCharacterPointsSlider.Value + speedCharacterPointsSlider.Value -
                               _points);
        }

        private void SettingSliders(CharacterPointsSlider characterPointsSlider)
        {
            characterPointsSlider
                .OnValueChangedAsObservable()
                .Subscribe(value => characterPointsSlider.SetValue(value - GetFreePointsCount()))
                .AddTo(this);
        }
    }
}