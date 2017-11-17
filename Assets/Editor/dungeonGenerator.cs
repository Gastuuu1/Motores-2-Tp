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

    public Texture BotonUsado;
    Vector2 offset = new Vector2();
    int AltoBoton = 30;
    int Anchoboton = 40;
    Rect rekt;

    public List<GameObject> ListaDeObjetos = new List<GameObject>();
    public List<GameObject> ListaVecinos = new List<GameObject>();

    List<Vector2> positions = new List<Vector2>();
    List<Vector2> CircleHandless = new List<Vector2>();

    public List<GameObject> FloorList = new List<GameObject>();

    int linesEvery = 40;
    int thiccLinesEvery = 5;


    public bool draw;
    public Vector2 drawing;

    [ExecuteInEditMode]
    [MenuItem("Terrain/Generador de Dungeon")]

    static void CreateWindow()
    {
        ((dungeonGenerator)GetWindow(typeof(dungeonGenerator))).Show();
    }

    private void OnGUI()
    {
        materialprueva = (GameObject)Resources.Load("Cube");
        _floor = (GameObject)Resources.Load("Piso");
        _wall = (GameObject)Resources.Load("Pared");
        _door = (GameObject)Resources.Load("Puerta");

        _floor = (GameObject)EditorGUILayout.ObjectField("Piso: ", _floor, typeof(GameObject), true);

       /* if (!_floor)
        {
            EditorGUILayout.HelpBox("Seleccione un píso", MessageType.Warning);
        }
        */
        EditorGUILayout.Space();

        _wall = (GameObject)EditorGUILayout.ObjectField("Pared: ", _wall, typeof(GameObject), true);
        /*if (!_wall)
        {
            EditorGUILayout.HelpBox("Seleccione una puerta", MessageType.Warning);
        }
        */
        EditorGUILayout.Space();

        _door = (GameObject)EditorGUILayout.ObjectField("Puerta: ", _door, typeof(GameObject), true);
        /*if (!_door)
        {
            EditorGUILayout.HelpBox("Seleccione una pared", MessageType.Warning);
        }
        */
        EditorGUILayout.Space();


        if (GUILayout.Button("Tocar"))

        {
            

            ((VentanaCreadora)GetWindow(typeof(VentanaCreadora))).Show();

            GUILayout.Label("Abrir la otra ventana");
            EditorGUILayout.EndHorizontal();
        }
        //    BotonUsado = (Texture)EditorGUILayout.ObjectField("TexturaImagen", BotonUsado, typeof(Texture), true);

        //Lineas 

        #region Cosa de los if
        //Recalcular segun las alertas 
        if (!_door && !_wall && !_floor)
        {
            rekt = new Rect(20, 220, 400, 500);
            maxSize = new Vector2(440, 650);
            minSize = new Vector2(440, 650);
            Repaint();
        }
        else if ((!_door && !_wall) || (!_floor && !_wall) || (!_floor && !_door))
        {
            rekt = new Rect(20, 170, 400, 500);
            maxSize = new Vector2(440, 590);
            minSize = new Vector2(440, 590);
            Repaint();

        }
        else if (!_door || !_wall || !_floor)
        {
            rekt = new Rect(20, 125, 400, 500);
            maxSize = new Vector2(440, 550);
            minSize = new Vector2(440, 550);
            Repaint();

        }
        else
        {
            rekt = new Rect(20, 200, 400, 500);
            maxSize = new Vector2(440, 530);
            minSize = new Vector2(440, 530);
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
        GUILayout.Label("Clean window");
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
        GUILayout.Label("Clear scene");
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
        //   GUI.color = Color.green;
        for (int j = 0; j < 300; j += AltoBoton)
        {

            for (int i = 0; i < 400; i += Anchoboton)
            {
                Rect Rectangulo = new Rect(i, j, Anchoboton, AltoBoton);
                if (GUI.Button(Rectangulo, "" + i / Anchoboton + j / AltoBoton))
                {                    
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
                        Debug.Log("Se Toco Boton: " + i / Anchoboton + j / AltoBoton);

                        #region NoVEr.
                        if (GameObject.Find("Habitacion " + (NumeroSenda - 10)))
                        {
                            Selection.activeGameObject = GameObject.Find("Habitacion "+NumeroSenda);

                            Transform trans = GameObject.Find("Habitacion " + (NumeroSenda - 10)).GetComponentInChildren<Transform>().GetChild(3);
                            Transform trans2 = GameObject.Find("Habitacion " + NumeroSenda).GetComponentInChildren<Transform>().GetChild(2);
                           
                            VentanaCreadora._ParedDerecha = trans.gameObject;
                            VentanaCreadora._VParedIzquierda = trans2.gameObject;
                        }
                        if (GameObject.Find("Habitacion " + (NumeroSenda + 10)))
                        {
                            Selection.activeGameObject = GameObject.Find("Habitacion " + NumeroSenda);
                            Transform trans = GameObject.Find("Habitacion " + (NumeroSenda + 10)).GetComponentInChildren<Transform>().GetChild(2);
                            Transform trans2 = GameObject.Find("Habitacion " + NumeroSenda).GetComponentInChildren<Transform>().GetChild(3);
                            VentanaCreadora._ParedIzquierda = trans.gameObject;
                            VentanaCreadora._VParedDerecha = trans2.gameObject;
                        }
                        if (GameObject.Find("Habitacion " + (NumeroSenda + 1)))
                        {
                            Selection.activeGameObject = GameObject.Find("Habitacion " + NumeroSenda);
                            Transform trans = GameObject.Find("Habitacion " + (NumeroSenda + 1)).GetComponentInChildren<Transform>().GetChild(1);
                            Transform trans2 = GameObject.Find("Habitacion " + NumeroSenda).GetComponentInChildren<Transform>().GetChild(0);
                            VentanaCreadora._ParedArriba = trans.gameObject;
                            VentanaCreadora._VParedAbajo = trans2.gameObject;                           
                        }
                        if (GameObject.Find("Habitacion " + (NumeroSenda - 1)))
                        {
                            Selection.activeGameObject = GameObject.Find("Habitacion " + NumeroSenda);
                            Transform trans = GameObject.Find("Habitacion " + (NumeroSenda - 1)).GetComponentInChildren<Transform>().GetChild(0);
                            Transform trans2 = GameObject.Find("Habitacion " + NumeroSenda).GetComponentInChildren<Transform>().GetChild(1);
                            VentanaCreadora._ParedAbajo = trans.gameObject;
                            VentanaCreadora._VParedArriba = trans2.gameObject;
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

        #region puntos rojos sobre la grilla

        /*    //Los puntitos rojos que proximamente van a ser cuadrados
            if (!Event.current.control && Event.current.button == 0 && Event.current.type == EventType.mouseDown)
            {
                int posx = (int)Event.current.mousePosition.x;
                int posy = (int)Event.current.mousePosition.y;

                positions.Add(new Vector2((posx), (posy)));
                CircleHandless.Add(new Vector2((posx), (posy)));
                Repaint();
            }
            foreach (var pos in CircleHandless)
            {
                Handles.color = Color.red;
                Handles.DrawSolidDisc(pos + offset, Vector3.forward, 2);
                var col = Color.red;
                col.a = .1f;
                Handles.color = col;
                Handles.CubeHandleCap(0, pos + offset, Quaternion.identity, 200, EventType.ContextClick);  //keondaestojelpmi            
            }
            Handles.EndGUI();
            GUI.EndGroup();
            */
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

