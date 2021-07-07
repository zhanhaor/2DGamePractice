using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerController player;

    public bool gameOver;
    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        player = FindObjectOfType<PlayerController>();


    }

    public void Update()
    {
        gameOver = player.isDead;
        UIManager.instance.GameOverUI(gameOver);
    }
}
