using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isDoorOpen = false;
    private bool doorIsClose = false;
    private SpriteRenderer sr;
    [SerializeField] private Sprite openedDoor;
    [SerializeField] private Sprite closedDoor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (doorIsClose && Input.GetKeyDown(KeyCode.E))
        {
            Switch();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        doorIsClose = true;
    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    doorIsClose = false;
    //}

    private void Switch()
    {
        isDoorOpen = !isDoorOpen;
        sr.sprite = isDoorOpen ? closedDoor : openedDoor;
        if(isDoorOpen)
        {
            GetComponent<BoxCollider2D>().enabled = false;

        }
        else
            GetComponent<BoxCollider2D>().enabled = true;

    }
}
