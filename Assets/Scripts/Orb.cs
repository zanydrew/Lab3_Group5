using UnityEngine;

public class Orb : MonoBehaviour
{
    [Header("Click palette")]
    [SerializeField] private Color[] clickColors = { Color.cyan, Color.magenta, Color.yellow };

    [Header("Spin")]
    [SerializeField] private float spinSpeed = 90f;

    [Header("Follow mouse")]
    [SerializeField] private float moveSpeed = 8f;

    private int clickIndex = -1;
    private Renderer orbRenderer;
    private Camera cam;

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