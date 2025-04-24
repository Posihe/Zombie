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
            //UI �Ŵ����� ź�� �ؽ�Ʈ�� ź���� ź�˰� ���� ��ü ź�� ǥ��
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }


    }

    private void OnAnimatorIK(int layerIndex)
    { //���� ������ gunPivot�� 3D���� ������ �Ȳ�ġ ��ġ�� �̵�
        gunPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);
        //IK�� ����Ͽ� �޼��� ��ġ�� ȸ���� ���� ���� �����̿� ����
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandmount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandmount.rotation);
        //IK�� ����Ͽ� �������� ��ġ�� ȸ���� ���� ������ �����̿� ����
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandmount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandmount.rotation);


    }

}
