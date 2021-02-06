using AlbedoTeam.Sdk.JobWorker;

namespace Communications.Business
{
    internal static class Program
    {
        private static void Main()
        {
            Worker.Configure<Startup>().Run();
        }
    }
}