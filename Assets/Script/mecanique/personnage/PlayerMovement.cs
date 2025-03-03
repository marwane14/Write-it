using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.5f;           // Vitesse de d�placement
    private Rigidbody2D rb;              // Composant Rigidbody2D du personnage
    private Vector2 dir;                 // Stocke les valeurs de d�placement
    private SPUM_Prefabs spum;           // R�f�rence � SPUM_Prefabs
    private Animator animator;           // R�f�rence � l'Animator

    void Start()
    {
        // R�cup�ration du composant Rigidbody2D attach� � l'objet
        rb = GetComponent<Rigidbody2D>();

        // R�cup�ration du script SPUM_Prefabs attach� au m�me GameObject
        spum = GetComponent<SPUM_Prefabs>();

        // R�cup�ration de l'Animator qui utilise SpumController
        if (spum != null)
        {
            animator = spum._anim;
        }
    }

    void Update()
    {
        // R�cup�ration des entr�es utilisateur
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        // V�rification du d�placement
        if (animator != null)
        {
            bool isMoving = dir.sqrMagnitude > 0;
            animator.SetBool("1_Move", isMoving);

            // Si le joueur se d�place � gauche, on force la direction de l'animation
            if (dir.x < 0)
            {
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
            }
            else
            {
                animator.SetFloat("Horizontal", dir.x);
                animator.SetFloat("Vertical", dir.y);
            }
        }
    }

    void FixedUpdate()
    {
        // D�placement du joueur en utilisant MovePosition dans FixedUpdate pour la physique
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}
