using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static ScoreManager instance = null;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ScoreManager>();

                if (instance == null)
                {
                    GameObject container = new GameObject("scoreManager");
                    instance = container.AddComponent<ScoreManager>();
                }
            }

            return instance;
        }
    }
    #endregion

    [SerializeField]
    private GameObject elementPrefab = null;    
    
    [SerializeField]
    private Transform scoreboardContent = null;

    public List<Score> scoreData = new List<Score>();

    public void AddScore(int point, int coin)
    {
        scoreData.Add(new Score(point, coin));
    }

    /// <summary>
    /// Clear the scoreboard.
    /// </summary>
    public void ClearContent()
    {
        foreach (Transform child in scoreboardContent)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Method of the Comparison<T> to use when comparing score.
    /// Use to sort the scoreboard.
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns> 0, 1 or -1 </returns>
    private int CompareScore(Score s1, Score s2)
    {
        return s2.point.CompareTo(s1.point);
    }

    /// <summary>
    /// Add all score on the scoreboard.
    /// </summary>
    public void FillContent()
    {
        scoreData.Sort(CompareScore);

        foreach (Score sc in scoreData)
        {
            GameObject go = Instantiate(elementPrefab, scoreboardContent);
            ElementScore es = go.GetComponent<ElementScore>();
            es.ChangePointText(sc.point.ToString());
            es.ChangeCoinText(sc.coin.ToString());
        }
    }
}
