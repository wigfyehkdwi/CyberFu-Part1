using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControls : MonoBehaviour
{
    public float speed = 5;
    public float attackingDistance = 1;
    public Vector3 direction;

    private Animator animatorEnemy;
    private Rigidbody rigidbodyEnemy;
    private Transform target;
    public bool isFollowingTarget;
    public bool isAttackingTarget;
    private float chasingPlayer = 0.01f;
    private float currentAttackingTime;
    private float maxAttackingTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        isFollowingTarget = true;
        currentAttackingTime = maxAttackingTime;
        animatorEnemy = GetComponent<Animator>();
        rigidbodyEnemy = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FollowTarget()
    {
        if (!isFollowingTarget)
        {
            return;
        }

        if (Vector3.Distance(transform.position, target.position) >= attackingDistance)
        {
            direction = target.position - transform.position;
            direction.y = 0;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 100);

            if (rigidbodyEnemy.velocity.sqrMagnitude != 0)
            {
                rigidbodyEnemy.velocity = transform.forward * speed;
                animatorEnemy.SetBool("Walk", true);
            }
        }
        else if (Vector3.Distance(transform.position, target.position) <= attackingDistance)
        {
            rigidbodyEnemy.velocity = Vector3.zero;
            animatorEnemy.SetBool("Walk", false);
            isFollowingTarget = false;
            isAttackingTarget = true;
        }

        direction = target.position - transform.position;
        direction.y = 0;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 100);
    }

    void Attack()
    {
        if (!isAttackingTarget)
        {
            return;
        }

        currentAttackingTime += Time.deltaTime;
        if (currentAttackingTime > maxAttackingTime)
        {
            currentAttackingTime = 0f;
            animatorEnemy.SetTrigger("Attack" + Random.Range(1, 4));
        }

       if (Vector3.Distance(transform.position, target.position) > attackingDistance + chasingPlayer)
       {
           isAttackingTarget = false;
           isFollowingTarget = true;
       }
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void Update()
    {
        Attack();
    }
}
