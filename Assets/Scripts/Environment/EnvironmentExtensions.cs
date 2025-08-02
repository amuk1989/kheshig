namespace Environment
{
    public static class EnvironmentExtensions
    {
        public static void ClearEnvironment(this IEnvironmentService environmentService)
        {
            var environmentServiceEnvironmentGuids = environmentService.EnvironmentGuids;

            for (var i = 0; i < environmentServiceEnvironmentGuids.Count; i++)
            {
                environmentService.Destroy(environmentServiceEnvironmentGuids[i]);
            }
        }
    }
}