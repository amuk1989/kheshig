using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AssetsController
{
    public class AssetHandler<TAssetType> : IAssetHandler where TAssetType : class
    {
        private AsyncOperationHandle<TAssetType> _loadingHandler;
        
        private readonly UniTaskCompletionSource<TAssetType> _completionSource;
        private readonly string _assetsPath;

        public AssetHandler(string assetsPath, UniTaskCompletionSource<TAssetType> completionSource)
        {
            _assetsPath = assetsPath;
            _completionSource = completionSource;
        }

        IUniTaskSource IAssetHandler.CompletionSource => _completionSource;

        async UniTask IAssetHandler.LoadAsync(CancellationToken cancellationToken)
        {
            _loadingHandler = Addressables.LoadAssetAsync<TAssetType>(_assetsPath);

            try
            {
                var asset = await _loadingHandler.ToUniTask(cancellationToken: cancellationToken);
                _completionSource.TrySetResult(asset);
                Debug.Log("Loading completed");
            }
            catch (Exception _)
            {
                Debug.LogWarning($"{this} : Loading cancelled");
                _completionSource.TrySetCanceled();
            }
        }

        public void Unload()
        {
            _completionSource.TrySetCanceled();
            if (_loadingHandler.IsValid()) Addressables.Release(_loadingHandler);
        }
    }
}