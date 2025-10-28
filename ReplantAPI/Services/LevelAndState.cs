using Il2CppReloaded.Gameplay;

namespace ReplantAPI.Services
{
    public class LevelServiceWrapper
    {
        public int GetCurrentWave()
        {
            var board = Core.ReplantAPI.Board;
            if (board == null)
                return 0;
            return board.mCurrentWave;
        }

        public int GetTotalWaves()
        {
            var board = Core.ReplantAPI.Board;
            if (board == null)
                return 0;
            return board.mNumWaves;
        }

        public bool IsFinalWave()
        {
            var board = Core.ReplantAPI.Board;
            if (board == null)
                return false;
            return board.mCurrentWave >= board.mNumWaves - 1;
        }
    }
}

namespace ReplantAPI.Core
{
    public class GameState
    {
        public BackgroundType GetBackground()
        {
            var board = ReplantAPI.Board;
            if (board == null)
                return (BackgroundType)(-1);
            return board.mBackground;
        }

        public bool IsNight()
        {
            var board = ReplantAPI.Board;
            if (board == null)
                return false;
            return board.StageIsNight();
        }

        public bool HasPool()
        {
            var board = ReplantAPI.Board;
            if (board == null)
                return false;
            return board.StageHasPool();
        }

        public int GetNumRows()
        {
            var board = ReplantAPI.Board;
            if (board == null)
                return 0;
            return board.GetNumRows();
        }
    }
}