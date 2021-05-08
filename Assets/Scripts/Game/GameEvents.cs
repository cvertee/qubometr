using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Assets.Scripts.Game
{
    public static class GameEvents
    {
        public static UnityEvent onPlayerDeath = new UnityEvent();
        public static UnityEvent onEnemyAlert = new UnityEvent();
        public static UnityEvent onHealthRestored = new UnityEvent();
        public static UnityEvent onPlayerReceivedDamage = new UnityEvent();
        public static UnityEvent onCoinPickup = new UnityEvent();
    }
}
