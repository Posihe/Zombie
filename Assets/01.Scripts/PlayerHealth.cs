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
        //LivingEntity�� OnEnable()����(�����ʱ�ȭ)
        base.OnEnable();
        healthSlider.gameObject.SetActive(true);
        healthSlider.maxValue = startingHealth;
        healthSlider.value = health;

        playerMovement.enabled = true;
        playerShooter.enabled = true;

      
        
        

    }
    

    public override void RestoreHealth(float newHealth)//ü��ȸ��
    {
        base.RestoreHealth(newHealth);
        healthSlider.value = health;
    }

    public override void OnDamage(float damage, Vector3 hitpoint, Vector3 hitNormal)//������ ó��
    {
        if(!dead)
        {
            playerAudioPlayer.PlayOneShot(hitClip);

        }
        base.OnDamage(damage, hitpoint, hitNormal);

        healthSlider.value = health;
        
    }

    public override void Die()//�������
    {
        base.Die();

        healthSlider.gameObject.SetActive(false);

        playerAudioPlayer.PlayOneShot(deathClip);
        playerAnimator.SetTrigger("Die");

        playerMovement.enabled = false;
        playerShooter.enabled = false;
    }
    private void OnTriggerEnter(Collider other)//�����۰� �浹�� �ش� �������� ����ϴ� ó��
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
