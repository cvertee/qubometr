using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game
{
    public static class GameData
    {
        //private static float hp = 50;
        //public static float HP
        //{
        //    get
        //    {
        //        return hp;
        //    }

        //    set
        //    {
        //        if (value > maxHp)
        //        {
        //            value = maxHp;
        //        }

        //        hp = value;
        //    }
        //}
        //public static float maxHp = 50;
        //public static int coins;
        //public static string sceneName;
        //public static string videoToLoad;

        //public static void InitFromSaveData(SaveData saveData)
        //{
        //    hp = saveData.hp;
        //    coins = saveData.coins;
        //    sceneName = saveData.sceneName;
        //}

        //public static SaveData GetSaveData()
        //{
        //    return new SaveData
        //    {
        //        hp = HP,
        //        coins = coins,
        //        sceneName = sceneName
        //    };
        //}

        private static SaveData data;
        public static SaveData Data
        {
            get
            {
                if (data == null)
                {
                    data = new SaveData
                    {
                        coins = 0,
                        hp = 50,
                        
                    };
                }

                return data;
            }

            set
            {
                data = value;
            }
        }
    }
}
