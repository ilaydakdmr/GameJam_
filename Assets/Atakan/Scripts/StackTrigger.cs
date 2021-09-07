using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTrigger : MonoBehaviour
{
    public bool inPlayer;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pick" && inPlayer)
        {
            StackManager.Instance.PickUp(collision.gameObject, true, "Untagged");
        }
    }
}
