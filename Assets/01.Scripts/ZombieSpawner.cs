using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab;

    public ZombieData[] zombieDatas;
    public Transform[] spawnPoints;

    private List<Zombie> zombies = new List<Zombie>();
    private int wave;
   

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance!=null&&GameManager.instance.isGameover)
        {
            return;

        }
        if(zombies.Count<=0)
        {

            SpawnWave();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        UIManager.instance.UpdateWaveText(wave, zombies.Count);

    }

    private void SpawnWave()
    {
        wave++;

        //���� ���̺�*1.5�� �ݿø��� �� ��ŭ ���� ����
        int spawnCount = Mathf.RoundToInt(wave * 1.5f);

        for(int i=0;i<spawnCount;i++)
        {
            CreatZombie();

        }

    }

    private void CreatZombie()
    {
        ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Zombie zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        zombie.Setup(zombieData);

        zombies.Add(zombie);

        //������ onDeath �̺�Ʈ�� �͸� �޼��� ���
        //����� ���� ����Ʈ���� ����
        zombie.onDeath += () => zombies.Remove(zombie);

        zombie.onDeath += () => Destroy(zombie.gameObject, 10f);

        zombie.onDeath += () => GameManager.instance.Addscore(100);



    }
}
