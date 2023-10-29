using Unity.Entities;
using Unity.Mathematics;

namespace Character.Data
{
    public struct CharacterData:IComponentData
    {
        public Entity Prefab;
    }
}