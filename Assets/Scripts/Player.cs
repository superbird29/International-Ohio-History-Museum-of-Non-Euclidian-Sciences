using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private DialogueUI dialogueUI;

    public float movementSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;
    private Animator animator;
    public float movementThreshold = 0.01f;

    public AudioSource walking;
        
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {

       
        //Does not allow any controls during dialogue, forcing the player to listen to the NPC
        if (!dialogueUI.IsOpen)
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");

            moveInput.Normalize();
            if (Input.GetButton("Interact"))
            {
                Interactable?.Interact(this);
            }
        }
    }


    void FixedUpdate()
    {
        rb2d.velocity = moveInput * movementSpeed;

        if (rb2d.velocity.magnitude > movementThreshold)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);

            if (!walking.isPlaying)
            {
                walking.Play();
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            walking.Stop();
            animator.SetFloat("LastInputX", animator.GetFloat("InputX"));
            animator.SetFloat("LastInputY", animator.GetFloat("InputY"));
        }

    }

}
