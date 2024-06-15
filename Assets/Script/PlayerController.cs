using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _BaseAnimator;

    // Player attachment animators
    public Animator _HairAnimator;
    public Animator _AccessoriesAnimator;
    public Animator _PantsAnimator;
    public Animator _ClothesAnimator;
 
    public Animator _ShoesAnimator;
    public float speed = 5f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _BaseAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement vector and apply velocity
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;
        _rb.velocity = movement * speed;

        // Update animator states based on movement
        bool isWalking = movement.x != 0 || movement.y != 0;
        if (isWalking)
        {
            _BaseAnimator.SetFloat("moveX", moveHorizontal);
            _BaseAnimator.SetFloat("moveY", moveVertical);
        }
        _BaseAnimator.SetBool("isWalking", isWalking);
        // Update attachment animators with the same parameters
        SyncAnimators(_HairAnimator, moveHorizontal, moveVertical, isWalking);
        SyncAnimators(_AccessoriesAnimator, moveHorizontal, moveVertical, isWalking);
        SyncAnimators(_PantsAnimator, moveHorizontal, moveVertical, isWalking);
        SyncAnimators(_ClothesAnimator, moveHorizontal, moveVertical, isWalking);
        SyncAnimators(_ShoesAnimator, moveHorizontal, moveVertical, isWalking);
    }

    // Method to sync parameters across animators
    void SyncAnimators(Animator animator, float moveX, float moveY, bool isWalking)
    {
        if (animator != null)
        {
            if (isWalking)
            {
                animator.SetFloat("moveX", moveX);
                animator.SetFloat("moveY", moveY);
            }
            animator.SetBool("isWalking", isWalking);
        }
    }

    // Simple way for interacting with shopkeeper as for now
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shopkeeper"))
        {
            // Trigger interaction with shopkeeper
        }
    }
}
