//@Author: Teodor Tysklind / FutureGames / Teodor.Tysklind@FutureGames.nu

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private GameObject snakePartPrefab;
    private LinkedList<GameObject> snakeParts;

    [NonSerialized] public bool isAlive;
    
    private Vector2 moveDirection;
    private const float moveSpeed = 1f;
    private const float updateInterval = 0.2f;
    private int numberOfParts = 5;

    private void Start()
    {
        isAlive = true;
        
        for(int i = 0 ; i < numberOfParts ; i++)
            AddPart();
        
        moveDirection = Vector2.left;
        StartCoroutine(Move());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDirection = Vector2.left;
        else if(Input.GetKeyDown(KeyCode.RightArrow))
            moveDirection = Vector2.right;
        else if(Input.GetKeyDown(KeyCode.DownArrow))
            moveDirection = Vector2.down;
        else if(Input.GetKeyDown(KeyCode.UpArrow))
            moveDirection = Vector2.up;
    }

    private IEnumerator Move()
    {
        Vector3 cachedPos = Vector3.zero;
        LinkedListNode<GameObject> currentPart = snakeParts.First;

        while (isAlive)
        {
            if (currentPart == snakeParts.First)
            {
                cachedPos = currentPart.Value.transform.position;
                Vector3 displacement = moveDirection * moveSpeed ;
                Vector3 newPos = currentPart.Value.transform.position + displacement;
                currentPart.Value.transform.position = newPos;

                if (currentPart.Next == null)
                {
                    yield return new WaitForSeconds(updateInterval);
                    continue;
                }

                currentPart = currentPart.Next;
            }
            else if (currentPart == snakeParts.Last)
            {
                currentPart.Value.transform.position = cachedPos;
                currentPart = snakeParts.First;
                yield return new WaitForSeconds(updateInterval);
            }
            else
            {
                Vector3 moveTo = cachedPos;
                cachedPos = currentPart.Value.transform.position;
                currentPart.Value.transform.position = moveTo;
                currentPart = currentPart.Next;
            }
        }
        
        Camera.main.backgroundColor = Color.black;
        Debug.Log("DED");
    }

    private void AddPart()
    {
        GameObject snakePart = Instantiate(snakePartPrefab);

        if (snakeParts == null)
        {
            snakeParts = new LinkedList<GameObject>();
            snakeParts.AddFirst(snakePart);
        }
        else
        {
            snakeParts.AddLast(snakePart);
        }
    }
}
