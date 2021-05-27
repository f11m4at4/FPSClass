using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Damage,
        Die
    }
    EnemyState mState = EnemyState.Idle;

    public float speed = 5;
    public float rotSpeed = 5;
    CharacterController cc;

    Transform target;

    public float idleDelayTime = 2;
    public float attackDelayTime = 2;
    public float damageDelayTime = 2;
    public float attackRange = 2;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();

        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        switch(mState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damage:
                Damage();
                break;
            case EnemyState.Die:
                Die();
                break;
        }
    }

    private void Idle()
    {
        currentTime += Time.deltaTime;
        if(currentTime > idleDelayTime)
        {
            currentTime = 0;
            mState = EnemyState.Move;
        }
    }

    private void Move()
    {
        Vector3 dir = target.position - transform.position;
        float distance = dir.magnitude;
        dir.y = 0;

        cc.SimpleMove(dir.normalized * speed);

        // È¸Àü
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);

        if(distance < attackRange)
        {
            mState = EnemyState.Attack;
        }
    }

    private void Attack()
    {
        currentTime += Time.deltaTime;
        if(currentTime > attackDelayTime)
        {
            currentTime = 0;
            print("Attack");
        }

        if(Vector3.Distance(target.position, transform.position) > attackRange)
        {
            mState = EnemyState.Move;
        }
    }
    private void Die()
    {
        
    }

    private void Damage()
    {
        
    }

}
