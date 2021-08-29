using UnityEngine;

public class Fly : MonoBehaviour
{
    private Animator animator = null;
    private Rigidbody2D rb2D = null;

    private float forceJump = 300.0f;
    private float speed = 2.0f;

    private bool startPosition => Mathf.Approximately(transform.position.x, 0.0f);

    [SerializeField]
    private GameObject tapText = null;

    private AudioSource audioSource = null;

    [SerializeField]
    private AudioClip jumpSound = null;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Places the bird out of the screen.
        transform.position = new Vector3(GameManager.Instance.GetXLeftUpCorner() - 2.5f, 0.0f, 0.0f);
        rb2D.isKinematic = true;
        animator.Play("Flying");
    }

    private void Update()
    {
        // First animation when start game.
        if (!startPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * speed);
        }
        else if (!tapText.activeSelf && rb2D.isKinematic)
        {
            tapText.SetActive(true);
        }

        // Use for debug on computer.
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (GameManager.Instance.state == GameManager.State.Play && startPosition)
        {
            audioSource.PlayOneShot(jumpSound);
            
            // At the begin, the first touch is to allow the bird to fly.
            if (rb2D.isKinematic)
            {
                rb2D.isKinematic = false;
                tapText.SetActive(false);
                animator.SetTrigger("Space");
                GameManager.Instance.StartGame();
            }
            else
            {
                animator.Play("FlyingAnimation");
                rb2D.AddForce(Vector2.up * forceJump);
            }
        }
    }
}
