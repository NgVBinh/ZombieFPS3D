using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameAreaCollision : MonoBehaviour
{
    private ZombieController zombie;
    [SerializeField] private GameObject ignitePrefab;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("player");
            other.GetComponent<PlayerManager>().SetupIgnite(true, 2);
        }

        if(other.CompareTag("Zombie"))
        {
            //Debug.Log( other.gameObject.GetComponentInParent<ZombieController>().weaponName);
            other.GetComponentInParent<ZombieController>().SetupIgnite(true, 1.5f);

        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>() != null)
        {
            Debug.Log("Player Exit");
            other.GetComponent<PlayerManager>().SetupIgnite(false, 0);
        }

        if (other.CompareTag("Zombie"))
        {
            //Debug.Log( other.gameObject.GetComponentInParent<ZombieController>().weaponName);
            zombie = other.GetComponentInParent<ZombieController>();

            GameObject newFlame = Instantiate(ignitePrefab,zombie.transform.position + new Vector3(0,0.4f,0),Quaternion.identity,zombie.transform);
            StartCoroutine(ZombieFinishIgnite(newFlame, zombie, 4));
            //other.GetComponentInParent<ZombieController>().SetupIgnite(false, 0f);

        }
    }

    private IEnumerator ZombieFinishIgnite(GameObject flame,ZombieController zombie, float timer)
    {
        yield return new WaitForSeconds(timer);
        zombie.SetupIgnite(false, 0);
        Destroy(flame);
    }
}
