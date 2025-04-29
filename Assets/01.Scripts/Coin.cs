using UnityEngine;

public class Coin : MonoBehaviour,IItem
{
    public int score = 200;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  public void Use(GameObject target)
    {
        GameManager.instance.Addscore(score);

        Destroy(gameObject );

    }
}
