using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{//총의 상태를 표현하는 데 사용할 타입을 선언
    public enum State {
        Ready,
        Empty,
        Reloading
    }

    public State state { get; private set; }//현재 총의 상태
   
    public Transform fireTransform;//탄알이 발사될 위치

    public ParticleSystem muzzleFlashEffect;//총구화염효과
    public ParticleSystem shellEjectEffect;//탄피배출효과

    private LineRenderer bulletLineRenderer;//탄알의 궤적을 그리기 위한 렌더러

    private AudioSource gunAudioPlayer;//총 소리 재생기

    public GunData gunData;//총의 현재 데이터

    private float fireDistance = 50f;//사정거리

    public int ammoRemain = 100;//남은 전체 탄알
    public int magAmmo;//현재 탄창에 남은 탄알

    private float lastFireTime;//총을 마지막으로 발사한 시점

    private void Awake()
    {
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;//사용할 점을 두 개로 변경
        bulletLineRenderer.enabled = false;
    }
    private void OnEnable()
    {
        ammoRemain = gunData.startAmmoRemain;
        magAmmo = gunData.magCapacity;

        state = State.Ready;
        lastFireTime = 0;
    }
    //발사 시도
    public void Fire()
    {
        if(state==State.Ready&&Time.time>=lastFireTime+gunData.timeBetFire)
        {
            //마지막 총 발사 시점 갱신
            lastFireTime = Time.time;
            //실제 발사 처리 실행
            Shot();

        }
        

    }
    //실제 발사 처리
    private void Shot()
    {
        Ray ray=new Ray(fireTransform.position, fireTransform.forward);
        RaycastHit hit;//충돌 정보 저장 컨테이너
        Vector3 hitposition = Vector3.zero;//탄알이 맞은 곳을 저장할 변수

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
    //발사 이펙트와 소리를 재생하고 탄알 궤적을 그림
    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        

        //총구 화염 효과
        muzzleFlashEffect.Play();
        //탄피 배출 효과
        shellEjectEffect.Play();
        gunAudioPlayer.PlayOneShot(gunData.shotClip);
        //선의 시작점은 총구의 위치
        bulletLineRenderer.SetPosition(0, fireTransform.position);
        //선의 끝점은 입력으로 들어온 충돌 위치
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);
        bulletLineRenderer.enabled = false;
    }
    //재장전 시도
    public bool Reload()
    {if (state == State.Reloading || ammoRemain <= 0 || magAmmo >= gunData.magCapacity)
        {
            return false;
        }
        StartCoroutine(ReloadRoutine());
        return true;

    }
    //실제 재장전 치리를 진행
    private IEnumerator ReloadRoutine()
    {//현재 상태를 재장전 중 상태로 전환
        state = State.Reloading;
        gunAudioPlayer.PlayOneShot(gunData.reloadClip);
        yield return new WaitForSeconds(gunData.reloadTime);
        int ammoToFill = gunData.magCapacity - magAmmo;//탄창에 채울 탄알 계산
        if(ammoRemain<ammoToFill)
        {
            ammoToFill = ammoRemain;
        }
        magAmmo += ammoToFill;
        ammoRemain -= ammoToFill;
        //총의 현재 상태를 발사 준비 상태로 변경
        state = State.Ready;
    }


   
}
