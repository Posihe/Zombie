using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {

            if(m_instance==null)
            {
                m_instance = FindAnyObjectByType<GameManager>();


            }
            return m_instance;
        }

    }
    private static GameManager m_instance;
    private int score = 0;
    public bool isGameover { get; private set; }
    void Awake()
    {
        if(instance!=this)
        {

            Destroy(gameObject);
        }
        
    }
    private void Start()
    {
        FindAnyObjectByType<PlayerHealth>().onDeath += EndGame;
    }


public void Addscore(int newScore)
    {

        if(!isGameover)
        {
            score += newScore;
            UIManager.instance.updateScoreText(score);
            
        }

    }
    public void EndGame()
    {

        isGameover = true;
        UIManager.instance.SetActiveGameoverUI(true);

    }
}
