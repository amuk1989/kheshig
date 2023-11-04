using Unity.Entities;

namespace Character.Data
{
    public struct CharacterUpgradeRequest:IComponentData
    {
        public float Power;
        public float Endurance;
        public float Speed;
        public float Intelligence;
        public float Reputation;
    }
}