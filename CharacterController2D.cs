using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CharacterController2D : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    [SerializeField, Tooltip("Acceleration while in the air.")]
    float airAcceleration = 30;

    [SerializeField, Tooltip("Deceleration applied when character is grounded and not attempting to move.")]
    float groundDeceleration = 70;

    [SerializeField, Tooltip("Max height the character will jump regardless of gravity")]
    float jumpHeight = 4;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    private bool grounded;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        if (grounded)
        {
            velocity.y = 0;

            // Hier hab ich nichts geändert Paul, nur die "grounded" Bedingung hinzugefügt
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }
        velocity.y += Physics2D.gravity.y * Time.deltaTime;
        float moveInput = Input.GetAxisRaw("Horizontal");
        float acceleration = grounded ? walkAcceleration : airAcceleration;
        float deceleration = grounded ? groundDeceleration : 0;
        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        }
        transform.Translate(velocity * Time.deltaTime);
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);
        grounded = false;
        foreach (Collider2D hit in hits)
        {
            if (hit == boxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    grounded = true;
                }
            }
        }
    }
   
}


