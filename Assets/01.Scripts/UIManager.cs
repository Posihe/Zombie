using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get 
        {
           if(m_instance==null)
            {
                m_instance = FindAnyObjectByType<UIManager>();

            }
            return m_instance;
        }



    }

    private static UIManager m_instance;//싱글턴에 할당될 변수

    public Text ammoText;//탄알 표시용
    public Text scoreText;//점수 표시용
    public Text waveText;
    public GameObject gameoverUI;

    public void updateAmmoText(int magAmmo,int remainAmmo)
    {
        ammoText.text = magAmmo + "/" + remainAmmo;

    }
    public void updateScoreText(int newScore)
    {
        scoreText.text = "Score:" + newScore;


    }

    public void UpdateWaveText(int waves,int count)
    {


        waveText.text = "Wave :" + waves + "\nEnemy Left :" + count;
    }
   public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);

    }
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void UpdateAmmoText(int a ,int b)
    {

      
    }
}
