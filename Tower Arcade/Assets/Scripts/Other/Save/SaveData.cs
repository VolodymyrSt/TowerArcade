using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class SaveData
    {
        public int CoinCurrency;
        public int CurrentUnlockedEntrance;
        public float LevelVoluem;
        public float MenuVoluem;
        public float MouseSensivity;
        public Dictionary<string, bool> ShopItems = new();
        public Dictionary<string, string> InventoryItems = new();
        public Dictionary<string, bool> LevelEntances = new();
        public List<TowerSO> TowerGenerals = new();
    }
}

