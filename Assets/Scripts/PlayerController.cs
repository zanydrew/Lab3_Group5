
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Lab wiring (drag your objects here)")]
    [SerializeField] private GameObject orbPrefab;      // TODO 5: bonus orb
    [SerializeField] private Transform cameraTransform; // TODO 3: camera follow

    private Rigidbody rb;
    private Vector3 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Start()
    {
        Debug.Log("[PlayerController] Ready. Collect all orbs to win.");
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // A/D + arrows
        float v = Input.GetAxis("Vertical");   // W/S + arrows
        input = new Vector3(h, 0f, v).normalized;
    }

    // Move through physics so walls actually block the player.
    void FixedUpdate()
    {
        Vector3 vel = input * moveSpeed;
        vel.y = rb.velocity.y;   // Unity 6: use rb.linearVelocity
        rb.velocity = vel;
    }

    // TODO 1: log the name of any wall you bump (once per bump).
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("[OnCollisionEnter] Bumped into: " + collision.gameObject.name);
    }

    // TODO 2: collect orbs.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Orb"))
        {
            Debug.Log("[OnTriggerEnter] Collected orb: " + other.name);

            if (GameManager.Instance != null)
                GameManager.Instance.Collect();

            Destroy(other.gameObject);
        }
    }

    // TODO 3: camera follows the player (player position + offset).
    void LateUpdate()
    {
        if (cameraTransform == null) return;

        Vector3 offset = new Vector3(0f, 12f, -8f);
        cameraTransform.position = Vector3.Lerp(
            cameraTransform.position,
            transform.position + offset,
            5f * Time.deltaTime);
    }

    // TODO 5: when the Player is destroyed, spawn one bonus orb at its last position.
    void OnDestroy()
    {
        if (orbPrefab != null && gameObject.scene.isLoaded)
        {
            Instantiate(orbPrefab, transform.position, Quaternion.identity);
            Debug.Log("[OnDestroy] Bonus orb spawned.");
        }
    }
}
