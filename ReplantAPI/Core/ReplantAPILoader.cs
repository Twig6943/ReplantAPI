using MelonLoader;

namespace ReplantAPI.Core
{
    public class ReplantAPILoader : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("========================================");
            LoggerInstance.Msg("        ReplantAPI Initializing...     ");
            LoggerInstance.Msg("========================================");

            var harmony = new HarmonyLib.Harmony("com.replantapi.core");
            harmony.PatchAll();

            LoggerInstance.Msg("⚠️  IMPORTANT NOTICE ⚠️");
            LoggerInstance.Msg("PLEASE READ THE NEW DOCUMENTATION:");
            LoggerInstance.Msg("https://replantapi.netlify.app/"); 
            LoggerInstance.Msg("----------------------------------------");
            LoggerInstance.Msg("Harmony patches applied successfully!");
            LoggerInstance.Msg("Board will be detected automatically!");
            LoggerInstance.Msg("API is ready for use by mods!");
            LoggerInstance.Msg("========================================");

            UpdateChecker.CheckForUpdates();
        }
    }
}
