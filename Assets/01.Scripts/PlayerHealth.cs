using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth :LivingEntity
{
    public Slider healthSlider;

    public AudioClip hitClip;
    public AudioClip deathClip;
    public AudioClip itemPickupClip;

    private AudioSource playerAudioPlayer;
    private Animator playerAnimator;

    private PlayerMovement playerMovement;
    private PlayerShooter playerShooter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudioPlayer = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooter = GetComponent<PlayerShooter>();
    }

    protected override void OnEnable()
    {
        //LivingEntity의 OnEnable()실행(상태초기화)
        base.OnEnable();
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = health;

        playerMovement.enabled = true;
        playerShooter.enabled = true;

      
        
        

    }
    

    public override void RestoreHealth(float newHealth)//체력회복
    {
        base.RestoreHealth(newHealth);
        healthSlider.value = health;
    }

    public override void OnDamage(float damage, Vector3 hitpoint, Vector3 hitNormal)//데미지 처리
    {
        if(!dead)
        {
            playerAudioPlayer.PlayOneShot(hitClip);

        }
        base.OnDamage(damage, hitpoint, hitNormal);

        healthSlider.value = health;
        
    }

    public override void Die()//사망적용
    {
        base.Die();

        healthSlider.gameObject.SetActive(false);

        playerAudioPlayer.PlayOneShot(deathClip);
        playerAnimator.SetTrigger("Die");

        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
    private void OnTriggerEnter(Collider other)//아이템과 충돌시 해당 아이템을 사용하는 처리
    {
        if(!dead)
        {
            IItem item = other.GetComponent<IItem>();
            if(item!=null)
            {
                item.Use(gameObject);
                playerAudioPlayer.PlayOneShot(itemPickupClip);


            }
        }
        
    }
}
