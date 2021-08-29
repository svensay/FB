using System.Collections;
using UnityEngine;

public class CoinCount : MonoBehaviour
{
    private AudioSource audioSource = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(TriggerCoinCoroutine());
    }

    private IEnumerator TriggerCoinCoroutine()
    {
        GameManager.Instance.AddOneCoin();
        GameManager.Instance.UpdateCoinText();
        audioSource.Play();
        while(audioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
    }
}
