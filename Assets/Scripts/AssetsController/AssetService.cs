using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace AssetsController
{
    public class AssetService : IAssetService
    {
        private readonly Dictionary<string, IAssetHandler> _assetHandlers = new();

        async UniTask<TAsset> IAssetService.LoadAsync<TAsset>(string path, CancellationToken cancellationToken)
        {
            var asset = await LoadAsync<TAsset>(path, cancellationToken);

            if (asset == null)
            {
                Unload(path);
            }

            return asset;
        }

        void IAssetService.Unload(string path)
        {
            Unload(path);
        }

        void IDisposable.Dispose()
        {
            foreach (var handler in _assetHandlers.Values)
            {
                handler?.Unload();
            }

            _assetHandlers.Clear();
        }

        async UniTask<TAsset> LoadAsync<TAsset>(string path, CancellationToken cancellationToken)
            where TAsset : class
        {
            if (!_assetHandlers.TryGetValue(path, out var source))
            {
                return await LoadAssetAsync<TAsset>(path, cancellationToken);
            }

            if (source.CompletionSource is not UniTaskCompletionSource<TAsset> taskSource) return null;

            var result = await taskSource.Task.SuppressCancellationThrow();
            return result.IsCanceled ? null : result.Result;

        }

        private void Unload(string path)
        {
            if (_assetHandlers.Remove(path, out var handler))
            {
                handler.Unload();
            }
        }

        private async UniTask<TAsset> LoadAssetAsync<TAsset>(string path, CancellationToken cancellationToken)
            where TAsset : class
        {
            var taskSource = new UniTaskCompletionSource<TAsset>();

            IAssetHandler handler = new AssetHandler<TAsset>(path, taskSource);
            _assetHandlers[path] = handler;
            handler.LoadAsync(cancellationToken).Forget();

            var result = await taskSource.Task.SuppressCancellationThrow();
            return result.IsCanceled ? null : result.Result;
        }
    }
}