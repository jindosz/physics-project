using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody2D rigid;

    public float maxForce = 20;

    private float angle = 0;
    private float force = 10;

    float velocity_X = 0f;
    float velocity_Y = 0f;

    bool canJump = true;

    void Start()
{
    rigid = GetComponent<Rigidbody2D>();
    if (rigid == null)
    {
        Debug.LogError("nonono");
    }
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canJump = true;
            transform.position = new Vector2(0, 0);
            rigid.velocity = Vector2.zero;
        }

        if (Mathf.Abs(force) <= maxForce)
            force += Input.GetAxisRaw("Horizontal")* 0.1f;
        else if (force < 0)
            force = -maxForce;
        else if (force > 0)
            force = maxForce;

        force = Mathf.Round(force * 10f) / 10f;

        if (angle > 90)
            angle = 90;
        else if (angle < 0)
            angle = 0;
        else
            angle += Input.GetAxisRaw("Vertical")*0.2f;

        angle = Mathf.Round(angle * 10f) / 10f;

        if(rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("floor"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    canJump = true;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump == true)
            {
                velocity_X = force * Mathf.Cos(angle * Mathf.Deg2Rad);
                velocity_Y = Mathf.Abs(force) * Mathf.Sin(angle * Mathf.Deg2Rad);

                Vector2 addForce = new Vector2(velocity_X, velocity_Y);

                canJump = false;
                rigid.AddForce(addForce, ForceMode2D.Impulse);
            }
        }
    }

    

    void OnGUI()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.Label(new Rect(screenPosition.x - 25, screenPosition.y - 25, 100, 50), $"Force: {force}");
        GUI.Label(new Rect(screenPosition.x - 25, screenPosition.y - 40, 100, 50), $"angle: {angle}");
    }
}
