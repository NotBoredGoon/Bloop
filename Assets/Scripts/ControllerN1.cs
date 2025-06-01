using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerN1 : MonoBehaviour
{

    [SerializeField] private GameObject device;
    [SerializeField] private SpriteRenderer buttonRef;
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;

    void Start()
    {
        buttonRef.color = offColor;
    }

    void OnTriggerEnter2D(Collider2D col)  
    {
        if (col.GetComponent<BoxCollider2D>().gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            device.GetComponent<SpriteRenderer>().enabled = false;
            device.GetComponent<BoxCollider2D>().enabled = false;
            buttonRef.color = onColor;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<BoxCollider2D>().gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            device.GetComponent<SpriteRenderer>().enabled = true;
            device.GetComponent<BoxCollider2D>().enabled = true;
            buttonRef.color = offColor;
        }
    }
}
