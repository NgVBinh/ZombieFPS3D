using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPooling : MonoBehaviour
{
    public GameObject bulletPrefab;
    private int size = 10;
    private List<GameObject> bulletPool;
    // Start is called before the first frame update
    private void Awake()
    {
        InitialPool();
    }

    public void InitialPool()
    {
        bulletPool = new List<GameObject>();
        for(int i = 0; i < size; i++)
        {
            bulletPool.Add(CreateNewBullet());
        }
    }

    public GameObject GetBulletInPool()
    {
        foreach(GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy) return bullet;
        }

        // them vien dan moi (neu het dan)
        GameObject newbullet = CreateNewBullet();
        bulletPool.Add(newbullet);
        return newbullet;
    }
    public GameObject CreateNewBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.SetParent(transform);
        bullet.SetActive(false);
        return bullet;
    }
    public static void ReturnBulletToPool(GameObject bullet)
    {
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.transform.rotation = Quaternion.identity;
        bullet.transform.position = Vector3.zero;
        bullet.SetActive(false);

    }

}
