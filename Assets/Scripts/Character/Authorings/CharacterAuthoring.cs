using System;
using Spawner.Authorings;
using Spawner.Data;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Character.Authorings
{
    public class CharacterAuthoring : MonoBehaviour
    {
    }
    
    public class CharacterBaker : Baker<CharacterAuthoring>
    {
        public override void Bake(CharacterAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new CharacterSpawnRequest());
        }
    }

}