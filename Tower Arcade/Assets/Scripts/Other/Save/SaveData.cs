using System;
using System.Collections.Generic;

namespace Game
{
    [Serializable]
    public class SaveData
    {
        public SaveData()
        {
            ShopItems = new();
            LevelEntances = new();
            InventoryItems = new();
            CoinCurrency = 0;
            MouseSensivity = 10;
            LevelVoluem = 1;
            MenuVoluem = 1;
            CurrentUnlockedEntrance = 0;
            TowerGenerals = new();
        }

        public int CoinCurrency;
        public int CurrentUnlockedEntrance;
        public float LevelVoluem;
        public float MenuVoluem;
        public float MouseSensivity;
        public Dictionary<string, bool> ShopItems;
        public Dictionary<string, string> InventoryItems;
        public Dictionary<string, bool> LevelEntances;
        public List<TowerSO> TowerGenerals;
    }
}

