using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.5f;           // Vitesse de d�placement
    private Rigidbody2D rb;              // Composant Rigidbody2D du personnage
    private Vector2 dir;                 // Stocke les valeurs de d�placement
    private Animator animator;           // Composant Animator du personnage

    void Start()
    {
        // R�cup�ration du composant Rigidbody2D attach� � l'objet
        rb = GetComponent<Rigidbody2D>();

        // R�cup�ration du composant Animator attach� � l'objet
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // R�cup�ration des entr�es de l'utilisateur pour les axes Horizontal et Vertical
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        // Mise � jour des param�tres de l'Animator pour le Blend Tree
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);

        // Calcul de la vitesse pour activer/d�sactiver l'animation de marche
        animator.SetFloat("Speed", dir.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // D�placement du joueur en utilisant MovePosition dans FixedUpdate pour la physique
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}