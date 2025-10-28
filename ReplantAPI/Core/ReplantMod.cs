using MelonLoader;
using HarmonyLib; 
using Il2CppReloaded.Gameplay;

namespace ReplantAPI.Core
{
    public abstract class ReplantMod : MelonMod
    {
        private HarmonyLib.Harmony _harmony; 

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg($"{Info.Name} v{Info.Version} loaded!");

            _harmony = new HarmonyLib.Harmony($"com.replant.{Info.Name.ToLower().Replace(" ", "")}");

            var boardUpdateOriginal = typeof(Board).GetMethod("Update");
            var boardUpdatePostfix = typeof(ReplantMod).GetMethod(nameof(BoardUpdatePostfix),
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            _harmony.Patch(boardUpdateOriginal, postfix: new HarmonyMethod(boardUpdatePostfix));

            OnModInitialize();
        }

        public virtual void OnModInitialize() { }
        public virtual void OnBoardUpdate() { }
        public virtual void OnZombieSpawned(Zombie zombie) { }
        public virtual void OnPlantPlaced(Plant plant) { }
        public virtual void OnCoinCollected(Coin coin) { }
        public virtual void OnWaveStart(int waveNumber) { }

        private static ReplantMod _instance;
        private static int _lastZombieCount = 0;
        private static int _lastPlantCount = 0;
        private static int _lastWave = 0;

        private static void BoardUpdatePostfix(Board __instance)
        {
            if (_instance == null || __instance == null)
                return;

            ReplantAPI.UpdateBoardCache(__instance);
            _instance.OnBoardUpdate();

            // Fixed: Zombie spawning detection
            if (__instance.m_zombies != null)
            {
                int currentZombieCount = 0;
                for (int i = 0; i < __instance.m_zombies.Count; i++)
                {
                    var zombie = __instance.m_zombies[i];
                    if (zombie != null && !zombie.mDead)
                        currentZombieCount++;
                }

                if (currentZombieCount > _lastZombieCount)
                {
                    // Just call for all zombies - simpler
                    for (int i = 0; i < __instance.m_zombies.Count; i++)
                    {
                        var zombie = __instance.m_zombies[i];
                        if (zombie != null && !zombie.mDead)
                        {
                            // Check if this is a new zombie (simple heuristic)
                            if (i >= _lastZombieCount)
                                _instance.OnZombieSpawned(zombie);
                        }
                    }
                }
                _lastZombieCount = currentZombieCount;
            }

            // Fixed: Plant placement detection (removed mPlantAge)
            if (__instance.m_plants != null)
            {
                int currentPlantCount = 0;
                for (int i = 0; i < __instance.m_plants.Count; i++)
                {
                    var plant = __instance.m_plants[i];
                    if (plant != null)
                        currentPlantCount++;
                }

                if (currentPlantCount > _lastPlantCount)
                {
                    // Call for newly placed plants
                    for (int i = 0; i < __instance.m_plants.Count; i++)
                    {
                        var plant = __instance.m_plants[i];
                        if (plant != null && i >= _lastPlantCount)
                            _instance.OnPlantPlaced(plant);
                    }
                }
                _lastPlantCount = currentPlantCount;
            }

            int currentWave = __instance.mCurrentWave;
            if (currentWave > _lastWave)
                _instance.OnWaveStart(currentWave);
            _lastWave = currentWave;
        }

        protected ReplantMod()
        {
            _instance = this;
        }
    }
}