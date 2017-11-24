using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class dungeonGenerator : EditorWindow
{
    public GameObject _floor;
    public GameObject _wall;
    public GameObject _door;
    public GameObject materialprueva;

    Vector2 offset = new Vector2();
    int AltoBoton = 30;
    int Anchoboton = 40;
    Rect rekt;

    public List<GameObject> ListaDeObjetos = new List<GameObject>();

    List<Vector2> positions = new List<Vector2>();
    List<Vector2> CircleHandless = new List<Vector2>();

    public List<GameObject> FloorList = new List<GameObject>();

    int linesEvery = 40;
    int thiccLinesEvery = 5;


    //Test
    public GameObject _ParedDerecha, _ParedIzquierda, _ParedArriba, _ParedAbajo;
    public GameObject _VParedDerecha, _VParedIzquierda, _VParedArriba, _VParedAbajo;
    public GameObject _CurrentFloor;


    [ExecuteInEditMode]
    [MenuItem("Terrain/Generador de Dungeon")]

    static void CreateWindow()
    {
        ((dungeonGenerator)GetWindow(typeof(dungeonGenerator))).Show();
    }

    private void OnGUI()
    {
        #region cossas
        materialprueva = (GameObject)Resources.Load("Cube");
        _floor = (GameObject)Resources.Load("Piso");
        _wall = (GameObject)Resources.Load("Pared");
        _door = (GameObject)Resources.Load("Puerta");

        _floor = (GameObject)EditorGUILayout.ObjectField("Piso: ", _floor, typeof(GameObject), true);

        EditorGUILayout.Space();

        _wall = (GameObject)EditorGUILayout.ObjectField("Pared: ", _wall, typeof(GameObject), true);

        EditorGUILayout.Space();

        _door = (GameObject)EditorGUILayout.ObjectField("Puerta: ", _door, typeof(GameObject), true);

        EditorGUILayout.Space();

        if (GUILayout.Button("Tocar"))
        {
            ((VentanaCreadora)GetWindow(typeof(VentanaCreadora))).Show();
            GUILayout.Label("Abrir la otra ventana");
            EditorGUILayout.EndHorizontal();
        }
        #endregion 

        #region Cosa de los if
        //Recalcular segun las alertas 
        if (!_door && !_wall && !_floor)
        {
            rekt = new Rect(20, 220, 400, 500);
            maxSize = new Vector2(440, 700);
            minSize = new Vector2(440, 700);
            Repaint();
        }
        else if ((!_door && !_wall) || (!_floor && !_wall) || (!_floor && !_door))
        {
            rekt = new Rect(20, 170, 400, 500);
            maxSize = new Vector2(440, 700);
            minSize = new Vector2(440, 700);
            Repaint();

        }
        else if (!_door || !_wall || !_floor)
        {
            rekt = new Rect(20, 125, 400, 500);
            maxSize = new Vector2(440, 700);
            minSize = new Vector2(440, 700);
            Repaint();

        }
        else
        {
            rekt = new Rect(20, 200, 400, 500);
            maxSize = new Vector2(440, 700);
            minSize = new Vector2(440, 700);
            Repaint();
        }
        #endregion

        #region Botonera
        //Boton para limpiar los puntitos 
        Rect rectAdd = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(rectAdd, GUIContent.none))
        {
            if (ListaDeObjetos.Count > 1)
            {
                for (int i = 0; i <= ListaDeObjetos.Count - 1; i++)
                {
                    DestroyImmediate(ListaDeObjetos[i]);
                }
                ListaDeObjetos.Clear();
                Repaint();
            }
        }
        GUILayout.Label("Clean scene");
        EditorGUILayout.EndHorizontal();

        Rect cleanSceneRekt = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(cleanSceneRekt, GUIContent.none))
        {
            for (int i = 0; i < FloorList.Count; i++)
            {
                if (ListaDeObjetos[i] != null)
                {
                    DestroyImmediate(ListaDeObjetos[i]);
                }
            }
            CircleHandless.Clear();
        }
        GUILayout.Label("Clear window");
        EditorGUILayout.EndHorizontal();

        Rect ClearlastPref = EditorGUILayout.BeginHorizontal("Button");

        if (GUI.Button(ClearlastPref, GUIContent.none) && ListaDeObjetos.Count > 0)
        {
            DestroyImmediate(ListaDeObjetos[ListaDeObjetos.Count - 1]);
            ListaDeObjetos.RemoveAt(ListaDeObjetos.Count - 1);
        }

        GUILayout.Label("Remove last biuld");
        EditorGUILayout.EndHorizontal();
        #endregion

        GUI.BeginGroup(rekt);

        #region Creador de botones
        Handles.BeginGUI();
        for (int j = 0; j < 300; j += AltoBoton)
        {

            for (int i = 0; i < 400; i += Anchoboton)
            {
                Rect Rectangulo = new Rect(i, j, Anchoboton, AltoBoton);

                // si el boton con id i/j existe, color = verde fluor
                // else color = white

                if (GUI.Button(Rectangulo, "" + i / Anchoboton + "," + j / AltoBoton))
                {
                    GUI.color = Color.green;

                    string Senda = "" + i / Anchoboton + j / AltoBoton;
                    int NumeroSenda = System.Convert.ToInt32(Senda);

                    if (!GameObject.Find("Habitacion " + NumeroSenda))
                    {
                        Rect miniRect = new Rect(i, j, Anchoboton / 2, AltoBoton / 2);
                        if (GUI.Button(miniRect, "altok"))
                        {
                            Debug.Log("Hi");
                        }
                        materialprueva = GameObject.Find("Cube");
                        GameObject Prueva = Instantiate(materialprueva);
                        Prueva.name = "Habitacion " + NumeroSenda;
                        ListaDeObjetos.Add(Prueva);
                        Prueva.transform.position = new Vector3(i / Anchoboton, Prueva.transform.position.y, j / -AltoBoton);

                        #region NoVEr.
                        if (GameObject.Find("Habitacion " + (NumeroSenda - 10)))
                        {
                            Selection.activeGameObject = GameObject.Find("Habitacion " + NumeroSenda);

                            Transform trans = GameObject.Find("Habitacion " + (NumeroSenda - 10)).GetComponentInChildren<Transform>().GetChild(3);
                            Transform trans2 = GameObject.Find("Habitacion " + NumeroSenda).GetComponentInChildren<Transform>().GetChild(2);

                            _ParedDerecha = trans.gameObject;
                            _VParedIzquierda = trans2.gameObject;
                        }
                        if (GameObject.Find("Habitacion " + (NumeroSenda + 10)))
                        {
                            Selection.activeGameObject = GameObject.Find("Habitacion " + NumeroSenda);
                            Transform trans = GameObject.Find("Habitacion " + (NumeroSenda + 10)).GetComponentInChildren<Transform>().GetChild(2);
                            Transform trans2 = GameObject.Find("Habitacion " + NumeroSenda).GetComponentInChildren<Transform>().GetChild(3);
                            _ParedIzquierda = trans.gameObject;
                            _VParedDerecha = trans2.gameObject;
                        }
                        if (GameObject.Find("Habitacion " + (NumeroSenda + 1)))
                        {
                            Selection.activeGameObject = GameObject.Find("Habitacion " + NumeroSenda);
                            Transform trans = GameObject.Find("Habitacion " + (NumeroSenda + 1)).GetComponentInChildren<Transform>().GetChild(1);
                            Transform trans2 = GameObject.Find("Habitacion " + NumeroSenda).GetComponentInChildren<Transform>().GetChild(0);
                            _ParedArriba = trans.gameObject;
                            _VParedAbajo = trans2.gameObject;
                        }
                        if (GameObject.Find("Habitacion " + (NumeroSenda - 1)))
                        {
                            Selection.activeGameObject = GameObject.Find("Habitacion " + NumeroSenda);
                            Transform trans = GameObject.Find("Habitacion " + (NumeroSenda - 1)).GetComponentInChildren<Transform>().GetChild(0);
                            Transform trans2 = GameObject.Find("Habitacion " + NumeroSenda).GetComponentInChildren<Transform>().GetChild(1);
                            _ParedAbajo = trans.gameObject;
                            _VParedArriba = trans2.gameObject;
                        }
                        #endregion
                    }
                    else
                    {
                        DestroyImmediate(GameObject.Find("Habitacion " + NumeroSenda));
                    }
                }
            }
        }

        Handles.EndGUI();
        GUI.EndGroup();
        #endregion

        #region test
        _CurrentFloor = Selection.activeGameObject;

        if (_CurrentFloor)
            EditorGUILayout.LabelField("El nombre del piso actual es :" + _CurrentFloor.name);

        Repaint();

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
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
        EditorGUILayout.EndHorizontal();
        #endregion

    }

    #region Ajuste de grilla
    Vector3 ReajustVector2(Vector2 _v2)
    { //Ajusta el vector2 a v3 y lo reduce
        Vector3 _v3;
        _v3.x = _v2.x / 6;
        _v3.y = 0;
        _v3.z = _v2.y / 6;
        return _v3;
    }
    #endregion
}

