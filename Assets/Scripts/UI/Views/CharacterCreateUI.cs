using System;
using Character.Configs;
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
        private CharacterConfigData _characterConfig;

        [Inject]
        private void Construct(EntityManager entityManager, IUIService uiService, CharacterConfigData configData)
        {
            _entityManager = entityManager;
            _uiService = uiService;
            _characterConfig = configData;
        }

        private void Start()
        {
            _start
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    var spawnEntity = _entityManager.CreateEntity(typeof(CharacterSpawnRequest));
                    _entityManager.SetComponentData(spawnEntity, new CharacterSpawnRequest() {IsLocalPlayer = true});

                    var upgradeEntity = _entityManager.CreateEntity(typeof(CharacterUpgradeRequest));
                    _entityManager.SetComponentData(upgradeEntity,
                        new CharacterUpgradeRequest()
                        {
                            Endurance = Mathf.RoundToInt(enduranceCharacterPointsSlider.Value),
                            Intelligence = Mathf.RoundToInt(intelligenceCharacterPointsSlider.Value),
                            Power = Mathf.RoundToInt(powerCharacterPointsSlider.Value),
                            Reputation = 0,
                            SpeedPoints = Mathf.RoundToInt(speedCharacterPointsSlider.Value)
                        });
                    
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
                               _characterConfig.DefaultExperience);
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