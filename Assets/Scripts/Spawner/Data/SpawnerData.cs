using Unity.Entities;
using Unity.Mathematics;

namespace Spawner
{
    public struct SpawnerData: IComponentData
    {
        public float3 SpawnPosition;
    }
}