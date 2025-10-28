using Il2CppReloaded.Gameplay;

namespace ReplantAPI.Services
{
    public class PlayerServiceWrapper
    {
        public int GetSun()
        {
            var board = Core.ReplantAPI.Board;
            if (board == null)
            {
                MelonLoader.MelonLogger.Warning("[ReplantAPI] Board is null! Make sure you're in a level.");
                return 0;
            }

            if (board.mSunMoney == null)
            {
                MelonLoader.MelonLogger.Warning("[ReplantAPI] board.mSunMoney is null!");
                return 0;
            }

            try
            {
                var sun = board.mSunMoney[0];
                if (sun == null)
                {
                    MelonLoader.MelonLogger.Warning("[ReplantAPI] sun object is null!");
                    return 0;
                }

                return sun.Amount;
            }
            catch (System.Exception e)
            {
                MelonLoader.MelonLogger.Error($"[ReplantAPI] Error getting sun: {e.Message}");
                return 0;
            }
        }

        public void SetSun(int amount)
        {
            var board = Core.ReplantAPI.Board;
            if (board == null)
            {
                MelonLoader.MelonLogger.Warning("[ReplantAPI] Board is null! Make sure you're in a level.");
                return;
            }

            if (board.mSunMoney == null)
            {
                MelonLoader.MelonLogger.Warning("[ReplantAPI] board.mSunMoney is null!");
                return;
            }

            try
            {
                var sun = board.mSunMoney[0];
                if (sun != null)
                {
                    sun.Amount = amount;
                    MelonLoader.MelonLogger.Msg($"[ReplantAPI] Successfully set sun to {amount}");
                }
                else
                {
                    MelonLoader.MelonLogger.Warning("[ReplantAPI] sun object is null!");
                }
            }
            catch (System.Exception e)
            {
                MelonLoader.MelonLogger.Error($"[ReplantAPI] Error setting sun: {e.Message}");
            }
        }

        public void AddSun(int amount)
        {
            var board = Core.ReplantAPI.Board;
            if (board == null)
            {
                MelonLoader.MelonLogger.Warning("[ReplantAPI] Board is null! Make sure you're in a level.");
                return;
            }

            try
            {
                board.AddSunMoney(amount, 0);
                MelonLoader.MelonLogger.Msg($"[ReplantAPI] Successfully added {amount} sun");
            }
            catch (System.Exception e)
            {
                MelonLoader.MelonLogger.Error($"[ReplantAPI] Error adding sun: {e.Message}");
            }
        }

        public void EnableInfiniteSun()
        {
            SetSun(9999);
        }

        public void EnableInstantCooldown()
        {
            var board = Core.ReplantAPI.Board;
            if (board == null || board.mSeedBank == null)
            {
                MelonLoader.MelonLogger.Warning("[ReplantAPI] Board or SeedBank is null!");
                return;
            }

            try
            {
                for (int i = 0; i < board.mSeedBank.mSeedPackets.Count; i++)
                {
                    var packet = board.mSeedBank.mSeedPackets[i];
                    if (packet != null)
                    {
                        packet.mRefreshCounter = 0;
                        packet.mRefreshTime = 0;
                    }
                }
                MelonLoader.MelonLogger.Msg("[ReplantAPI] Instant cooldown enabled!");
            }
            catch (System.Exception e)
            {
                MelonLoader.MelonLogger.Error($"[ReplantAPI] Error enabling instant cooldown: {e.Message}");
            }
        }
    }
}