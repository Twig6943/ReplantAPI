using System.Collections.Generic;
using System.Linq;
using Il2CppReloaded.Gameplay;

namespace ReplantAPI.Collections
{
    public class PlantCollection
    {
        public List<Plant> GetAll()
        {
            var board = Core.ReplantAPI.Board;
            if (board == null || board.m_plants == null)
                return new List<Plant>();

            var plants = new List<Plant>();
            for (int i = 0; i < board.m_plants.Count; i++)
            {
                var plant = board.m_plants[i];
                if (plant != null)
                    plants.Add(plant);
            }
            return plants;
        }

        public List<Plant> Where(System.Func<Plant, bool> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public int Count()
        {
            return GetAll().Count;
        }

        public int CountByType(SeedType type)
        {
            return GetAll().Count(p => p.mSeedType == type);
        }

        public List<Plant> InRow(int row)
        {
            return GetAll().Where(p => p.mRow == row).ToList();
        }

        public Plant At(int row, int col)
        {
            return GetAll().FirstOrDefault(p => p.mRow == row && p.mPlantCol == col);
        }

        public void RemoveAll()
        {
            foreach (var plant in GetAll())
                plant.Die();
        }

        public void MakeIndestructible()
        {
            foreach (var plant in GetAll())
            {
                plant.mPlantHealth = 999999;
                plant.mPlantMaxHealth = 999999;
            }
        }

        public void HealAll()
        {
            foreach (var plant in GetAll())
                plant.mPlantHealth = plant.mPlantMaxHealth;
        }

        public Plant GetRandom()
        {
            var plants = GetAll();
            if (plants.Count == 0)
                return null;
            var random = new System.Random();
            return plants[random.Next(plants.Count)];
        }
    }
}