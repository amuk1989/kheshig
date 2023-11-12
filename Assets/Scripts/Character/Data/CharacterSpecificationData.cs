using System;
using Unity.Entities;

namespace Character.Data
{
    [Serializable]
    public struct CharacterSpecificationData:IComponentData
    {
        internal float Power;
        internal float Endurance;
        internal float Speed;
        internal float Intelligence;
        internal float Reputation;
    }
}