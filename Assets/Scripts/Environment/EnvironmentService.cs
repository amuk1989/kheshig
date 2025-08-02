using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Environment
{
    internal class EnvironmentService : IEnvironmentService
    {
        private readonly Dictionary<string, SpawnHandler> _spawnHandlers = new();

        private readonly SpawnHandler.Factory _spawnHandlerFactory;

        public EnvironmentService(SpawnHandler.Factory spawnHandlerFactory)
        {
            _spawnHandlerFactory = spawnHandlerFactory;
        }

        public IReadOnlyList<string> EnvironmentGuids => _spawnHandlers.Keys.ToArray();

        public async UniTask PreloadAsync(string guid, CancellationToken cancellationToken)
        {
            var handler = _spawnHandlerFactory.Create(guid);
            await handler.LoadResourceAsync(cancellationToken);
            handler.Dispose();
        }

        public async UniTask SpawnEnvironmentAsync(string guid, Transform parent, CancellationToken cancellationToken)
        {
            var environment = await SpawnAsync(guid, cancellationToken);
            if (environment) environment.transform.SetParent(parent, false);
        }

        public void Destroy(string guid)
        {
            if (_spawnHandlers.Remove(guid, out var handler))
            {
                handler.Dispose();
            }
        }

        private async UniTask<GameObject> SpawnAsync(string guid, CancellationToken cancellationToken)
        {
            if (_spawnHandlers.TryGetValue(guid, out var spawnHandler))
            {
                var spawnTask = await spawnHandler.CompletionSource.Task.SuppressCancellationThrow();

                return spawnTask.IsCanceled ? null : spawnTask.Result;
            }

            _spawnHandlers[guid] = _spawnHandlerFactory.Create(guid);

            return await _spawnHandlers[guid].SpawnAsync(cancellationToken);
        }
    }
}