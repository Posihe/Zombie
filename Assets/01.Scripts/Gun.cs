using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{//���� ���¸� ǥ���ϴ� �� ����� Ÿ���� ����
    public enum State {
        Ready,
        Empty,
        Reloading
    }

    public State state { get; private set; }//���� ���� ����
   
    public Transform fireTransform;//ź���� �߻�� ��ġ

    public ParticleSystem muzzleFlashEffect;//�ѱ�ȭ��ȿ��
    public ParticleSystem shellEjectEffect;//ź�ǹ���ȿ��

    private LineRenderer bulletLineRenderer;//ź���� ������ �׸��� ���� ������

    private AudioSource gunAudioPlayer;//�� �Ҹ� �����

    public GunData gunData;//���� ���� ������

    private float fireDistance = 50f;//�����Ÿ�

    public int ammoRemain = 100;//���� ��ü ź��
    public int magAmmo;//���� źâ�� ���� ź��

    private float lastFireTime;//���� ���������� �߻��� ����

    private void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;//����� ���� �� ���� ����
        bulletLineRenderer.enabled = false;
    }
    private void OnEnable()
    {
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity;

        state = State.Ready;
        lastFireTime = 0;
    }
    //�߻� �õ�
    public void Fire()
    {
        if(state==State.Ready&&Time.time>=lastFireTime+gunData.timeBetFire)
        {
            //������ �� �߻� ���� ����
            lastFireTime = Time.time;
            //���� �߻� ó�� ����
            Shot();

        }
        

    }
    //���� �߻� ó��
    private void Shot()
    {
        Ray ray=new Ray(fireTransform.position, fireTransform.forward);
        RaycastHit hit;//�浹 ���� ���� �����̳�
        Vector3 hitposition = Vector3.zero;//ź���� ���� ���� ������ ����

        if(Physics.Raycast(ray,out hit,fireDistance))
        {

            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if(target!=null)
            {

                target.OnDamage(gunData.damage, hit.point, hit.normal);
                
            }
            hitposition = hit.point;
        }
        else 
        {
            hitposition = fireTransform.position + fireTransform.forward * fireDistance;
        }
        StartCoroutine(ShotEffect(hitposition));
        magAmmo--;
        if(magAmmo<=0)
        {

            state = State.Empty;
        }
        

    }
    //�߻� ����Ʈ�� �Ҹ��� ����ϰ� ź�� ������ �׸�
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        

        //�ѱ� ȭ�� ȿ��
        muzzleFlashEffect.Play();
        //ź�� ���� ȿ��
        shellEjectEffect.Play();
        gunAudioPlayer.PlayOneShot(gunData.shotClip);
        //���� �������� �ѱ��� ��ġ
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        //���� ������ �Է����� ���� �浹 ��ġ
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);
        bulletLineRenderer.enabled = false;
    }
    //������ �õ�
    public bool Reload()
    {if (state == State.Reloading || ammoRemain <= 0 || magAmmo >= gunData.magCapacity)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        return true;

    }
    //���� ������ ġ���� ����
    private IEnumerator ReloadRoutine()
    {//���� ���¸� ������ �� ���·� ��ȯ
        state = State.Reloading;
        gunAudioPlayer.PlayOneShot(gunData.reloadClip);
        yield return new WaitForSeconds(gunData.reloadTime);
        int ammoToFill = gunData.magCapacity - magAmmo;//źâ�� ä�� ź�� ���
        if(ammoRemain<ammoToFill)
        {
            ammoToFill = ammoRemain;
        }
        magAmmo += ammoToFill;
        ammoRemain -= ammoToFill;
        //���� ���� ���¸� �߻� �غ� ���·� ����
        state = State.Ready;
    }


   
}
