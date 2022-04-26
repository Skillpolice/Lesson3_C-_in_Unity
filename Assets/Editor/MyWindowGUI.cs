using UnityEngine;
using UnityEditor;


public class MyWindowGUI : EditorWindow
{

    public Color myColor;         // Градиент цвета
    public MeshRenderer GO;      // Ссылка на рендер объекта


    public Material _newMat;
    private Transform _mainCam;



    [MenuItem("Инструменты / GUI Window / Creator Prefab")]
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
            myColor = RGBSlider(new Rect(10, 30, 200, 20), myColor);  // Отрисовка пользовательского набора слайдеров для получения градиента цвета
            GO.sharedMaterial.color = myColor; // Покраска объекта
        }
        else
        {
            if (GUI.Button(new Rect(10, 60, 100, 30), "Creat"))
            {
                _mainCam = Camera.main.transform;
                GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube); //создаем примитивный объект
                MeshRenderer GOrenderer = temp.GetComponent<MeshRenderer>(); //получаем компонент
                GOrenderer.sharedMaterial = _newMat; //назначаем материал
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

    // Отрисовка пользовательского слайдера
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMaxValue, string labelText) // ДЗ добавить MinValue
    {
        // создаём прямоугольник с координатами в пространстве и заданой шириной и высотой 
        Rect labelRect = new Rect(screenRect.x, screenRect.y, screenRect.width / 2, screenRect.height);

        GUI.Label(labelRect, labelText);   // создаём Label на экране

        Rect sliderRect = new Rect(screenRect.x + screenRect.width / 2, screenRect.y, screenRect.width / 2, screenRect.height); // Задаём размеры слайдера
        sliderValue = GUI.HorizontalSlider(sliderRect, sliderValue, 0.0f, sliderMaxValue); // Вырисовываем слайдер и считываем его параметр
        return sliderValue; // Возвращаем значение слайдера
    }

    // Отрисовка тройной слайдер группы, каждый слайдер отвечает за свой цвет
    Color RGBSlider(Rect screenRect, Color rgb)
    {
        // Используя пользовательский слайдер, создаём его
        rgb.r = LabelSlider(screenRect, rgb.r, 1.0f, "Red");

        // делаем промежуток
        screenRect.y += 20;
        rgb.g = LabelSlider(screenRect, rgb.g, 1.0f, "Green");

        screenRect.y += 20;
        rgb.b = LabelSlider(screenRect, rgb.b, 1.0f, "Blue");

        screenRect.y += 20;
        rgb.a = LabelSlider(screenRect, rgb.a, 1.0f, "alpha");

        return rgb; // возвращаем цвет
    }
}



