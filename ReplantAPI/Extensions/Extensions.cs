using Il2CppReloaded.Gameplay;
using Il2CppReloaded.Data;

namespace ReplantAPI.Extensions
{
    public static class ZombieExtensions
    {
        public static bool IsClose(this Zombie zombie, float threshold = 250f)
        {
            return zombie.mPosX < threshold;
        }

        public static int GetTotalHealth(this Zombie zombie)
        {
            return zombie.mBodyHealth + zombie.mHelmHealth + zombie.mShieldHealth;
        }

        public static void Kill(this Zombie zombie)
        {
            zombie.TakeDamage(999999, 0);
        }

        public static void Freeze(this Zombie zombie, int duration = 300)
        {
            zombie.mChilledCounter = duration;
        }
    }

    public static class PlantExtensions
    {
        public static void Heal(this Plant plant)
        {
            plant.mPlantHealth = plant.mPlantMaxHealth;
        }

        public static bool IsDamaged(this Plant plant)
        {
            return plant.mPlantHealth < plant.mPlantMaxHealth;
        }
    }
    public static class AchievementExtensions
    {
        public static bool IsUnlocked(this AchievementType achievement)
        {
            return Core.ReplantAPI.Achievements.HasAchievement(achievement);
        }
        public static bool IsLocked(this AchievementType achievement)
        {
            return !Core.ReplantAPI.Achievements.HasAchievement(achievement);
        }

        public static bool Unlock(this AchievementType achievement)
        {
            return Core.ReplantAPI.Achievements.UnlockAchievement(achievement);
        }

        public static bool Lock(this AchievementType achievement)
        {
            return Core.ReplantAPI.Achievements.LockAchievement(achievement);
        }

        public static bool Toggle(this AchievementType achievement)
        {
            if (achievement.IsUnlocked())
                return achievement.Lock();
            else
                return achievement.Unlock();
        }
    }
}