using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game
{
    [Serializable]
    public class GameData : Singleton<GameData>
    {
        private float hp = 50;
        public float HP
        {
            get
            {
                return hp;
            }

            set
            {
                if (value > maxHp)
                {
                    value = maxHp;
                }

                hp = value;
            }
        }
        public float maxHp = 50;
        public int coins;
        public string sceneName;
        public string videoToLoad;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void InitFromSaveData(SaveData saveData)
        {
            hp = saveData.hp;
            coins = saveData.coins;
            sceneName = saveData.sceneName;
        }

        public SaveData GetSaveData()
        {
            return new SaveData
            {
                hp = HP,
                coins = coins,
                sceneName = sceneName
            };
        }

        public Player GetPlayer()
        {
            return FindObjectOfType<Player>();
        }
    }
}
