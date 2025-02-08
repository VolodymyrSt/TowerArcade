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
        public Dictionary<string, bool> ShopItems = new();
        public Dictionary<string, string> InventoryItems = new();
        public Dictionary<string, bool> LevelEntances = new();
    }
}

