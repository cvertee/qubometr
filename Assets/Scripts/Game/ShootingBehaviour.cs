using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    public float fireInterval;
    public GameObject bulletPrefab;

    private void OnEnable() => StartCoroutine(ShootCoroutine());
    private void OnDisable() => StopCoroutine(nameof(ShootCoroutine));
    
    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            var transform1 = transform;
            var bulletGameObject = Instantiate(bulletPrefab, transform1.position, transform1.rotation);
            var bullet = bulletGameObject.GetComponent<Bullet>();
            bullet.direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            bullet.owner = this.gameObject;
            yield return new WaitForSeconds(fireInterval);
        }
    }
}
