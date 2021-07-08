using UnityEngine;
using Zenject;

namespace Game
{
    public class CoinSpawner
    {
        private Coin.Factory coinFactory;
        private AudioManager audioManager;
        
        [Inject]
        public void Init(Coin.Factory coinFactory, AudioManager audioManager)
        {
            this.coinFactory = coinFactory;
            this.audioManager = audioManager;
        }

        public void Spawn(Vector3 position)
        {
            // TODO: USE POOL!!!!!!!!!!!
            //Instantiate(Resources.Load("Prefabs/Coin"),
            //    transform.position + new Vector3(Random.Range(0, 7), 0), Quaternion.identity);

            var coin = coinFactory.Create(audioManager);
            coin.transform.position = position;
        }
    }
}