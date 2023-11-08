using Unity.Entities;
using Unity.Mathematics;

namespace Character.Data
{
    public struct CharacterOriginalData:IComponentData
    {
        public Entity Prefab;
    }
}