using System.Collections.Generic;
using System.Linq;
using Il2CppReloaded.Gameplay;

namespace ReplantAPI.Collections
{
    public class ZombieCollection
    {
        public List<Zombie> GetAll()
        {
            var board = Core.ReplantAPI.Board;
            if (board == null || board.m_zombies == null)
                return new List<Zombie>();

            var zombies = new List<Zombie>();
            for (int i = 0; i < board.m_zombies.Count; i++)
            {
                var zombie = board.m_zombies[i];
                if (zombie != null && !zombie.mDead)
                    zombies.Add(zombie);
            }
            return zombies;
        }

        public List<Zombie> Where(System.Func<Zombie, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public int Count()
        {
            return GetAll().Count;
        }

        public int CountByType(ZombieType type)
        {
            return GetAll().Count(z => z.mZombieType == type);
        }

        public List<Zombie> InRow(int row)
        {
            return GetAll().Where(z => z.mRow == row).ToList();
        }

        public List<Zombie> PastX(float x)
        {
            return GetAll().Where(z => z.mPosX < x).ToList();
        }

        public Zombie GetClosest()
        {
            var zombies = GetAll();
            if (zombies.Count == 0)
                return null;
            return zombies.OrderBy(z => z.mPosX).FirstOrDefault();
        }

        public void KillAll()
        {
            foreach (var zombie in GetAll())
            {
                zombie.TakeDamage(999999, 0);
            }
        }

        public void FreezeAll(int duration = 300)
        {
            foreach (var zombie in GetAll())
                zombie.mChilledCounter = duration;
        }
        public void SubtractSpeed(float multiplier)
        {
            foreach (Zombie zombie in this.GetAll())
            {
                zombie.mVelX -= multiplier;
            }
        }

        public void SetSpeed(float multiplier)
        {
            foreach (var zombie in GetAll())
                zombie.mVelX *= multiplier;
        }

        public Zombie GetRandom()
        {
            var zombies = GetAll();
            if (zombies.Count == 0)
                return null;
            var random = new System.Random();
            return zombies[random.Next(zombies.Count)];
        }
    }
}