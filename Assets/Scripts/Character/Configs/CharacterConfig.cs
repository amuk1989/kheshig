using System;
using Base.Data;
using Base.Interfaces;
using Unity.Entities;
using UnityEngine;

namespace Character.Configs
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/CharacterConfig", order = 0)]
    public class CharacterConfig : BaseConfig<CharacterConfigData>
    {
    }

    [Serializable]
    public struct CharacterConfigData : IConfigData, IComponentData
    {
    }
}