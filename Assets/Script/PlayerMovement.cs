using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4.5f;           // Vitesse de déplacement
    private Rigidbody2D rb;              // Composant Rigidbody2D du personnage
    private Vector2 dir;                 // Stocke les valeurs de déplacement
    private Animator animator;           // Composant Animator du personnage

    void Start()
    {
        // Récupération du composant Rigidbody2D attaché à l'objet
        rb = GetComponent<Rigidbody2D>();

        // Récupération du composant Animator attaché à l'objet
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Récupération des entrées de l'utilisateur pour les axes Horizontal et Vertical
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        // Mise à jour des paramètres de l'Animator pour le Blend Tree
        animator.SetFloat("Horizontal", dir.x);
        animator.SetFloat("Vertical", dir.y);

        // Calcul de la vitesse pour activer/désactiver l'animation de marche
        animator.SetFloat("Speed", dir.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Déplacement du joueur en utilisant MovePosition dans FixedUpdate pour la physique
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }
}