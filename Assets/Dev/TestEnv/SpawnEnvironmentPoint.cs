using System.Diagnostics;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Environment.TestEnv
{
    public class SpawnEnvironmentPoint : MonoBehaviour
    {
        [SerializeField] private string _guid;
        [SerializeField] private float _distance;
        [SerializeField] private Color _gizmosColor;

        private GameObject _player;
        private float _sqrDistance;
        private IEnvironmentService _environmentService;

        [Inject]
        private void Construct(IEnvironmentService environmentService)
        {
            _environmentService = environmentService;
        }

        private void Start()
        {
            _sqrDistance = Mathf.Pow(_distance, 2f);
            _player = GameObject.FindWithTag("Player");
            
            _environmentService.PreloadAsync(_guid, destroyCancellationToken).Forget();
        }

        private void Update()
        {
            if ((_player.transform.position - transform.position).sqrMagnitude < _sqrDistance)
                LoadResource().Forget();
        }

        private async UniTask LoadResource()
        {
            var environmentServiceEnvironmentGuids = _environmentService.EnvironmentGuids;

            for (var i = 0; i < environmentServiceEnvironmentGuids.Count; i++)
            {
                if (environmentServiceEnvironmentGuids[i].Equals(_guid)) continue;
                _environmentService.Destroy(environmentServiceEnvironmentGuids[i]);
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            await _environmentService.SpawnEnvironmentAsync(_guid, transform, destroyCancellationToken);

            stopWatch.Stop();
            UnityEngine.Debug.Log($"Loading time: {stopWatch.ElapsedMilliseconds}");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(transform.position, _distance);
        }
    }
}