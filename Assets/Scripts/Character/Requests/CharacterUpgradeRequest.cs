using Unity.Entities;

namespace Character.Data
{
    public struct CharacterUpgradeRequest:IComponentData
    {
        public int Power;
        public int Endurance;
        public int SpeedPoints;
        public int Intelligence;
        public int Reputation;
    }
}