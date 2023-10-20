using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool on = false;
    private bool buttonIsClose = false;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && buttonIsClose)
        {
            Debug.Log("Button clicked!");
            on = !on;
            sr.color = on ? Color.green : Color.red;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        buttonIsClose = true;
    }
}
