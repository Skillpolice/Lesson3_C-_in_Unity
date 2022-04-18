using UnityEngine;

public class GUIHealthPlayer : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] private float _playerHealth = 100f;


    //[Header("GUIScreen")]
    //[SerializeField] private float _widthScreen;
    //[SerializeField] private float _heightScreen;

    [Header("GUI BoxSize")]
    [SerializeField] private float _boxWidthSizeHealth = 100f;
    [SerializeField] private float _boxHeightSizeHealth = 25f;

    private void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 100, 0, _boxWidthSizeHealth, _boxHeightSizeHealth), _playerHealth.ToString());
    }

}
