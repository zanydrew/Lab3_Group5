<<<<<<< HEAD
        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
=======
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// /// <summary>
// /// STARTER — A collectable orb for the "Orb Collector" lab goal.
// /// Attach to a small sphere (scale 0.6), give it a Collider with
// /// "Is Trigger" checked, and tag the object "Orb".
// ///
// /// ALREADY WORKS: click-to-recolor via OnMouseDown (GetMouseButtonDown
// /// fallback included), a gentle bob + spin every frame, and a
// /// gameObject.SetActive(false) hide on trigger entry is handled by the
// /// player (see PlayerController TODO 2).
// /// </summary>
// public class Orb : MonoBehaviour
// {
//     [Header("Click palette")]
//     [SerializeField] private Color[] clickColors = { Color.cyan, Color.magenta, Color.yellow };

//     [Header("Bob & spin")]
//     [SerializeField] private float bobHeight = 0.15f;
//     [SerializeField] private float spinSpeed = 90f;

//     private Vector3 basePosition;
//     private Vector3 baseScale;
//     private int clickIndex;
//     private float hideTime = -1f;

//     void Awake()
//     {
//         basePosition = transform.position;
//         baseScale = transform.localScale;
//     }

//     void Update()
//     {
//         // Bob up and down using Time.time (seconds since Play started).
//         float bob = Mathf.Sin(Time.time * 3f) * bobHeight;
//         transform.position = basePosition + Vector3.up * bob;
//         transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.World);
//     }

//     private void OnMouseDown()
//     {
//         // Click the orb's collider → cycle its color. Uses GetComponent<T>().
//         clickIndex = (clickIndex + 1) % clickColors.Length;
//         GetComponent<Renderer>().material.color = clickColors[clickIndex];
//         Debug.Log($"[Orb] Clicked → {clickColors[clickIndex]}");
//     }


//     // NOTE: The player script collects orbs in its OnTriggerEnter. This script
//     // only handles looking pretty, being clicked, and bobbing.
// }
 using UnityEngine;
>>>>>>> origin/mainV2

public class Orb : MonoBehaviour
{
    [Header("Click palette")]
    [SerializeField] private Color[] clickColors = { Color.cyan, Color.magenta, Color.yellow };

    [Header("Spin")]
    [SerializeField] private float spinSpeed = 90f;

<<<<<<< HEAD
    private Vector3 basePosition;
    private Vector3 baseScale;
    private int clickIndex= -1;
    private float hideTime = -1f;
=======
    [Header("Follow mouse")]
    [SerializeField] private float moveSpeed = 8f;

    private int clickIndex = -1;
    private Renderer orbRenderer;
    private Camera cam;
>>>>>>> origin/mainV2













    

    void Awake()
    {
        orbRenderer = GetComponent<Renderer>();
        cam = Camera.main;
    }

    void Update()
    {
        // Spin in place.
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime, Space.Self);
    }

    // TODO 4: click -> cycle colour.
    private void OnMouseDown()
    {
        if (clickColors.Length > 0)
        {
            clickIndex = (clickIndex + 1) % clickColors.Length;
            orbRenderer.material.color = clickColors[clickIndex];
            Debug.Log($"[Orb] Clicked -> {clickColors[clickIndex]}");
        }
    }

<<<<<<< HEAD
    // NOTE: The player script collects orbs in its OnTriggerEnter. This script
    // only handles looking pretty, being clicked, and bobbing.
}
=======
    // While the mouse button is held on the orb, move toward the mouse position.
    private void OnMouseDrag()
    {
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane floor = new Plane(Vector3.up, transform.position); // plane at the orb's height

        if (floor.Raycast(ray, out float distance))
        {
            Vector3 target = ray.GetPoint(distance);
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        }
    }
}
>>>>>>> origin/mainV2
