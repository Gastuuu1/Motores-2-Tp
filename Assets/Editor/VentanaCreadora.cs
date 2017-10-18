using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VentanaCreadora : EditorWindow
{

    public GameObject _Piso;
    public GameObject _Pared;
    public GameObject _Puerta;

    private int _CantidadDePuertas = 1;    

    [MenuItem("Creacion De Terreno/Ventana De Dungeon")]

    static void SetiarVentana()
    {
        ((VentanaCreadora)GetWindow(typeof(VentanaCreadora))).Show();
    }

    private void OnGUI()
    {
        maxSize = new Vector2(300,300);
        minSize = new Vector2(400, 600);

        #region Object Field
        EditorGUILayout.HelpBox("Seleccione un píso", MessageType.Info);
        _Piso = (GameObject)EditorGUILayout.ObjectField("Objeto: ", _Piso, typeof(GameObject), true);
        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Seleccione una loli", MessageType.Info);
        _Pared = (GameObject)EditorGUILayout.ObjectField("Loli: ", _Pared, typeof(GameObject), true);
        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Seleccione un dragon :v", MessageType.Info);
        _Puerta = (GameObject)EditorGUILayout.ObjectField("Dragon :v: ", _Puerta, typeof(GameObject), true);
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

        #region BotoneraDireccional
        GUILayout.Button("↑");
        GUILayout.BeginHorizontal();
        GUILayout.Button("←",GUILayout.Width(250));
        GUILayout.Button("→");
        GUILayout.EndHorizontal();


        #endregion
        fixedValues();
    }

    void fixedValues()
    {
        _CantidadDePuertas = Mathf.Max(_CantidadDePuertas, 0);
        _CantidadDePuertas = Mathf.Min(_CantidadDePuertas, 4);
    }
}
