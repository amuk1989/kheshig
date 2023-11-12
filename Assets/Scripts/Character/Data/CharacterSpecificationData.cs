using System;
using Unity.Entities;

namespace Character.Data
{
    [Serializable]
    public struct CharacterSpecificationData:IComponentData
    {
        internal int Power;
        internal int Endurance;
        internal int Speed;
        internal int Intelligence;
        internal int Reputation;
    }
}