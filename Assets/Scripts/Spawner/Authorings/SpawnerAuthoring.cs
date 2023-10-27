using Spawner.Data;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Spawner.Authorings
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        [Range(0, 10f)] [SerializeField] private float _range;
        public float3 Position => transform.position;
        public float Range => _range;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(Position, _range);
            Gizmos.DrawWireSphere(Position, _range);
        }
    }

    internal class SpawnerBacker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new SpawnerData()
            {
                SpawnPosition = authoring.Position,
                Range = authoring.Range
            });
        }
    }
}