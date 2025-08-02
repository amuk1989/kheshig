using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace AssetsController
{
    public interface IAssetService : IDisposable
    {
        UniTask<TAsset> LoadAsync<TAsset>(string path, CancellationToken cancellationToken) where TAsset : class;

        void Unload(string path);
    }
}