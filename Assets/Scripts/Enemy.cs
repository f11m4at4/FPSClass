using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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


    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;

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
        if(isLadder)
        {
            return;
        }
        // agent 가 비활성화 되어 있다면
        if (agent.enabled == false)
        {
            // 활성화 시켜주자
            agent.enabled = true;
        }

        // 이동한다.
        Vector3 dir = target.position - transform.position;
        float distance = dir.magnitude;
        //dir.y = 0;

        //cc.SimpleMove(dir.normalized * speed);

        //// 회전
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
        agent.destination = target.position;

        if(distance < attackRange)
        {
            mState = EnemyState.Attack;
            agent.enabled = false;
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

    private void OnTriggerEnter(Collider other)
    {
        // 만약 부딪힌 녀석이 OffMeshLink
        if (other.tag == "OffMeshLink" && isLadder == false)
        {
            agent.enabled = false;
            StartCoroutine(LadderUp(other.transform.parent));
        }
    }

    bool isLadder = false;
    IEnumerator LadderUp(Transform parent)
    {
        isLadder = true;
        Vector3 targetPosition = parent.Find("OffMeshEnd").position;

        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 2 * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                break;
            }
            yield return null;
        }

        transform.position = targetPosition;
        agent.enabled = true;
        isLadder = false;
    }
}
