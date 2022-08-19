namespace Hexed.Loader
{
    public class MainLoader
    {
        public static unsafe void OnApplicationStart()
        {
            Initialize.OnStart();
        }

        public static void OnLevelWasLoaded(int level)
        {
            Initialize.OnLevelLoaded(level);
        }

        public static void OnLevelWasInitialized(int level)
        {
            Initialize.OnLevelInit(level);
        }

        public static void OnUpdate()
        {
            Initialize.OnUpdate();
        }

        public static void OnApplicationQuit()
        {
            Initialize.OnQuit();
        }
    }
}
