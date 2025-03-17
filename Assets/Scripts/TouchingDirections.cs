using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;

// Uses the collider to check directions to see if the object is currently on the ground, touching the wall, or touching the ceiling
public class TouchingDirections : MonoBehaviour
{
    public float groundDistance = 0.05f;
    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    public ContactFilter2D castFilter;
    Animator animator;

  
    [SerializeField]
    private bool _isGrounded = true;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

  

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
       
    }
}
