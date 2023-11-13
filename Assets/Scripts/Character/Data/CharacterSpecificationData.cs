using System;
using Unity.Entities;

namespace Character.Data
{
    [Serializable]
    public struct CharacterSpecificationData:IComponentData
    {
        public int Power;
        public int Endurance;
        public float Speed;
        public int Intelligence;
        public int Reputation;
    }
}