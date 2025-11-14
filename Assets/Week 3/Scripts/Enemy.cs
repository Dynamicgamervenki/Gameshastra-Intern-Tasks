using DG.Tweening;
using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    private GameObject target;
    private float distanceBtwEnemyAndTarget;
    private int noOfSpawnedBullets = 0;

    [SerializeField] private float minAttackDistance = 5.0f;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private Transform SpawnTransform;
    [SerializeField] private float shootTimer = 1.0f;


    private void Start()
    {
        target = FindFirstObjectByType<Player2D>().gameObject;
        StartCoroutine(AttackAfterDelay());
    }


    IEnumerator AttackAfterDelay()
    {
        while(target)
        {
            Attack();
            yield return new WaitForSeconds(shootTimer);
        }
    }

    private void Attack()
    {
        if (target == null || BulletPrefab == null || SpawnTransform == null)
            return;
        
        distanceBtwEnemyAndTarget = Vector3.Distance(transform.position,target.transform.position);
        if(distanceBtwEnemyAndTarget < minAttackDistance)
        {
            //GameObject bullet = Instantiate(BulletPrefab,SpawnTransform.position,SpawnTransform.rotation);
            GameObject bullet =  ObjectPool.Instance.GetPooledObject();
            if(bullet == null)
                return;
            bullet.transform.position = SpawnTransform.position;
            bullet.transform.rotation = SpawnTransform.rotation;
            bullet.SetActive(true);
            noOfSpawnedBullets++;
            bullet.name = "Bullet : " + noOfSpawnedBullets; 
        }
    }

    private void Patrol()
    {

    }
}

