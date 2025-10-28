using System;
using System.Net.Http;
using System.Threading.Tasks;
using MelonLoader;

namespace ReplantAPI.Core
{
    public class UpdateChecker
    {
        private const string CURRENT_VERSION = "1.0.1"; 
        private const string VERSION_FILE_URL = "https://raw.githubusercontent.com/HenHenV2/ReplantAPI/refs/heads/main/version";
        private const string DOWNLOAD_URL = "https://gamebanana.com/mods/629661"; 

        private static bool _hasChecked = false;

        public static async void CheckForUpdates()
        {
            if (_hasChecked) return;
            _hasChecked = true;

            try
            {
                await CheckForUpdatesAsync();
            }
            catch (Exception ex)
            {
                MelonLogger.Warning($"[ReplantAPI] Could not check for updates: {ex.Message}");
            }
        }

        private static async Task CheckForUpdatesAsync()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(5);

                try
                {
                    string latestVersion = await client.GetStringAsync(VERSION_FILE_URL);
                    latestVersion = latestVersion.Trim(); 

                    if (IsNewerVersion(latestVersion, CURRENT_VERSION))
                    {
                        MelonLogger.Msg("");
                        MelonLogger.Msg("╔════════════════════════════════════════════════╗");
                        MelonLogger.Msg("║                                                ║");
                        MelonLogger.Msg("║     ⚠️  ReplantAPI Update Available! ⚠️       ║");
                        MelonLogger.Msg("║                                                ║");
                        MelonLogger.Msg($"║  Your Version:   {CURRENT_VERSION,-29} ║");
                        MelonLogger.Msg($"║  Latest Version: {latestVersion,-29} ║");
                        MelonLogger.Msg("║                                                ║");
                        MelonLogger.Msg("║  Download the latest version from GameBanana:  ║");
                        MelonLogger.Msg($"║  {DOWNLOAD_URL,-47} ║");
                        MelonLogger.Msg("║                                                ║");
                        MelonLogger.Msg("╚════════════════════════════════════════════════╝");
                        MelonLogger.Msg("");
                    }
                    else
                    {
                        MelonLogger.Msg($"[ReplantAPI] ✓ You're up to date! (v{CURRENT_VERSION})");
                    }
                }
                catch (HttpRequestException ex)
                {
                    MelonLogger.Warning($"[ReplantAPI] Could not check for updates: {ex.Message}");
                }
                catch (TaskCanceledException)
                {
                    MelonLogger.Warning("[ReplantAPI] Update check timed out");
                }
                catch (Exception ex)
                {
                    MelonLogger.Warning($"[ReplantAPI] Error checking for updates: {ex.Message}");
                }
            }
        }

        private static bool IsNewerVersion(string latest, string current)
        {
            try
            {
                var latestParts = latest.Split('.');
                var currentParts = current.Split('.');

                for (int i = 0; i < Math.Max(latestParts.Length, currentParts.Length); i++)
                {
                    int latestNum = i < latestParts.Length ? int.Parse(latestParts[i]) : 0;
                    int currentNum = i < currentParts.Length ? int.Parse(currentParts[i]) : 0;

                    if (latestNum > currentNum)
                        return true; 
                    if (latestNum < currentNum)
                        return false;
                }

                return false; 
            }
            catch
            {
                return false; 
            }
        }
    }
}