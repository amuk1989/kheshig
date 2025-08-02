using System.Threading;
using AssetsController;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace Environment.TestEnv
{
    public class SpawnScenePoint : MonoBehaviour
    {
        [SerializeField] private string _guid;
        [SerializeField] private float _distance;
        [SerializeField] private Color _gizmosColor;

        private GameObject _player;
        private bool _isLoad;
        private GameObject _resource;
        private AsyncInstantiateOperation<GameObject> _instantiateOperation;
        private float _sqrDistance;
        private CancellationTokenSource _loadingCancellationTokenSource;
        private IAssetService _assetService;

        [Inject]
        private void Construct(IAssetService assetService)
        {
            _assetService = assetService;
        }

        private void Start()
        {
            _sqrDistance = Mathf.Pow(_distance, 2f);
            _player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if ((_player.transform.position - transform.position).sqrMagnitude < _sqrDistance)
                LoadResource().Forget();
            // else
                // UnloadResource();
        }

        private async UniTask LoadResource()
        {
            if (_isLoad) return;

            _isLoad = true;

            _loadingCancellationTokenSource =
                CancellationTokenSource.CreateLinkedTokenSource(destroyCancellationToken);

            var resource = await Addressables
                .LoadSceneAsync(_guid, LoadSceneMode.Additive, false)
                .ToUniTask(cancellationToken: _loadingCancellationTokenSource.Token);

            await resource.ActivateAsync().ToUniTask(cancellationToken: _loadingCancellationTokenSource.Token);
        }

        private void UnloadResource()
        {
            if (!_isLoad) return;

            _loadingCancellationTokenSource?.Cancel();
            _loadingCancellationTokenSource?.Dispose();
            _loadingCancellationTokenSource = null;

            _assetService.Unload(_guid);

            Destroy(_resource);
            _isLoad = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(transform.position, _distance);
        }
    }
}