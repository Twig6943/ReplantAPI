using HarmonyLib;
using Il2CppReloaded.Gameplay;

namespace ReplantAPI.Core
{
    [HarmonyPatch(typeof(Board), "Update")]
    public class BoardUpdatePatch
    {
        static void Postfix(Board __instance)
        {
            ReplantAPI.UpdateBoardCache(__instance);
        }
    }
}