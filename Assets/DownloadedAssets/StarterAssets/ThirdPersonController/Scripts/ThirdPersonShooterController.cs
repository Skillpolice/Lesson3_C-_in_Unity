using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    private ThirdPersonController _thridPersoneController;
    private StarterAssetsInputs _starterAssetsInputs;
    private StarterAssetsInputs _lightSpot;

    [SerializeField] private CinemachineVirtualCamera _aimVirtualCamera;
    [SerializeField] private LayerMask _aimColliderLayerMask = new LayerMask();
    [SerializeField] private Light _light;
    [SerializeField] private Transform _debugTransform;

    [Header("Bullet transform")]
    [SerializeField] private Transform _pfBulletProjectile;
    [SerializeField] private Transform _spawnBulletPosition;

    [Header("UI Sensitivity ThirdPerson")]
    [SerializeField] private float _normalSensitivity;
    [SerializeField] private float _aimSensitivity;


    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _lightSpot = GetComponent<StarterAssetsInputs>();
        _thridPersoneController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        Vector3 mouseWorldPos = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f); //Центрирование для мыши и геймпада
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        //Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()); // центрирование для мыши
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, _aimColliderLayerMask))
        {
            _debugTransform.position = raycastHit.point;
            mouseWorldPos = raycastHit.point;
        }


        if (_starterAssetsInputs.aim)
        {
            _aimVirtualCamera.gameObject.SetActive(true);
            _light.gameObject.SetActive(true);

            _thridPersoneController.SetSensitivityThirdPersone(_aimSensitivity);
            _thridPersoneController.SetRotationOnMove(false);

            //Игрок смотрит в направлении камеры
            Vector3 worldAimTarget = mouseWorldPos;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            _aimVirtualCamera.gameObject.SetActive(false);
            _light.gameObject.SetActive(false);

            _thridPersoneController.SetSensitivityThirdPersone(_normalSensitivity);
            _thridPersoneController.SetRotationOnMove(true);
        }

        if (_starterAssetsInputs.shoot)
        {
            Vector3 aimDir = (mouseWorldPos - _spawnBulletPosition.position).normalized;

            Instantiate(_pfBulletProjectile, _spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
            _starterAssetsInputs.shoot = false;
        }

    }







}
