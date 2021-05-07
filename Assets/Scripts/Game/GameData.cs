using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Game
{
    public static class GameData
    {
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
                        sceneName = SceneManager.GetActiveScene().name
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
