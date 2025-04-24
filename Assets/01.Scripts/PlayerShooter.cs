using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
    public Transform gunPivot;
    public Transform leftHandmount;
    public Transform rightHandmount;

    private PlayerInput playerInput;
    private Animator playerAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        
    }

    private void OnEnable()
    {
        gun.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        gun.gameObject.SetActive(true);
    }
   
    // Update is called once per frame
    void Update()
    {
        if(playerInput.fire)
        {

            gun.Fire();
        }
        else if(playerInput.reload)
        {
            if(gun.Reload())
            {
                playerAnimator.SetTrigger("Reload");


            }


        }
        UpdateUI();
        
    }
 private void UpdateUI()
    {
        if(gun!=null&&UIManager.instance!=null)
        {
            //UI 매니저의 탄알 텍스트에 탄차의 탄알과 남은 전체 탄알 표시
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }


    }

    private void OnAnimatorIK(int layerIndex)
    { //총의 기준점 gunPivot을 3D모델의 오른쪽 팔꿈치 위치로 이동
        gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
        //IK를 사용하여 왼손의 위치와 회전을 총의 왼쪽 손잡이에 맞춤
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandmount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandmount.rotation);
        //IK를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춤
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandmount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandmount.rotation);


    }

}
