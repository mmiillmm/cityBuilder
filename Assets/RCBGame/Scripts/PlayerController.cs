using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // A játékos sebessége
    public float distToGround; // Távolság a talajig
    public float runSpeed;
    public LayerMask terrainLayer; // A réteg, amelyet a raycast ellenőriz
    public Rigidbody rb; // A játékos Rigidbody komponense
    private Animator _animator; // A játékos Animator komponense

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); // Rigidbody komponens lekérése
        _animator = GetComponent<Animator>(); // Animator komponens lekérése
    }

    void Update()
    {
        RaycastHit rayHit; // Raycast találat
        Vector3 castPosition = transform.position; // Az aktuális pozíció
        castPosition.y += 1; // Az Y pozíció növelése, hogy a raycast kicsit magasabbra induljon

        if (Physics.Raycast(castPosition, -transform.up, out rayHit, Mathf.Infinity, terrainLayer))
        {
            if (rayHit.collider != null)
            {
                Vector3 movePosition = transform.position; // Az aktuális pozíció
                movePosition.y = rayHit.point.y + distToGround; // A Y pozíció beállítása a talaj szintje alapján
                transform.position = movePosition; // Az új pozíció beállítása
            }
        }

        float x = Input.GetAxis("Horizontal"); // A vízszintes bemenet lekérése
        float y = Input.GetAxis("Vertical"); // A függőleges bemenet lekérése
        Vector3 moveDirection = new Vector3(x, 0, y); // A mozgás iránya
        rb.velocity = moveDirection * speed; // A sebesség beállítása

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : speed;
        rb.velocity = moveDirection * currentSpeed;

        if (x != 0 || y != 0)
        {
            _animator.SetBool("isWalking", !isRunning); // Sétálási animáció, ha nem fut
            _animator.SetBool("isRunning", isRunning); // Futási animáció, ha a shift lenyomva van
        }
        else
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isRunning", false); // Nincs mozgási animáció
        }


        if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // A játékos tükrözése balra
        }
        else if (x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // A játékos normál irányba állítása
        }
    }
}