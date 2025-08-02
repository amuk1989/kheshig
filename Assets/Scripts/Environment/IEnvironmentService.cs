using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Environment
{
    public interface IEnvironmentService
    {
        IReadOnlyList<string> EnvironmentGuids { get; }

        UniTask PreloadAsync(string guid, CancellationToken cancellationToken);

        UniTask SpawnEnvironmentAsync(string guid, Transform parent, CancellationToken cancellationToken);

        void Destroy(string guid);
    }
}