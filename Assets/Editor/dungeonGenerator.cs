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
    Vector2 offset = new Vector2();
    Rect rekt;

    List<Vector2> positions = new List<Vector2>();
    List<Vector2> CircleHandless = new List<Vector2>();

    public List<GameObject> FloorList = new List<GameObject>();

    int linesEvery = 40;
    int thiccLinesEvery = 5;

    [ExecuteInEditMode]
    [MenuItem("Terrain/Generador de Dungeon")]

    static void CreateWindow()
    {
        ((dungeonGenerator)GetWindow(typeof(dungeonGenerator))).Show();
    }


    private void OnGUI()
    {
        _floor = (GameObject)Resources.Load("Piso");
        _wall = (GameObject)Resources.Load("Pared");
        _door = (GameObject)Resources.Load("Puerta");

        int thickLines = 0;

        _floor = (GameObject)EditorGUILayout.ObjectField("Piso: ", _floor, typeof(GameObject), true);
        if (!_floor)
        {
            EditorGUILayout.HelpBox("Seleccione un píso", MessageType.Warning);
        }
        EditorGUILayout.Space();

        _wall = (GameObject)EditorGUILayout.ObjectField("Pared: ", _wall, typeof(GameObject), true);
        if (!_wall)
        {
            EditorGUILayout.HelpBox("Seleccione una puerta", MessageType.Warning);
        }
        EditorGUILayout.Space();

        _door = (GameObject)EditorGUILayout.ObjectField("Puerta: ", _door, typeof(GameObject), true);
        if (!_door)
        {
            EditorGUILayout.HelpBox("Seleccione una pared", MessageType.Warning);
        }
        EditorGUILayout.Space();


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
            for (int i = 0; i < FloorList.Count; i++)
            {
                DestroyImmediate(FloorList[i]);
            }
            FloorList.Clear();
            positions.Clear();
            CircleHandless.Clear();
            Repaint();
        }
        GUILayout.Label("Clean window");
        EditorGUILayout.EndHorizontal();


        Rect cleanSceneRekt = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(cleanSceneRekt, GUIContent.none))
        {
            for (int i = 0; i < FloorList.Count; i++)
            {
                DestroyImmediate(FloorList[i]);
            }
            for (int i = 0; i < positions.Count; i++)
            {
                DestroyImmediate(FloorList[i]);
            }
            CircleHandless.Clear();
        }
        GUILayout.Label("Clear scene");
        EditorGUILayout.EndHorizontal();


        Rect ClearlastPref = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(ClearlastPref, GUIContent.none))
        {
            DestroyImmediate(FloorList[FloorList.Count - 1]);
            FloorList.RemoveAt(FloorList.Count - 1);
            CircleHandless.RemoveAt(CircleHandless.Count - 1);
        }
        GUILayout.Label("Remove last biuld");
        EditorGUILayout.EndHorizontal();


        ////boton para generar cositas
        Rect generateRekt = EditorGUILayout.BeginHorizontal("Button");
        if (GUI.Button(generateRekt, GUIContent.none))
        {
            foreach (var p in positions)
            {
                _floor.transform.position = ReajustVector2(p);
                GameObject currentfloor = Instantiate(_floor);
                FloorList.Add(currentfloor);
            }
            positions.Clear();
        }
        GUILayout.Label("Generate");
        EditorGUILayout.EndHorizontal();


        #endregion

        GUI.BeginGroup(rekt);
        #region Creador de lineas
        //Lineas en sí
        Handles.BeginGUI();
        for (int i = (int)(0 + offset.x); i < position.width + offset.x; i += linesEvery)
        {
            Handles.DrawLine(new Vector3(i, 0, 0), new Vector3(i, position.height, 0));
            if (thiccLinesEvery == 0 || thickLines % thiccLinesEvery == 0)
            {
                Handles.DrawLine(new Vector3(i - 1, 0, 0), new Vector3(i - 1, position.height, 0));
                Handles.DrawLine(new Vector3(i + 1, 0, 0), new Vector3(i + 1, position.height, 0));
            }
            thickLines++;
        }
        thickLines = 0;
        for (int i = (int)(0 + offset.y); i < position.height + offset.y; i += linesEvery)
        {

            Handles.DrawLine(new Vector3(0, i, 0), new Vector3(position.width, i, 0));
            if (thiccLinesEvery == 0 || thickLines % thiccLinesEvery == 0)
            {
                Handles.DrawLine(new Vector3(0, i - 1, 0), new Vector3(position.width, i - 1, 0));
                Handles.DrawLine(new Vector3(0, i + 1, 0), new Vector3(position.width, i + 1, 0));
            }
            thickLines++;
        }
        #endregion

        #region puntos rojos sobre la grilla

        //Los puntitos rojos que proximamente van a ser cuadrados
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

