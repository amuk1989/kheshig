using Unity.Entities;
using Unity.Mathematics;

namespace Spawner.Data
{
    public struct SpawnerData: IComponentData
    {
        public float3 SpawnPosition;
        public float Range;
    }
}