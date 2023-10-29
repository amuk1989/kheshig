using Unity.Entities;

namespace Character.Data
{
    public struct CharacterSpecificationRequest:IComponentData
    {
        public float Power;
        public float Endurance;
        public float Speed;
        public float Intelligence;
        public float Reputation;
    }
}