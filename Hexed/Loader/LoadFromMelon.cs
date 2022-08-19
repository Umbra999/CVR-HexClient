using MelonLoader;

namespace Hexed.Loader
{
    public class LoadFromMelon : MelonMod
    {
        public override void OnApplicationStart()
        {
            Initialize.OnStart();
        }

        public override void OnUpdate()
        {
            Initialize.OnUpdate();
        }

        public override void OnLevelWasLoaded(int level)
        {
            Initialize.OnLevelLoaded(level);
        }

        public override void OnLevelWasInitialized(int level)
        {
            Initialize.OnLevelInit(level);
        }

        public override void OnApplicationQuit()
        {
            Initialize.OnQuit();
        }
    }
}
