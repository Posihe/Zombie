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

    private static UIManager m_instance;//�̱��Ͽ� �Ҵ�� ����

    public Text ammoText;//ź�� ǥ�ÿ�
    public Text scoreText;//���� ǥ�ÿ�
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
