using Player.Data;
using Unity.Burst;
using Unity.Entities;

namespace Player.Systems
{
    [BurstCompile]
    public partial struct CreatePlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CreatePlayerRequest>();
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
            
            foreach (var (request, entity) in SystemAPI.Query<RefRO<CreatePlayerRequest>>().WithEntityAccess())
            {
                entityCommandBuffer.DestroyEntity(entity);
                
                if (SystemAPI.HasSingleton<PlayerData>()) return;
                
                var player = entityCommandBuffer.CreateEntity();
                entityCommandBuffer.AddComponent<PlayerData>(player);
            }
        }
    }
}