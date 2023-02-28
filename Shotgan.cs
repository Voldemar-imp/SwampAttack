using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgan : Weapon
{
    private int _bulletCount = 5;
    private float _spread = 0.3f;

    public override void Shoot(Transform shootPoint)
    {
        if (Time.timeScale != 0)
        {
            int spreadCount = 2;

            for (float i = - _spread; i < _spread; i+= (_spread* spreadCount) /_bulletCount) 
            {
                Instantiate(Bullet, shootPoint.position + new Vector3(0, i, 0), Quaternion.identity);
            }
        }
    }
}
