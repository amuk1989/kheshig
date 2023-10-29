using Unity.Entities;

namespace Character.Data
{
    internal struct CharacterSpecificationData:IComponentData
    {
        internal float Power;
        internal float Endurance;
        internal float Speed;
        internal float Intelligence;
        internal float Reputation;
    }
}