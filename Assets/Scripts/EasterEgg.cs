using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EasterEgg : MonoBehaviour
{
    // public delegate void OnPlayerCollision();
    // public static event OnPlayerCollision onPlayerCollision;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<BoxCollider2D>().gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameObject.Find("Game").GetComponent<GameUtil>().SetEasterEggCollected();
            Destroy(gameObject);
        }
    }
}
