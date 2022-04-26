using UnityEngine;
using UnityEditor;


public class MyWindowGUI : EditorWindow
{

    public Color myColor;         // �������� �����
    public MeshRenderer GO;      // ������ �� ������ �������


    public Material _newMat;
    private Transform _mainCam;



    [MenuItem("����������� / GUI Window / Creator Prefab")]
    public static void ShowMyWindow()
    {
        GetWindow(typeof(MyWindowGUI), false, "Creator Pefab");
    }



    void OnGUI()
    {
        GO = EditorGUILayout.ObjectField("MeshOBJ", GO, typeof(MeshRenderer), true) as MeshRenderer;
        _newMat = EditorGUILayout.ObjectField("MaterialOBJ", _newMat, typeof(Material), true) as Material;

        if (GO != null)
        {
            myColor = RGBSlider(new Rect(10, 30, 200, 20), myColor);  // ��������� ����������������� ������ ��������� ��� ��������� ��������� �����
            GO.sharedMaterial.color = myColor; // �������� �������
        }
        else
        {
            if (GUI.Button(new Rect(10, 60, 100, 30), "Creat"))
            {
                _mainCam = Camera.main.transform;
                GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube); //������� ����������� ������
                MeshRenderer GOrenderer = temp.GetComponent<MeshRenderer>(); //�������� ���������
                GOrenderer.sharedMaterial = _newMat; //��������� ��������
                temp.transform.position = new Vector3(_mainCam.position.x, _mainCam.position.y, _mainCam.position.z + 10);
                GO = GOrenderer;
            }

        }

        if (GUI.Button(new Rect(10, 160, 100, 30), "Delete"))
        {
            DestroyImmediate(GO.gameObject);
            GO = null;
        }



    }

    // ��������� ����������������� ��������
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMaxValue, string labelText) // �� �������� MinValue
    {
        // ������ ������������� � ������������ � ������������ � ������� ������� � ������� 
        Rect labelRect = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(labelRect, labelText);   // ������ Label �� ������

        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height); // ����� ������� ��������
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, 0.0f, sliderMaxValue); // ������������ ������� � ��������� ��� ��������
        return sliderValue; // ���������� �������� ��������
    }

    // ��������� ������� ������� ������, ������ ������� �������� �� ���� ����
    Color RGBSlider(Rect screenRect, Color rgb)
    {
        // ��������� ���������������� �������, ������ ���
        rgb.r = LabelSlider(screenRect, rgb.r, 1.0f, "Red");

        // ������ ����������
        screenRect.y += 20;
        rgb.g = LabelSlider(screenRect, rgb.g, 1.0f, "Green");

        screenRect.y += 20;
        rgb.b = LabelSlider(screenRect, rgb.b, 1.0f, "Blue");

        screenRect.y += 20;
        rgb.a = LabelSlider(screenRect, rgb.a, 1.0f, "alpha");

        return rgb; // ���������� ����
    }
}



