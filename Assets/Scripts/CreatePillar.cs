using UnityEngine;

public class CreatePillar : MonoBehaviour
{
    [SerializeField]
    private GameObject[] pillarPrefab;       
    
    [SerializeField]
    private GameObject[] coinPrefab;    

    private float countTime = 0.0f;

    private float stepTime = 5.0f;

    private int nextIndex = -1;
    private int index = -1;
    private bool coinCreated = false;

    private void Start()
    {
        nextIndex = Random.Range(0, coinPrefab.Length);
    }

    /// <summary>
    /// Create random pillars each <see cref="stepTime"/>.
    /// </summary>
    private void Update()
    {
        // If two same type of pillars are created, we place a piece between them.
        if (index == nextIndex && index != 2 && countTime > stepTime/2 && !coinCreated)
        {
            Instantiate(coinPrefab[index], transform);
            coinCreated = true;
        }

        if(countTime > stepTime)
        {
            index = nextIndex;
            Instantiate(pillarPrefab[index], transform);
            countTime = 0.0f;
            coinCreated = false;
            nextIndex = Random.Range(0, coinPrefab.Length);
        }

        countTime += Time.deltaTime;
    }
}
