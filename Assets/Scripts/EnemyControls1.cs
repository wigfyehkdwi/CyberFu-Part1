using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls1 : MonoBehaviour
{
    public float speed = 5;
    public float attackingDistance = 1;
    public Vector3 direction;

    private Animator animatorEnemy;
    private Rigidbody rigidbodyEnemy;
    private Transform target;
    private bool isFollowingTarget;
    private bool isAttackingTarget;
    private float chasingPlayer = 0.01f;
    private float currentAttackingTime;
    private float maxAttackingTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        isFollowingTarget = true;
        currentAttackingTime = maxAttackingTime;
        animatorEnemy = GetComponent<animatorEnemy>();
        rigidbodyEnemy = GetComponent<rigidbodyEnemy>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
