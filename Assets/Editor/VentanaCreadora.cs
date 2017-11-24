using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VentanaCreadora : EditorWindow
{
    public GameObject Camara;
    public GameObject CurrentFloor;
    public static GameObject _ParedDerecha, _ParedIzquierda, _ParedArriba, _ParedAbajo;
    public static GameObject _VParedDerecha, _VParedIzquierda, _VParedArriba, _VParedAbajo;

    [ExecuteInEditMode]
    [MenuItem("Creacion De Terreno/Ventana De Dungeon")]

    static void SetiarVentana()
    {
        ((VentanaCreadora)GetWindow(typeof(VentanaCreadora))).Show();
    }

    private void OnGUI()
    {
        CurrentFloor = Selection.activeGameObject;

        if (CurrentFloor)
        {
            EditorGUILayout.LabelField("El nombre del piso actual es :" + CurrentFloor.name);
        }
        #region Modo Manual       
                 
        Repaint();

        EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        GUILayout.Label("", GUILayout.Width(140));

        if (_ParedArriba)
        {
            GUI.color = Color.green;
        }
        else GUI.color = Color.red;

        if (GUILayout.Button("↑", GUILayout.Width(100)))
        {
            DestroyImmediate(_ParedArriba);
            DestroyImmediate(_VParedAbajo);
        }
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();

        if (_ParedDerecha)
        {
            GUI.color = Color.green;
        }
        else GUI.color = Color.red;

        GUILayout.Label("", GUILayout.Width(43));
        if (GUILayout.Button("←", GUILayout.Width(100)))
        {
            DestroyImmediate(_ParedDerecha);
            DestroyImmediate(_VParedIzquierda);
        }
        if (_ParedIzquierda)
        {
            GUI.color = Color.green;
        }
        else GUI.color = Color.red;

        GUILayout.Label("", GUILayout.Width(87));
        if (GUILayout.Button("→", GUILayout.Width(100)))
        {
            DestroyImmediate(_ParedIzquierda);
            DestroyImmediate(_VParedDerecha);
        }
        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

        GUILayout.BeginHorizontal();

        if (_ParedAbajo)
        {
            GUI.color = Color.green;
        }
        else GUI.color = Color.red;

        GUILayout.Label("", GUILayout.Width(140));
        if (GUILayout.Button("↓", GUILayout.Width(100)))
        {
            DestroyImmediate(_ParedAbajo);
            DestroyImmediate(_VParedArriba);
        }
        GUILayout.EndHorizontal();

        #endregion
        Repaint();
    }
}
