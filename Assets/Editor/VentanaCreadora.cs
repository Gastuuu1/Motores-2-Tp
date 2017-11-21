using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VentanaCreadora : EditorWindow
{
    private bool ModoManual = true;
    public GameObject Camara;
    public GameObject CurrentFloor;
    public GameObject _Piso;
    public GameObject _Pared;
    public GameObject _Puerta;
    public static GameObject _ParedDerecha, _ParedIzquierda, _ParedArriba, _ParedAbajo;
    public static GameObject _VParedDerecha, _VParedIzquierda, _VParedArriba, _VParedAbajo;

    public List<string> ListaDeCercanos;

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

        if (CurrentFloor)
        {
            EditorGUILayout.LabelField("El nombre del piso actual es :" + CurrentFloor.name);
        }

        #region Variables
        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("La cantidad de puertas a crear es : ", GUILayout.Width(210));
        GUILayout.TextField("" + _CantidadDePuertas, GUILayout.Width(20));
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
        ModoManual = GUILayout.Toggle(ModoManual, "▼", GUILayout.Width(10));
        EditorGUILayout.LabelField("Editor Manual :", EditorStyles.boldLabel);
        GUILayout.EndHorizontal();

        if (ModoManual)
        {
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
                /*  _ParedArriba.SetActive(false);
                  _VParedAbajo.SetActive(false);*/ //se caga todo
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
            /*    _ParedDerecha.SetActive(false);
                _VParedIzquierda.SetActive(true);*/
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
                /*   _ParedIzquierda.SetActive(false);
                   _VParedDerecha.SetActive(false);*/
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

    void BuscarCeldasVecinas()
    {
        string LugarActual = CurrentFloor.name;
        char Posx = LugarActual[LugarActual.Length];
        char Posy = LugarActual[LugarActual.Length - 1];
        Debug.Log("" + Posx + Posy);
    }


}
