using Unity.Entities;

namespace Spawner.Data
{
    public struct CharacterSpawnRequest:IComponentData
    {
        public bool IsLocalPlayer;
    }
}