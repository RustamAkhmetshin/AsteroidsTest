
using UnityEngine;

public interface IPool
{
    Bullet Spawn(Vector3 position);
    void HideToPool(Bullet g);
    void Preload(int qty);
}
