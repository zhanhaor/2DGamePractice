using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player get hurt");
            collision.GetComponent<IDamageable>().GetHit(1);
        }

        if(collision.CompareTag("Bomb"))
        {

        }


    }
}
