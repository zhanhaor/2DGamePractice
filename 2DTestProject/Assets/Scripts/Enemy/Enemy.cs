using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyBaseState currentState;  // FSM state switch var

    public Animator enemyAnim;
    public int enemyAnimState;
    [Header("Movement")]
    public float speed;
    public Transform pointA, pointB;
    public Transform targetPoint;

    public List<Transform> attackList = new List<Transform>();// List for searching Player or Bombs in the range

    public PatrolState patrolState = new PatrolState();// FSM state
    public AttackState attackState = new AttackState();// FSM state

    public virtual void Init()
    {
        enemyAnim = GetComponent<Animator>();
    }

    public void Awake()
    {
        Init();
    }

    void Start()
    {
        TransitionToState(patrolState);
    }

    void Update()
    {
        currentState.OnUpdate(this);
        enemyAnim.SetInteger("state", enemyAnimState);

    }

    public void TransitionToState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);

    }

    public void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        FlipDirection();
    }

    public void AttackAction() // Attack Player
    {
        Debug.Log("Base attack");
    }

    public virtual void SkillAction() //Use skill to the bomb , virtual允许子类访问
    {
        Debug.Log("Skill attack");
    }

    public void FlipDirection()//flip enemy direction
    {
        if (transform.position.x < targetPoint.position.x)
            transform.rotation = Quaternion.Euler(0f,0f,0f);
        else
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    }

    public void SwitchPoint()
    {
        if(Mathf.Abs(pointA.position.x - transform.position.x)>Mathf.Abs(pointB.position.x - transform.position.x))
        {
            targetPoint = pointA;
        }
        else
        {
            targetPoint = pointB;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(!attackList.Contains(collision.transform))
        {
            attackList.Add(collision.transform);
        }
            

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        attackList.Remove(collision.transform);
    }
}
