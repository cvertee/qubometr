using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game
{
    class ShootingEnemy : Enemy
    {
        public GameObject bullet;

        private void Start()
        {
            StartCoroutine(ShootCoroutine());
        }

        IEnumerator ShootCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.5f);
                Instantiate(bullet);
            }
        }
    }
}
