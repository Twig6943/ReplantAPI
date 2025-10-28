using Il2CppReloaded.Gameplay;

namespace ReplantAPI.Services
{
    public class SpawnServiceWrapper
    {
        public Zombie Zombie(ZombieType type, int row = -1, int fromWave = 0)
        {
            var board = Core.ReplantAPI.Board;
            if (board == null)
                return null;

            if (row == -1)
                return board.AddZombie(type, fromWave, true);
            else
                return board.AddZombieInRow(type, row, fromWave, true);
        }

        public Zombie RandomZombie(int row = -1)
        {
            var random = new System.Random();
            var zombieTypes = System.Enum.GetValues(typeof(ZombieType));
            var randomType = (ZombieType)zombieTypes.GetValue(random.Next(1, 24));
            return Zombie(randomType, row);
        }

        public Plant Plant(SeedType type, int row, int col, SeedType imitater = SeedType.None)
        {
            var board = Core.ReplantAPI.Board;
            if (board == null)
                return null;

            return board.AddPlant(col, row, type, imitater);
        }

        public void TriggerWave()
        {
            var board = Core.ReplantAPI.Board;
            if (board == null)
                return;

            board.SpawnZombieWave();
        }
    }
}