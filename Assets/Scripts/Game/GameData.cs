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
        private int hp = 50;
        public int HP
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
        public int maxHp = 50;
        public int coins;
        public string sceneName;
    }
}
