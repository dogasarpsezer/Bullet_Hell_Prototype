using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerPivot;
    [SerializeField] private Rigidbody2D rigidbody2D;
    Vector3 direction = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }        
        
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
        }

        var movement = (speed * Time.deltaTime) * direction;
        var newPos = transform.position + movement;
        rigidbody2D.MovePosition(newPos);
        
        animator.SetFloat("Speed",movement.magnitude);
        animator.SetInteger("Direction", 1);

        var faceDirection = playerPivot.localScale.x / Mathf.Abs(playerPivot.localScale.x);
        if ( (faceDirection < 0 && direction.x > 0) || (faceDirection > 0 && direction.x < 0))
        {
            animator.SetInteger("Direction", -1);
        }
    }
}
