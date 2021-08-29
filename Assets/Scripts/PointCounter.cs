using UnityEngine;

public class PointCounter : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.Instance.AddOnePoint();
        GameManager.Instance.UpdateScoreText();
    }
}
