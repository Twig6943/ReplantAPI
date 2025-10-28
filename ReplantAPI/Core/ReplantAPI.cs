using Il2CppReloaded.Gameplay;
using Il2CppReloaded.Services;
using Il2CppReloaded.TreeStateActivities;
using ReplantAPI.Collections;
using ReplantAPI.Services;

namespace ReplantAPI.Core
{
    public static class ReplantAPI
    {
        private static Board _cachedBoard;
        private static GameplayActivity _cachedActivity;
        private static IAudioService _cachedAudio;

        public static Board Board
        {
            get
            {
                if (_cachedBoard != null)
                {
                    try
                    {
                        var test = _cachedBoard.mSunMoney;
                        return _cachedBoard;
                    }
                    catch
                    {
                        _cachedBoard = null;
                    }
                }

                var activity = GameplayActivity;
                if (activity != null)
                {
                    _cachedBoard = activity.m_board;
                    return _cachedBoard;
                }

                return null;
            }
        }

        public static GameplayActivity GameplayActivity
        {
            get
            {
                if (_cachedActivity != null)
                {
                    try
                    {
                        var test = _cachedActivity.m_board;
                        return _cachedActivity;
                    }
                    catch
                    {
                        _cachedActivity = null;
                    }
                }

                var board = _cachedBoard;
                if (board != null)
                {
                    _cachedActivity = board.mApp;
                    return _cachedActivity;
                }

                return null;
            }
        }

        public static IAudioService Audio
        {
            get
            {
                if (_cachedAudio != null)
                {
                    try
                    {
                        var test = _cachedAudio.BurstOverride;
                        return _cachedAudio;
                    }
                    catch
                    {
                        _cachedAudio = null;
                    }
                }

                var activity = GameplayActivity;
                if (activity != null)
                {
                    _cachedAudio = activity.m_audioService;
                    return _cachedAudio;
                }

                return null;
            }
        }

        public static bool IsGameActive => Board != null;

        private static ZombieCollection _zombies;
        private static PlantCollection _plants;
        private static CoinCollection _coins;
        private static ProjectileCollection _projectiles;

        public static ZombieCollection Zombies
        {
            get
            {
                if (_zombies == null)
                    _zombies = new ZombieCollection();
                return _zombies;
            }
        }

        public static PlantCollection Plants
        {
            get
            {
                if (_plants == null)
                    _plants = new PlantCollection();
                return _plants;
            }
        }

        public static CoinCollection Coins
        {
            get
            {
                if (_coins == null)
                    _coins = new CoinCollection();
                return _coins;
            }
        }

        public static ProjectileCollection Projectiles
        {
            get
            {
                if (_projectiles == null)
                    _projectiles = new ProjectileCollection();
                return _projectiles;
            }
        }

        private static AudioServiceWrapper _audioService;
        private static PlayerServiceWrapper _playerService;
        private static SpawnServiceWrapper _spawnService;
        private static LevelServiceWrapper _levelService;
        private static AchievementServiceWrapper _achievementService;
        private static AchievementCollection _achievementCollection;

        public static AudioServiceWrapper AudioService
        {
            get
            {
                if (_audioService == null)
                    _audioService = new AudioServiceWrapper();
                return _audioService;
            }
        }

        public static PlayerServiceWrapper Player
        {
            get
            {
                if (_playerService == null)
                    _playerService = new PlayerServiceWrapper();
                return _playerService;
            }
        }

        public static SpawnServiceWrapper Spawn
        {
            get
            {
                if (_spawnService == null)
                    _spawnService = new SpawnServiceWrapper();
                return _spawnService;
            }
        }

        public static LevelServiceWrapper Level
        {
            get
            {
                if (_levelService == null)
                    _levelService = new LevelServiceWrapper();
                return _levelService;
            }
        }

        public static AchievementServiceWrapper Achievements
        {
            get
            {
                if (_achievementService == null)
                    _achievementService = new AchievementServiceWrapper();
                return _achievementService;
            }
        }

        public static AchievementCollection AchievementList
        {
            get
            {
                if (_achievementCollection == null)
                    _achievementCollection = new AchievementCollection();
                return _achievementCollection;
            }
        }

        public static GameState State => new GameState();

        internal static void UpdateBoardCache(Board board)
        {
            _cachedBoard = board;
            _cachedActivity = board?.mApp;
            _cachedAudio = _cachedActivity?.m_audioService;
        }
    }
}