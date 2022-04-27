using UnityEngine;

public class IKAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    [SerializeField] private Transform __handObj;
    [SerializeField] private Transform __lookObj;

    [SerializeField] private float _rightHandWeight;


    [Header("Left Foot")]
    [SerializeField] private float _leftFootWeight;
    public Transform _leftLowerLeg;
    public Transform _leftFoot;
    public LayerMask _mask;

    public Vector3 _leftFootPosition;
    public Quaternion _leftFootRotation;

    private int _leftHash;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {

        _leftHash = Animator.StringToHash("Left_foot");

        _leftLowerLeg = _animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
        _leftFoot = _animator.GetBoneTransform(HumanBodyBones.LeftFoot);
    }


    private void OnAnimatorIK(int layerIndex)
    {
        RaycastHit hit;
        _leftFootWeight = _animator.GetFloat(_leftHash);

        if (Physics.Raycast(_leftLowerLeg.position, Vector3.down, out hit, 1.5f, _mask))
        {
            _leftFootPosition = Vector3.Lerp(_leftFoot.position, hit.point + Vector3.up * 0.3f, Time.deltaTime * 10f);
            _leftFootRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightHandWeight); //обращаемся к руке (позиция - поворот)
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, _rightHandWeight);

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _leftFootWeight); //обращаемся к Ноге (позиция - поворот)
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _leftFootWeight);

        _animator.SetLookAtWeight(1); //повотор головы

        _animator.SetIKPosition(AvatarIKGoal.LeftFoot, _leftFootPosition);
        _animator.SetIKRotation(AvatarIKGoal.LeftFoot, _leftFootRotation);


        //Объект к которому тянемся и смотрим
        if (__handObj)
        {
            _animator.SetIKPosition(AvatarIKGoal.RightHand, __handObj.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, __handObj.rotation);
        }

        if (__lookObj)
        {
            _animator.SetLookAtPosition(__lookObj.position);
        }
    }










}
