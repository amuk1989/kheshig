using Character.Data;
using Player.Data;
using Spawner.Data;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Spawner.Systems
{
    public partial struct SpawnSystem : ISystem
    {
        private EntityQuery _spawnerQuery;
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate(state.GetEntityQuery(ComponentType.ReadWrite<CharacterSpawnRequest>()));
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate<SpawnerData>();
        }
        
        public void OnDestroy(ref SystemState state)
        {
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBufferSystem = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);

            if (!SystemAPI.TryGetSingleton<SpawnerData>(out var spawner) || 
                !SystemAPI.TryGetSingleton<CharacterOriginalData>(out var character)) return;
            
            foreach (var (request, entity) in SystemAPI.Query<RefRW<CharacterSpawnRequest>>().WithEntityAccess())
            {
                if (SystemAPI.HasSingleton<PlayerPrefabData>() && request.ValueRO.IsLocalPlayer) continue;
                    
                var random = new Random((uint)SystemAPI.Time.ElapsedTime + 1);

                var randomAngle = random.NextFloat() * 360;
                var randomRad = random.NextFloat() * spawner.Range;

                var position = new float3(randomRad * math.sin(randomAngle) + spawner.SpawnPosition.x, 
                    spawner.SpawnPosition.y, randomRad * math.cos(randomAngle) + spawner.SpawnPosition.z);

                var characterEntity = entityCommandBuffer.Instantiate(character.Prefab);
                
                entityCommandBuffer.SetComponent(characterEntity, LocalTransform.FromPosition(position));
                if (request.ValueRO.IsLocalPlayer) entityCommandBuffer.AddComponent<PlayerPrefabData>(characterEntity);
                entityCommandBuffer.AddComponent<CharacterData>(characterEntity);
                entityCommandBuffer.DestroyEntity(entity);
            }
        }
    }
}