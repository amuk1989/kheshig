using System.Threading;
using Cysharp.Threading.Tasks;

namespace AssetsController
{
    public interface IAssetHandler
    {
        IUniTaskSource CompletionSource { get; }
        UniTask LoadAsync(CancellationToken cancellationToken);
        void Unload();
    }
}