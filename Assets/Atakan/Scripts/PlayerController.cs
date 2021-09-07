using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.InGame)
        {
            transform.position += transform.forward * (Time.deltaTime * speed);
        }
        if(StackManager.Instance.parent.childCount == 0)
        {
            GameManager.Instance.gameState = GameManager.GameState.GameOver;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Time.timeScale = 1;
        }
    }

}
