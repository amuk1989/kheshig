using Character.Configs;
using Character.Data;
using Player.Data;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace Character.Systems
{
    [BurstCompile]
    public partial struct CharacterUpgradeSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CharacterUpgradeRequest>();
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate(state.GetEntityQuery(ComponentType.ReadOnly<PlayerPrefabData>(), 
                ComponentType.ReadWrite<CharacterSpecificationData>()));
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBufferSystem = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            var player = SystemAPI.GetSingletonEntity<PlayerPrefabData>();
            var character = SystemAPI.GetSingletonEntity<ThirdPersonPlayer>();
            var currentSpecification = SystemAPI.GetComponentRO<CharacterSpecificationData>(player).ValueRO;
            
            foreach (var (request, entity) in SystemAPI.Query<RefRO<CharacterUpgradeRequest>>().WithEntityAccess())
            {
                entityCommandBuffer.SetComponent(player, new CharacterSpecificationData()
                {
                    Power = currentSpecification.Power + request.ValueRO.Power,
                    Endurance = currentSpecification.Endurance + request.ValueRO.Endurance,
                    Intelligence = currentSpecification.Intelligence + request.ValueRO.Intelligence,
                    Speed = currentSpecification.Speed + request.ValueRO.Speed,
                    Reputation = currentSpecification.Reputation + request.ValueRO.Reputation,
                });
                
                entityCommandBuffer.DestroyEntity(entity);
            }
        }
    }
}