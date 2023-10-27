using System.ComponentModel;
using Base.Interfaces;
using Character.Data;
using Main.Data;
using Unity.Entities;
using UnityEngine;
using Zenject;

namespace Utility
{
    public static class ZenjectExtensions
    {
        public static void InstallServiceAsInterface<TService>(this DiContainer container)
        {
            container
                .BindInterfacesTo<TService>()
                .AsSingle()
                .NonLazy();
        }
        
        public static void InstallRegistry<TRegistry>(this DiContainer container, TRegistry registry) 
            where TRegistry: struct, IConfigData
        {
            container
                .Bind<TRegistry>()
                .FromInstance(registry);
        }
    }
}