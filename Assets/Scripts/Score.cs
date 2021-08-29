[System.Serializable]

/// <summary>
/// Use to save all score on file.
/// </summary>
public class Score
{
    public int point;
    public int coin;

    public Score(int point, int coin)
    {
        this.point = point;
        this.coin = coin;
    }
}
