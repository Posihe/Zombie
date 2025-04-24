using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Zombie : LivingEntity
{
    public LayerMask whatIstarget;//���� ��� ���̾�
    private LivingEntity targetEntity;//�������
    private NavMeshAgent navMeshAgent;//��ΰ�� Ai

    public ParticleSystem hitEffect;
    public AudioClip deathSound;
    public AudioClip hitSound;

    private Animator zombieAnimator;
    private AudioSource zombieAudioPlayer;
    private Renderer zombieRenderer;//������ ������Ʈ

    public float damage = 20f;
    public float timeBetAttack = 0.5f;//���ݰ���
    private float lastAttackTime;//������ ���� ����

    private bool hasTarget//������ ����� �����ϴ��� �˷��ִ� ������Ƽ
    {
      get
        {
            if(targetEntity!=null&&!targetEntity.dead)
            {
                return true;

            }
            return false;

        }
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        zombieAudioPlayer = GetComponent<AudioSource>();

        zombieRenderer = GetComponentInChildren<Renderer>();
        
        
    }

  public void Setup(ZombieData zombieData)//����AI�� �ʱ� ������ �����ϴ� �¾� �޼���
    {

        startingHealth = zombieData.health;
        health = zombieData.health;

        damage = zombieData.damage;

        navMeshAgent.speed = zombieData.speed;

        zombieRenderer.material.color = zombieData.skinColor;

    }
    private void Start()
    {
        StartCoroutine(UpdatePath());
    }
    
    // Update is called once per frame
    void Update()
    {
        zombieAnimator.SetBool("HasTarget", hasTarget);//���� ��� ���� �ִϸ��̼� ���
    }

    private IEnumerator UpdatePath()
    {


        while(!dead)
        {
            if(hasTarget)
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetEntity.transform.position);

            }
            else
            {
                navMeshAgent.isStopped = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, whatIstarget);

                for(int i=0;i<colliders.Length;i++)
                {

                    LivingEntity livingEntity = colliders[i].GetComponent<LivingEntity>();
                    if(livingEntity!=null&&!livingEntity.dead)
                    {


                        targetEntity = livingEntity;

                        break;//for�� ��� ����
                    }
                    
                }
            }

                yield return new WaitForSeconds(0.25f);
        }
    }

    public override void OnDamage(float damage, Vector3 hitpoint, Vector3 hitNormal)
    {if (!dead)
        {
            hitEffect.transform.position = hitpoint;
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            hitEffect.Play();

            zombieAudioPlayer.PlayOneShot(hitSound);
        }
            base.OnDamage(damage, hitpoint, hitNormal);
        
    }

    public override void Die()
    {
        base.Die();

        Collider[] zombieColliders = GetComponents<Collider>();
        for(int i=0;i<zombieColliders.Length;i++)
        {
            zombieColliders[i].enabled = false;
        }

        navMeshAgent.isStopped = true;
        navMeshAgent.enabled = false;

        zombieAnimator.SetTrigger("Die");
        zombieAudioPlayer.PlayOneShot(deathSound);

    }

    private void OnTriggerStay(Collider other)
    {
        if(!dead&&Time.time>=lastAttackTime+timeBetAttack)
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();

            if(attackTarget!=null&&attackTarget==targetEntity)
            {

                lastAttackTime = Time.time;

                Vector3 hitpoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;

                attackTarget.OnDamage(damage, hitpoint, hitNormal);
            }

        }
       

        
    }
}
