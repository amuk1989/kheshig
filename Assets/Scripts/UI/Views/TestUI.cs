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
    public class TestUI : MonoBehaviour, IDisposable
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
                    var entity = _entityManager.CreateEntity(typeof(CharacterSpawnRequest));
                    _entityManager.SetComponentData(entity, new CharacterSpawnRequest() {IsLocalPlayer = true});
                    Dispose();
                })
                .AddTo(this);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}