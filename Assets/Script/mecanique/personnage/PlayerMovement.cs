using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.5f;           // Vitesse de déplacement
    private Rigidbody2D rb;              // Composant Rigidbody2D du personnage
    private Vector2 dir;                 // Stocke les valeurs de déplacement
    private SPUM_Prefabs spum;           // Référence à SPUM_Prefabs
    private Animator animator;           // Référence à l'Animator

    void Start()
    {
        // Récupération du composant Rigidbody2D attaché à l'objet
        rb = GetComponent<Rigidbody2D>();

        // Récupération du script SPUM_Prefabs attaché au même GameObject
        spum = GetComponent<SPUM_Prefabs>();

        // Récupération de l'Animator qui utilise SpumController
        if (spum != null)
        {
            animator = spum._anim;
        }
    }

    void Update()
    {
        // Récupération des entrées utilisateur
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        // Vérification du déplacement
        if (animator != null)
        {
            bool isMoving = dir.sqrMagnitude > 0;
            animator.SetBool("1_Move", isMoving);

            // Si le joueur se déplace à gauche, on force la direction de l'animation
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
        // Déplacement du joueur en utilisant MovePosition dans FixedUpdate pour la physique
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}
