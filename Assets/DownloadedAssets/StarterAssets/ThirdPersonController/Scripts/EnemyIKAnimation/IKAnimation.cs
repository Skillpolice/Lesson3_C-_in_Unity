using UnityEngine;

public class IKAnimation : MonoBehaviour
{
    private Animator _animator;

    [Header("Head Obj & Head Look")]
    [SerializeField] private Transform _lookObj; //смотрим

    [Header("Zone UI")]
    [SerializeField] private float _lookRadius = 4f;
    private float _distanceToPlayer;

    [Header("Hend's Obj & Hand's Weight ")]
    [SerializeField] private Transform _rightHandObj; //т€немс€
    [SerializeField] private Transform _leftHandObj; //т€немс€
    [SerializeField] private float _rightHandWeight;
    [SerializeField] private float _leftHandWeight;

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

        _distanceToPlayer = Vector3.Distance(transform.position, _lookObj.transform.position);

        if (_distanceToPlayer > _lookRadius)
        {
            _animator.SetLookAtWeight(0.1f); //повотор головы
        }
        else
        {
            _animator.SetLookAtWeight(1f);
        }


        //Right hand
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, _rightHandWeight); //обращаемс€ к руке (позици€ - поворот)
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, _rightHandWeight);

        //Left hand
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, _leftHandWeight); //обращаемс€ к руке (позици€ - поворот)
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, _leftHandWeight);


        //leftfoot
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, _leftFootWeight); //обращаемс€ к Ќоге (позици€ - поворот)
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, _leftFootWeight);


        _animator.SetIKPosition(AvatarIKGoal.LeftFoot, _leftFootPosition);
        _animator.SetIKRotation(AvatarIKGoal.LeftFoot, _leftFootRotation);


        //ќбъект к которому т€немс€ и смотрим
        if (_rightHandObj && _leftHandObj)
        {
            //Right
            _animator.SetIKPosition(AvatarIKGoal.RightHand, _rightHandObj.position);
            _animator.SetIKRotation(AvatarIKGoal.RightHand, _rightHandObj.rotation);

            //Left
            _animator.SetIKPosition(AvatarIKGoal.LeftHand, _leftHandObj.position);
            _animator.SetIKRotation(AvatarIKGoal.LeftHand, _leftHandObj.rotation);
        }

        if (_lookObj)
        {
            _animator.SetLookAtPosition(_lookObj.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _lookRadius);

    }








}
