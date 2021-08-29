using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField]
    private GameObject deadEye = null;    
    
    [SerializeField]
    private AudioClip gameOverSound = null;

    private Fly flyScript = null;
    private Rigidbody2D rb2 = null;
    private AudioSource audioSource = null;

    private void Start()
    {
        if(deadEye == null)
        {
            deadEye = transform.GetChild(0).gameObject;
        }

        flyScript = GetComponent<Fly>();
        rb2 = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        deadEye.SetActive(true);
        flyScript.enabled = false;
        rb2.isKinematic = true;
        rb2.simulated = false;
        audioSource.PlayOneShot(gameOverSound);
        GameManager.Instance.GameOver();
    }
}
