using System.Collections.Generic;
using System.Linq;
using Il2CppReloaded.Gameplay;

namespace ReplantAPI.Collections
{
    public class CoinCollection
    {
        public List<Coin> GetAll()
        {
            var board = Core.ReplantAPI.Board;
            if (board == null || board.m_coins == null)
                return new List<Coin>();

            var coins = new List<Coin>();
            for (int i = 0; i < board.m_coins.Count; i++)
            {
                var coin = board.m_coins[i];
                if (coin != null)
                    coins.Add(coin);
            }
            return coins;
        }

        public void CollectAll()
        {
            foreach (var coin in GetAll())
                coin.Die();
        }

        public int Count()
        {
            return GetAll().Count;
        }
    }

    public class ProjectileCollection
    {
        public List<Projectile> GetAll()
        {
            var board = Core.ReplantAPI.Board;
            if (board == null || board.m_projectiles == null)
                return new List<Projectile>();

            var projectiles = new List<Projectile>();
            for (int i = 0; i < board.m_projectiles.Count; i++)
            {
                var projectile = board.m_projectiles[i];
                if (projectile != null && !projectile.mDead)
                    projectiles.Add(projectile);
            }
            return projectiles;
        }

        public void RemoveAll()
        {
            foreach (var projectile in GetAll())
                projectile.Die();
        }

        public int Count()
        {
            return GetAll().Count;
        }
    }
}