//@Author: Teodor Tysklind / FutureGames / Teodor.Tysklind@FutureGames.nu

using UnityEngine;

public class SnakePart : MonoBehaviour
{
    private Snake snake;
    
    private void Start()
    {
        snake = FindObjectOfType<Snake>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time > 1f)
            snake.isAlive = false;
    }
}
