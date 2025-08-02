using System;
using System.Threading;
using AssetsController;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Environment
{
    public partial class SpawnHandler : IDisposable
    {
        private readonly UniTaskCompletionSource<GameObject> _completionSource;
        private readonly IAssetService _assetService;
        private readonly string _path;

        private GameObject _environment;
        private AsyncInstantiateOperation<GameObject> _instantiateOperation;

        public SpawnHandler(string path, IAssetService assetService)
        {
            _path = path;
            _assetService = assetService;
            _completionSource = new UniTaskCompletionSource<GameObject>();
        }

        public UniTaskCompletionSource<GameObject> CompletionSource => _completionSource;

        public async UniTask LoadResourceAsync(CancellationToken cancellationToken)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(_path, cancellationToken);
            if (!prefab) Unload();
        }

        public async UniTask<GameObject> SpawnAsync(CancellationToken cancellationToken)
        {
            var prefab = await _assetService.LoadAsync<GameObject>(_path, cancellationToken);
            if (!prefab) Dispose();

            try
            {
                _instantiateOperation = Object.InstantiateAsync(prefab);
                var environment = await _instantiateOperation.WithCancellation(cancellationToken: cancellationToken);

                if (environment == null || environment.Length == 0)
                {
                    Dispose();
                    return null;
                }

                _environment = environment[0];
                _completionSource.TrySetResult(_environment);

                return _environment;
            }
            catch (Exception _)
            {
                Dispose();
                return null;
            }
        }

        public void Unload()
        {
            _assetService.Unload(_path);
        }

        public void Dispose()
        {
            _instantiateOperation?.Cancel();

            Unload();

            _completionSource.TrySetCanceled();
            if (_environment) Object.Destroy(_environment);
        }
    }
}