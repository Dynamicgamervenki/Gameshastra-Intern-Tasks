using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    public List<EnemyBase> Enemies;
    public SerializedDictionary<EnemyBase, SplinePatrol> enemyData = new SerializedDictionary<EnemyBase, SplinePatrol>();
 //   bool isPatroling = false;

    private void Start()
    {
        Enemies = new List<EnemyBase>();
    }


    private void Update()
    {
        foreach(EnemyBase patrolEnemy in enemyData.Keys)
        {
            if(patrolEnemy.currentState == EnemyState.Patrol)
            {
                Patrol(patrolEnemy);
            }
            else
            {
                Debug.Log(patrolEnemy.name + " %: " + enemyData[patrolEnemy].GetCurrentDistancePercentage());
            }
        }
    }

    public void AddToEnemyList(EnemyBase enemy)
    {
        Enemies.Add(enemy);
        if (enemy is IPatrol p)
        {
            enemyData.Add(enemy, enemy.GetComponent<SplinePatrol>());
            if(enemy.TryGetComponent<SplinePatrol>(out SplinePatrol splinePatrol))
            {
                splinePatrol.Init(p.GetSplineContainer(),p.GetPatrolSpeed());
            }
            else
            {
                Debug.LogError("Attach SplinePatrol Script For Enemy If You Want The Enemy To Be Able To Patrol !");
            }
        }
    }

    public void Patrol(EnemyBase enemy)
    {
     //   isPatroling = true;
        enemyData[enemy].MoveAlongSpline(enemy.transform);
        if (enemyData[enemy] is IPatrol p)
        {
            p.IsPatroling = true;
          
        }
    }
    
}
