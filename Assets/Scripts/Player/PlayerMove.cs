using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public event Action<bool> onPlayerIdleChange;

    [Header("Setup")]
    [SerializeField] private float speed;
    [SerializeField] private string starTag;

    private float currenSpeed = 0;

    private Vector3 mousePos;

    private Rigidbody2D rb;


    [Header("References")]
    [SerializeField] private PlayerJump jump;
    [SerializeField] private StarsCounterManager starsCounterManager;
    [SerializeField] private ButtonData buttonData;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currenSpeed = speed; 
    }

    public void CheckMousePos() 
    {
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    public void StartMoveTouch() 
    {
        if (mousePos.x > 0.5)
        {
            rb.AddForce(new Vector2(speed, 0), ForceMode2D.Force);
        }

        if (mousePos.x < 0.5)
        {
            rb.AddForce(new Vector2(-speed, 0), ForceMode2D.Force);
        }
    }

    public void CorrectPlayerSpeed() 
    {
        if(jump.isGrounded)
        {
            onPlayerIdleChange?.Invoke(true);
            speed = 0;
            rb.AddForce(new Vector2(-0.8f, 0), ForceMode2D.Force);
        }

        else 
        {
            speed = currenSpeed;
            onPlayerIdleChange?.Invoke(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(starTag)) 
        {
            Destroy(collision.gameObject);
            MultiplyStars();
        }
    }

    private void MultiplyStars() 
    {
        if (buttonData.LoadInfo("X2"))
        {
            starsCounterManager.AddStars(2);
        }

        if (!buttonData.LoadInfo("X2")) 
        {
            starsCounterManager.AddStars(1);
        }
    }
}
