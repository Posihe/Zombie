using UnityEngine;

public class Sword : MonoBehaviour
{
    private PlayerInput playerInput;
    public Transform swordPivot;
    public Transform leftHandmount;
    public Transform rightHandmount;
    private Animator playerAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    { 
        if(playerInput.fire)
        {
            playerAnimator.SetTrigger("Attack");

        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        swordPivot.position = playerAnimator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // 왼손 IK 설정
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandmount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandmount.rotation);

        // 오른손 IK 설정 (수정된 부분)
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);
        playerAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandmount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandmount.rotation);
    }

}
