using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VentanaCreadora : EditorWindow
{
    private bool ModoManual;
    public GameObject CurrentFloor;
    public GameObject _Piso;
    public GameObject _Pared;
    public GameObject _Puerta;

    private int _CantidadDePuertas = 1;    

    [ExecuteInEditMode]
    [MenuItem("Creacion De Terreno/Ventana De Dungeon")]

    static void SetiarVentana()
    {
        ((VentanaCreadora)GetWindow(typeof(VentanaCreadora))).Show();
    }

    private void OnGUI()
    {
        CurrentFloor = Selection.activeGameObject;

        maxSize = new Vector2(300,300);
        minSize = new Vector2(400, 600);

        if (CurrentFloor)
        {
        EditorGUILayout.LabelField("El nombre del piso actual es :" + CurrentFloor.name);
        }

        #region Object Field
        if (!_Piso)
        {
        EditorGUILayout.HelpBox("Seleccione un píso", MessageType.Info);
        }
        _Piso = (GameObject)EditorGUILayout.ObjectField("Objeto: ", _Piso, typeof(GameObject), true);
        EditorGUILayout.Space();

        if (!_Pared)
        {
        EditorGUILayout.HelpBox("Seleccione una puerta", MessageType.Info);
        }
        _Pared = (GameObject)EditorGUILayout.ObjectField("Puerta: ", _Pared, typeof(GameObject), true);
        EditorGUILayout.Space();

        if (!_Puerta)
        {
        EditorGUILayout.HelpBox("Seleccione una pared", MessageType.Info);
        }
        _Puerta = (GameObject)EditorGUILayout.ObjectField("Pared: ", _Puerta, typeof(GameObject), true);
        EditorGUILayout.Space();
        #endregion

        #region Variables
        EditorGUILayout.BeginHorizontal();
        
        GUILayout.Label("La cantidad de puertas a crear es : ",GUILayout.Width(210));
        GUILayout.TextField("" + _CantidadDePuertas,GUILayout.Width(20));
        if (GUILayout.Button("<"))
        {
            _CantidadDePuertas--;
        }

        if (GUILayout.Button(">"))
        {
            _CantidadDePuertas++;
        }

        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Reset"))
        {
            _CantidadDePuertas = 0;
        }

        #endregion



        #region Modo Manual
        GUILayout.BeginHorizontal();
        ModoManual = GUILayout.Toggle(ModoManual, "▼",GUILayout.Width(10));
        EditorGUILayout.LabelField("Editor Manual :",EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        if (ModoManual)
        {
        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        GUILayout.Label("", GUILayout.Width(140));
            if (GUILayout.Button("↑", GUILayout.Width(100)))
            {
                GameObject PisoCreado = Instantiate(CurrentFloor);
                PisoCreado.transform.position = CurrentFloor.transform.position + new Vector3(CurrentFloor.transform.position.x + 10, CurrentFloor.transform.position.y, CurrentFloor.transform.position.z);
                Selection.activeGameObject = PisoCreado;
                CurrentFloor = PisoCreado;
            }
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.Label("", GUILayout.Width(43));
            if (GUILayout.Button("←", GUILayout.Width(100)))
            {
                GameObject PisoCreado = Instantiate(CurrentFloor);
                PisoCreado.transform.position = CurrentFloor.transform.position + new Vector3(CurrentFloor.transform.position.x, CurrentFloor.transform.position.y, CurrentFloor.transform.position.z-10);
                Selection.activeGameObject = PisoCreado;
                CurrentFloor = PisoCreado;
            }
            
        GUILayout.Label("", GUILayout.Width(87));
            if (GUILayout.Button("→", GUILayout.Width(100)))
            {
                GameObject PisoCreado = Instantiate(CurrentFloor);
                PisoCreado.transform.position = CurrentFloor.transform.position + new Vector3(CurrentFloor.transform.position.x, CurrentFloor.transform.position.y, CurrentFloor.transform.position.z+10);
                Selection.activeGameObject = PisoCreado;
                CurrentFloor = PisoCreado;
            }            
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();
        GUILayout.Label("", GUILayout.Width(140));
            if (GUILayout.Button("↓", GUILayout.Width(100)))
            {
                GameObject PisoCreado = Instantiate(CurrentFloor);
                PisoCreado.transform.position = CurrentFloor.transform.position + new Vector3(CurrentFloor.transform.position.x-10, CurrentFloor.transform.position.y, CurrentFloor.transform.position.z);
                Selection.activeGameObject = PisoCreado;
                CurrentFloor = PisoCreado;
            }            
        GUILayout.EndHorizontal();
        }


        #endregion

        fixedValues();

        Repaint();
    }

    void fixedValues()
    {
        _CantidadDePuertas = Mathf.Max(_CantidadDePuertas, 1);
        _CantidadDePuertas = Mathf.Min(_CantidadDePuertas, 4);
    }
}
