using System.Collections.Generic;
using System.Linq;
using Il2CppReloaded.Data;

namespace ReplantAPI.Collections
{
    public class AchievementCollection
    {
        private Services.AchievementServiceWrapper GetService()
        {
            return Core.ReplantAPI.Achievements;
        }
        public int GetUnlockedCount()
        {
            int count = 0;
            var achievements = System.Enum.GetValues(typeof(AchievementType));

            foreach (AchievementType achievement in achievements)
            {
                if (achievement == AchievementType.MaxAchievements)
                    continue;

                if (GetService().HasAchievement(achievement))
                    count++;
            }

            return count;
        }

        public int GetTotalCount()
        {
            return (int)AchievementType.MaxAchievements;
        }

        public float GetCompletionPercentage()
        {
            int total = GetTotalCount();
            if (total == 0)
                return 0f;

            return ((float)GetUnlockedCount() / total) * 100f;
        }
        public List<AchievementType> GetUnlocked()
        {
            var unlocked = new List<AchievementType>();
            var achievements = System.Enum.GetValues(typeof(AchievementType));

            foreach (AchievementType achievement in achievements)
            {
                if (achievement == AchievementType.MaxAchievements)
                    continue;

                if (GetService().HasAchievement(achievement))
                    unlocked.Add(achievement);
            }

            return unlocked;
        }
        public List<AchievementType> GetLocked()
        {
            var locked = new List<AchievementType>();
            var achievements = System.Enum.GetValues(typeof(AchievementType));

            foreach (AchievementType achievement in achievements)
            {
                if (achievement == AchievementType.MaxAchievements)
                    continue;

                if (!GetService().HasAchievement(achievement))
                    locked.Add(achievement);
            }

            return locked;
        }

        public void UnlockAll()
        {
            var achievements = System.Enum.GetValues(typeof(AchievementType));

            foreach (AchievementType achievement in achievements)
            {
                if (achievement == AchievementType.MaxAchievements)
                    continue;

                GetService().UnlockAchievement(achievement);
            }

            MelonLoader.MelonLogger.Msg("[ReplantAPI] All achievements unlocked!");
        }
        public void LockAll()
        {
            var achievements = System.Enum.GetValues(typeof(AchievementType));

            foreach (AchievementType achievement in achievements)
            {
                if (achievement == AchievementType.MaxAchievements)
                    continue;

                GetService().LockAchievement(achievement);
            }

            MelonLoader.MelonLogger.Msg("[ReplantAPI] All achievements locked!");
        }
        public bool HasFullCompletion()
        {
            return GetUnlockedCount() == GetTotalCount();
        }
        public void PrintAll()
        {
            MelonLoader.MelonLogger.Msg("=== Achievement Status ===");
            var achievements = System.Enum.GetValues(typeof(AchievementType));

            foreach (AchievementType achievement in achievements)
            {
                if (achievement == AchievementType.MaxAchievements)
                    continue;

                bool unlocked = GetService().HasAchievement(achievement);
                string status = unlocked ? "[✓]" : "[ ]";
                MelonLoader.MelonLogger.Msg($"{status} {achievement}");
            }

            MelonLoader.MelonLogger.Msg($"Total: {GetUnlockedCount()}/{GetTotalCount()} ({GetCompletionPercentage():F1}%)");
        }
    }
}