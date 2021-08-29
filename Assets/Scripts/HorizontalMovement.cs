using UnityEngine;

/// <summary>
/// Move a gameObject to the left at the speed <see cref="speed"/>.
/// </summary>
public class HorizontalMovement : MonoBehaviour
{
    private float speed = 1.0f;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.left, speed * Time.deltaTime);

        if(transform.position.x < GameManager.Instance.GetXLeftUpCorner() - 5.0f)
        {
            Destroy(gameObject);
        }
    }
}
