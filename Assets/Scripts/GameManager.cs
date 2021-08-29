using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    #region SINGLETON PATTERN
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject container = new GameObject("gameManager");
                    instance = container.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }
    #endregion

    public enum State {Menu, Play, GameOver, Pause, Score};

    public State state = State.Menu;

    #region Serialize Field
    [SerializeField]
    private Text scoreText = null;

    [SerializeField]
    private Text coinText = null;
    
    [SerializeField]
    private Animator plusOneCoinAnimator = null;

    [SerializeField]
    private ParticleSystem[] effects = null;

    [SerializeField]
    private Transform pillarFactory = null;    
    
    [SerializeField]
    private Transform playerGameObject = null;        
    
    [SerializeField]
    private GameObject birdPrefab = null;    
    
    [SerializeField]
    private GameObject gameOverPannel = null;

    [SerializeField]
    private GameObject pausePanel = null;
    #endregion

    private int point = 0;

    private int coin = 0;

    private Vector3 topLeftCornerScreenPosition = default;

    private Fly flyPlayer = null;

    #region UNITY CALLBACKS
    private void Start()
    {
        Load();
        topLeftCornerScreenPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
    }

    private void OnApplicationQuit()
    {
        Save();
    }
    #endregion

    #region BASIC FUNCTIONS
    public float GetXLeftUpCorner()
    {
        return topLeftCornerScreenPosition.x;
    }

    public void AddOnePoint()
    {
        point += 1;
    }
    
    public void AddOneCoin()
    {
        coin += 1;
        plusOneCoinAnimator.Play("PlusOneCoinState");
    }    
    
    public int GetPoint()
    {
        return point;
    }
    
    public int GetCoin()
    {
        return coin;
    }

    public void UpdateCoinText()
    {
        coinText.text = coin.ToString();
    }

    public void UpdateScoreText()
    {
        scoreText.text = point.ToString();
    }
    #endregion

    #region GAME FUNCTIONS
    public void QuitApplication()
    {
        Application.Quit();
    }

    /// <summary>
    /// State :
    /// Menu => 0
    /// Play => 1
    /// GameOver => 2
    /// Pause => 3
    /// Score => 4
    /// </summary>
    /// <param name="stId">State id</param>
    public void SelectState(int stId)
    {
        state = (State)stId;
    }
    
    public void SwitchTimeScale()
    {
        Time.timeScale = pausePanel.activeSelf ? 0.0f : 1.0f;
    }

    public void StartGame()
    {
        state = State.Play;

        pillarFactory.gameObject.SetActive(true);
        pillarFactory.GetComponent<CreatePillar>().enabled = true;
    }

    public void Restart()
    {
        state = State.Play;
        if(playerGameObject.childCount > 0)
        {
            Destroy(playerGameObject.GetChild(0).gameObject);
        }

        GameObject go = Instantiate(birdPrefab, playerGameObject);
        flyPlayer = go.GetComponent<Fly>();

        foreach (ParticleSystem particleSystem in effects)
        {
            particleSystem.Play();
        }        
        
        foreach (Transform child in pillarFactory)
        {
            Destroy(child.gameObject);
        }

        point = 0;
        coin = 0;
        UpdateScoreText();
        UpdateCoinText();
    }

    public void TapFly()
    {
        if(flyPlayer != null)
        {
            flyPlayer.Jump();
        }
    }

    public void GameOver()
    {
        state = State.GameOver;
        foreach (ParticleSystem particleSystem in effects)
        {
            particleSystem.Pause();
        }

        foreach (Transform child in pillarFactory)
        {
            child.GetComponent<HorizontalMovement>().enabled = false;
        }
        pillarFactory.GetComponent<CreatePillar>().enabled = false;
        gameOverPannel.SetActive(true);
        ScoreManager.Instance.AddScore(point, coin);
    }
    #endregion

    #region DATA FUNCTIONS

    /// <summary>
    /// Save the score data.
    /// </summary>
    public void Save()
    {
        FileStream fs;
        string filePath = Application.persistentDataPath + "save.dat";
        if (File.Exists(filePath))
        {
            fs = File.OpenWrite(filePath);
        }
        else
        {
            fs = File.Create(filePath);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            formatter.Serialize(fs, ScoreManager.Instance.scoreData);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }

    /// <summary>
    /// Load all score saved in scoreData.
    /// </summary>
    public void Load()
    {
        string filePath = Application.persistentDataPath + "save.dat";
        if (!File.Exists(filePath))
        {
            ScoreManager.Instance.scoreData = new List<Score>();
            Save();
        }

        FileStream fs = File.OpenRead(filePath);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            ScoreManager.Instance.scoreData = (List<Score>)formatter.Deserialize(fs);
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            fs.Close();
        }
    }
    #endregion
}
