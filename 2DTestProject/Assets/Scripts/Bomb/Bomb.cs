using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Animator bombAnim;
    private Collider2D coll;
    private Rigidbody2D rbBoob;

    public float startTime;
    public float waitTime;
    public float bombForce;

    [Header("Check")]
    public float radius;
    public LayerMask targetLayer;

    void Start()
    {
        bombAnim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        rbBoob = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    void Update()
    {
        if(Time.time > startTime + waitTime)
        {
            bombAnim.Play("Bomb_explotion");
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Explosion() //Animation event, check the starting time and ending time of the bomb.
    {
        coll.enabled = false;
        Collider2D[] aroundObjects = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        rbBoob.gravityScale = 0;

        foreach (var item in aroundObjects)
        {
            Vector3 objPos = transform.position - item.transform.position;

            item.GetComponent<Rigidbody2D>().AddForce((-objPos + Vector3.up) * bombForce, ForceMode2D.Impulse);
            
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }


}
