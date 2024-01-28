using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BulletInteractible
{
    bool onBulletHit(Bullet bullet);
}
