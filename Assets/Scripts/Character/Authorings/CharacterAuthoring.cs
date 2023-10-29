using System;
using Character.Data;
using Character.Views;
using Spawner.Authorings;
using Spawner.Data;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Character.Authorings
{
    public class CharacterAuthoring : MonoBehaviour
    {
        [SerializeField] private CharacterPrefab _characterPrefab;
        
        public CharacterPrefab CharacterPrefab => _characterPrefab;
    }
    
    public class CharacterBaker : Baker<CharacterAuthoring>
    {
        public override void Bake(CharacterAuthoring authoring)
        {
            var authoringEntity = GetEntity(TransformUsageFlags.None);
            
            AddComponent(authoringEntity, new CharacterData()
            {
                Prefab = GetEntity(authoring.CharacterPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }

}