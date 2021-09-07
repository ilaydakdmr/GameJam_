using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencils : MonoBehaviour
{
    public int listIndex;
    public float scalepow;
    public bool moving;
    public Vector3 destiny;
    public float movingTime=0.2f;

    private float temptime;
    public Vector3 firstLocalPos;
    private void Update()
    {
        if (GameManager.Instance.gameState == GameManager.GameState.InGame)
        {
            var scale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z-Time.deltaTime*scalepow);
            ScaleAround(-transform.up,scale);
            if(transform.localScale.z <= 0)
            {
                kill();
            }
        }

        if(moving)
        {
            transform.localPosition = Vector3.Lerp(firstLocalPos, destiny, temptime/ movingTime );
            temptime += Time.deltaTime;
            if (temptime > movingTime) moving = false;
        }
    }

    private void kill()
    {
        StackManager.Instance.popUp(listIndex);
        Destroy(gameObject);
        StackManager.Instance.delayedSortList();
    }

    public void ScaleAround(Vector3 pivot, Vector3 newScale)
    {
        Vector3 A = transform.localPosition;
        Vector3 B = pivot;
 
        Vector3 C = A - B; // diff from object pivot to desired pivot/origin
 
        float RS = newScale.x / transform.localScale.x; // relataive scale factor
 
        // calc final position post-scale
        Vector3 FP = B + C * RS;
 
        // finally, actually perform the scale/translation
        transform.localScale = newScale;
        transform.localPosition = FP;
    }

    /* public void ScaleAround(GameObject target, Vector3 pivot, Vector3 newScale)
     {
     Vector3 A = target.transform.localPosition;
     Vector3 B = pivot;
 
     Vector3 C = A - B; // diff from object pivot to desired pivot/origin
 
     float RS = newScale.x / target.transform.localScale.x; // relataive scale factor
 
     // calc final position post-scale
     Vector3 FP = B + C * RS;
 
     // finally, actually perform the scale/translation
     target.transform.localScale = newScale;
     target.transform.localPosition = FP;
     }
     */
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube" && GetComponent<StackTrigger>().inPlayer)
        {
            kill();
        }
        if(collision.gameObject.tag == "FinishLevel")
        {
            GameManager.Instance.gameState = GameManager.GameState.Finish;
        }
    }
}
