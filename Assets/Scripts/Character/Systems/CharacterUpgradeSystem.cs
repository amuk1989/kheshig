using Character.Configs;
using Character.Data;
using Main.Data;
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
            state.RequireForUpdate(state.GetEntityQuery(ComponentType.ReadWrite<CharacterSpecificationData>()));
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
            
            var character = SystemAPI.GetSingleton<ThirdPersonPlayer>().ControlledCharacter;
            var currentSpecification = SystemAPI.GetComponentRO<CharacterSpecificationData>(character).ValueRO;

            if (!SystemAPI.TryGetSingletonEntity<ConfigTag>(out var configEntity)) return;
            var characterConfig = SystemAPI.GetComponent<CharacterConfigData>(configEntity);

            foreach (var (request, entity) in SystemAPI.Query<RefRO<CharacterUpgradeRequest>>().WithEntityAccess())
            {
                entityCommandBuffer.SetComponent(character, new CharacterSpecificationData()
                {
                    Power = currentSpecification.Power + request.ValueRO.Power,
                    Endurance = currentSpecification.Endurance + request.ValueRO.Endurance,
                    Intelligence = currentSpecification.Intelligence + request.ValueRO.Intelligence,
                    Speed = currentSpecification.Speed + request.ValueRO.SpeedPoints,
                    Reputation = currentSpecification.Reputation + request.ValueRO.Reputation,
                });
                
                entityCommandBuffer.DestroyEntity(entity);
            }
        }

        private int CalculateSpeed(CharacterConfigData configData, int currentSpeed, int speedPoints)
        {
            
            return 50;
        }
    }
}