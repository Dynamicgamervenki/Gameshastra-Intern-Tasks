using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<EnemyBase> Enemies;
    //public List<EnemyBase> PatrolEnemies;

    public Dictionary<EnemyBase, SplinePatrol> enemyData = new Dictionary<EnemyBase, SplinePatrol>();

    bool isPatroling = false;

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
        }
    }

    public void AddToEnemyList(EnemyBase enemy)
    {
        Enemies.Add(enemy);
        if (enemy is IPatrol p)
        {
            //PatrolEnemies.Add(enemy);
            //SplinePatrol splinePatrol = enemy.GetComponent<SplinePatrol>();
            //splinePatrol.Init(p.GetSplineContainer(),p.GetPatrolSpeed());
            enemyData.Add(enemy, enemy.GetComponent<SplinePatrol>());
            enemy.GetComponent<SplinePatrol>().Init(p.GetSplineContainer(),p.GetPatrolSpeed());
        }
    }

    public void Patrol(EnemyBase enemy)
    {
        isPatroling = true;
     //   SplinePatrol splinePatrol = enemy.GetComponent<SplinePatrol>();
      //  splinePatrol.MoveAlongSpline(enemy.transform);
        enemyData[enemy].MoveAlongSpline(enemy.transform);
    }
    
}
