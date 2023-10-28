using System;
using Character.Data;
using Spawner.Data;
using UniRx;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Views
{
    public class TestUI : MonoBehaviour
    {
        [SerializeField] private Button _start;

        private EntityManager _entityManager;

        [Inject]
        private void Construct(EntityManager entityManager)
        {
            _entityManager = entityManager;
        }

        private void Start()
        {
            _start
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _entityManager.CreateEntity(typeof(CharacterSpawnRequest));
                })
                .AddTo(this);
        }
    }
}