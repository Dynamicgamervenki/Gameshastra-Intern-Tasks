using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemtAnimator : MonoBehaviour
{


    private const string IS_WALKING = "IsWalking";


    [SerializeField] private EnemyBase enemy;


    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, enemy.currentState == EnemyState.Patrol || enemy.currentState == EnemyState.Chase);
    }

}