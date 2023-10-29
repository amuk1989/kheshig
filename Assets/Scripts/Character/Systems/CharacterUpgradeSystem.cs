using Character.Configs;
using Character.Data;
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
            state.RequireForUpdate<CharacterSpecificationRequest>();
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
            state.RequireForUpdate(state.GetEntityQuery(ComponentType.ReadOnly<PlayerData>(), 
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
            
            var player = SystemAPI.GetSingletonEntity<PlayerData>();
            var currentSpecification = SystemAPI.GetComponentRO<CharacterSpecificationData>(player).ValueRO;
            
            foreach (var (request, entity) in SystemAPI.Query<RefRO<CharacterSpecificationRequest>>().WithEntityAccess())
            {
                entityCommandBuffer.SetComponent(player, new CharacterSpecificationData()
                {
                    Power = math.clamp(currentSpecification.Power + request.ValueRO.Power,0f,100f),
                    Endurance = math.clamp(currentSpecification.Endurance + request.ValueRO.Endurance,0f,100f),
                    Intelligence = math.clamp(currentSpecification.Intelligence + request.ValueRO.Intelligence, 0f, 100f),
                    Speed = math.clamp(currentSpecification.Speed + request.ValueRO.Speed, 0f, 100f),
                    Reputation = currentSpecification.Reputation + request.ValueRO.Reputation,
                });
                
                entityCommandBuffer.DestroyEntity(entity);
            }
        }
    }
}