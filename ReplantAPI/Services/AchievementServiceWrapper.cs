using Il2CppReloaded.Data;
using Il2CppReloaded.Services;

namespace ReplantAPI.Services
{
    public class AchievementServiceWrapper
    {
        private IAchievementService GetService()
        {
            var activity = Core.ReplantAPI.GameplayActivity;
            if (activity == null)
            {
                MelonLoader.MelonLogger.Warning("[ReplantAPI] GameplayActivity is null! Achievement service unavailable.");
                return null;
            }

            return activity.m_achievementService;
        }

        public bool HasAchievement(AchievementType achievement)
        {
            var service = GetService();
            if (service == null)
                return false;

            try
            {
                return service.IsGranted(achievement);
            }
            catch (System.Exception e)
            {
                MelonLoader.MelonLogger.Error($"[ReplantAPI] Error checking achievement: {e.Message}");
                return false;
            }
        }
        public bool UnlockAchievement(AchievementType achievement)
        {
            var service = GetService();
            if (service == null)
                return false;

            try
            {
                bool success = service.SetAchievement(achievement, true);
                if (success)
                {
                    MelonLoader.MelonLogger.Msg($"[ReplantAPI] Achievement unlocked: {achievement}");
                }
                return success;
            }
            catch (System.Exception e)
            {
                MelonLoader.MelonLogger.Error($"[ReplantAPI] Error unlocking achievement: {e.Message}");
                return false;
            }
        }

        public bool LockAchievement(AchievementType achievement)
        {
            var service = GetService();
            if (service == null)
                return false;

            try
            {
                bool success = service.SetAchievement(achievement, false);
                if (success)
                {
                    MelonLoader.MelonLogger.Msg($"[ReplantAPI] Achievement locked: {achievement}");
                }
                return success;
            }
            catch (System.Exception e)
            {
                MelonLoader.MelonLogger.Error($"[ReplantAPI] Error locking achievement: {e.Message}");
                return false;
            }
        }
        public bool IsInitialized()
        {
            var service = GetService();
            if (service == null)
                return false;

            try
            {
                return service.IsInitialized;
            }
            catch
            {
                return false;
            }
        }

        public Il2CppSystem.Collections.Generic.IReadOnlyList<AchievementEntryData> GetAllAchievements()
        {
            var service = GetService();
            if (service == null)
                return null;

            try
            {
                return service.Achievements;
            }
            catch (System.Exception e)
            {
                MelonLoader.MelonLogger.Error($"[ReplantAPI] Error getting achievements list: {e.Message}");
                return null;
            }
        }
        public void SendPendingAchievements()
        {
            var service = GetService();
            if (service == null)
                return;

            try
            {
                service.SendPendingAchievements();
                MelonLoader.MelonLogger.Msg("[ReplantAPI] Pending achievements sent to platform");
            }
            catch (System.Exception e)
            {
                MelonLoader.MelonLogger.Error($"[ReplantAPI] Error sending pending achievements: {e.Message}");
            }
        }
        public Il2CppSystem.Collections.Generic.List<int> GetPendingAchievements()
        {
            var service = GetService();
            if (service == null)
                return null;

            try
            {
                return service.GetPendingAchievements();
            }
            catch (System.Exception e)
            {
                MelonLoader.MelonLogger.Error($"[ReplantAPI] Error getting pending achievements: {e.Message}");
                return null;
            }
        }
    }
}