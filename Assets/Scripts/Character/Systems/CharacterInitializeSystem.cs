using Character.Data;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Character.Systems
{
    public partial struct CharacterInitializeSystem: ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate(state.GetEntityQuery(ComponentType.ReadOnly<ThirdPersonPlayer>(), 
                ComponentType.Exclude<CharacterData>()));
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entityCommandBufferSystem = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (playerData, entity) in SystemAPI.Query<RefRO<ThirdPersonPlayer>>().WithEntityAccess())
            {
                entityCommandBuffer.AddComponent<CharacterData>(entity);
            }
        }
    }
}