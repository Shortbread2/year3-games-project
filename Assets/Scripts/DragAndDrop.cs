using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 originalPosition; // Added to store the original position

    public GameObject RowColCollider;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;

            // Reset to the original position if not colliding
            if (!IsColliding())
            {
                transform.position = originalPosition;
            }
        }

        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }
    }

    private bool IsColliding()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f);

        foreach (Collider2D collider in colliders)
        {
            // Check if the collider is a PolygonCollider2D, not the collider of the current GameObject,
            // and has the name "RowColCollider"
            if (collider.GetType() == typeof(PolygonCollider2D) && collider.gameObject != gameObject &&
                collider.gameObject == RowColCollider)
            {
                // Destroy the GameObject
                Destroy(gameObject);
                SpriteRenderer renderer = RowColCollider.gameObject.GetComponent<SpriteRenderer>();
                Color color = renderer.color;
                color.a = 255f;
                renderer.color = color;
                return true; // Return true to indicate a collision
            }
        }

        return false; // No collision with a Polygon Collider named "RowColCollider"
    }
}


// if (collision.gameObject == RowColCollider)
//{
// Destroy the current GameObject
//  Destroy(gameObject);
//Debug.Log("wassa");

// Access the SpriteRenderer component of the collision object
//SpriteRenderer renderer = collision.gameObject.GetComponent<SpriteRenderer>();

//if (renderer != null)
// {
// Modify the alpha value
//   Color color = renderer.color;
// color.a = 255f; // Change this value to the desired alpha
//renderer.color = color;
// }
//}

