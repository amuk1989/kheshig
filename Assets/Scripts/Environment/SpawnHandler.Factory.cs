using AssetsController;
using VContainer;

namespace Environment
{
    public partial class SpawnHandler
    {
        public class Factory
        {
            private readonly IObjectResolver _objectResolver;
            private readonly IAssetService _assetService;

            public Factory(IObjectResolver objectResolver, IAssetService assetService)
            {
                _objectResolver = objectResolver;
                _assetService = assetService;
            }

            public SpawnHandler Create(string path)
            {
                return new SpawnHandler(path, _assetService);
            }
        }
    }
}