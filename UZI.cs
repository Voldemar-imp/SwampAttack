using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UZI : Weapon
{
    private int _bulletCount = 10;
    private float _delayBetweenShots = 0.06f;
    private float _spread = 0.1f;
    private bool _isShooting = false;

    private void OnEnable()
    {
        _isShooting = false;
    }

    private IEnumerator Shooting(Transform shootPoint)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_delayBetweenShots);

        for (int i = 0; i < _bulletCount; i++) 
        {
            Vector3 shootPosition = shootPoint.position + new Vector3(0, Random.Range(-_spread, _spread), 0);
            Instantiate(Bullet, shootPosition, Quaternion.identity);
            yield return waitForSeconds;
        }

        _isShooting = false;
    }

    public override void Shoot(Transform shootPoint)
    {
        if (Time.timeScale != 0)
        {
            if (_isShooting == false)
            {
                _isShooting = true;
                StartCoroutine(Shooting(shootPoint));
            }
        }
    }  
}
