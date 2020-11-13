using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour, IPool
{
    private const int PreloadQty = 20;
    
    [SerializeField] private GameObject _bulletPrefab;
    private Stack<Bullet> _inactiveObjects;

    private void Awake()
    {
        _inactiveObjects = new Stack<Bullet>(PreloadQty);
        
    }
    
    public Bullet Spawn(Vector3 position)
    {
        if (_inactiveObjects.Count > 0)
        {
            Bullet b = _inactiveObjects.Pop();
            b.transform.position = position;
            b.transform.rotation = Quaternion.identity;
            b.gameObject.SetActive(true);
            return b;
        }
        else
        {
            return Instantiate(_bulletPrefab, position, Quaternion.identity).GetComponent<Bullet>();
        }
    }


    public void HideToPool(Bullet b)
    {
        b.gameObject.SetActive(false);
        _inactiveObjects.Push(b);
    }

    public void Preload(int qty)
    {
        for (int i = 0; i < qty; i++)
        {
            GameObject newBullet = Instantiate(_bulletPrefab);
            HideToPool(newBullet.GetComponent<Bullet>());
        }
    }
}
