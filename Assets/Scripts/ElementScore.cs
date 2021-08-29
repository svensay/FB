using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Edit score on the scoreboard text.
/// </summary>
public class ElementScore : MonoBehaviour
{

    [SerializeField]
    private Text pointText = null;    
    
    [SerializeField]
    private Text coinText = null;

    void Start()
    {
        if (pointText == null)
        {
            pointText = transform.GetChild(0).GetComponent<Text>();
        }        
        
        if (coinText == null)
        {
            coinText = transform.GetChild(1).GetComponent<Text>();
        }
    }
    
    public void ChangePointText(string point)
    {
        pointText.text = point;
    }    
    
    public void ChangeCoinText(string coin)
    {
        coinText.text = coin;
    }
}
