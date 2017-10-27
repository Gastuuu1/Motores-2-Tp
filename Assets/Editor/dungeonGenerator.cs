using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class dungeonGenerator : EditorWindow {
    public GameObject _floor;
    public GameObject _wall;
    public GameObject _door;
    Vector2 offset = new Vector2();
    Rect rekt;

    int linesEvery = 40;
    int thiccLinesEvery = 5;

    [ExecuteInEditMode]
    [MenuItem ( "Terrain/Generador de Dungeon" )]

    static void CreateWindow() {
        ((dungeonGenerator) GetWindow ( typeof ( dungeonGenerator ) )).Show ();
    }


    private void OnGUI() {
        int thickLines = 0;


        _floor = (GameObject) EditorGUILayout.ObjectField ( "Piso: ", _floor, typeof ( GameObject ), true );
        if ( !_floor ) {
            EditorGUILayout.HelpBox ( "Seleccione un píso", MessageType.Warning );
        }
        EditorGUILayout.Space ();

        _wall = (GameObject) EditorGUILayout.ObjectField ( "Pared: ", _wall, typeof ( GameObject ), true );
        if ( !_wall ) {
            EditorGUILayout.HelpBox ( "Seleccione una puerta", MessageType.Warning );
        }
        EditorGUILayout.Space ();

        _door = (GameObject) EditorGUILayout.ObjectField ( "Puerta: ", _door, typeof ( GameObject ), true );
        if ( !_door ) {
            EditorGUILayout.HelpBox ( "Seleccione una pared", MessageType.Warning );
        }
        EditorGUILayout.Space ();
        //Lineas 
        GUI.BeginGroup(rekt);
        if ( !_door && !_wall && !_floor ) {
            rekt = new Rect(20, 220, 400, 400);
            maxSize = new Vector2(440, 650);
            minSize = new Vector2(440, 650);
            Repaint();
        } else if ( (!_door && !_wall) || (!_floor && !_wall) || (!_floor && !_door) ) {
            rekt = new Rect(20, 170, 400, 400);
            maxSize = new Vector2(440, 590);
            minSize = new Vector2(440, 590);
            Repaint();

        } else if ( !_door || !_wall || !_floor ) {
            rekt = new Rect(20, 125, 400, 400);
            maxSize = new Vector2(440, 550);
            minSize = new Vector2(440, 550);
            Repaint();

        } else {
        rekt = new Rect(20, 85, 400, 400);
            maxSize = new Vector2(440, 510);
            minSize = new Vector2(440, 510);
            Repaint();
        }


        Handles.BeginGUI();
        for ( int i = (int) (0 + offset.x); i < position.width + offset.x; i += linesEvery ) {
            Handles.DrawLine(new Vector3(i, 0, 0), new Vector3(i, position.height, 0));
            if ( thiccLinesEvery == 0 || thickLines % thiccLinesEvery == 0 ) {
                Handles.DrawLine(new Vector3(i - 1, 0, 0), new Vector3(i - 1, position.height, 0));
                Handles.DrawLine(new Vector3(i + 1, 0, 0), new Vector3(i + 1, position.height, 0));
            }
            thickLines++;
        }
        thickLines = 0;
        for ( int i = (int) (0 + offset.y); i < position.height + offset.y; i += linesEvery ) {

            Handles.DrawLine(new Vector3(0, i, 0), new Vector3(position.width, i, 0));
            if ( thiccLinesEvery == 0 || thickLines % thiccLinesEvery == 0 ) {
                Handles.DrawLine(new Vector3(0, i - 1, 0), new Vector3(position.width, i - 1, 0));
                Handles.DrawLine(new Vector3(0, i + 1, 0), new Vector3(position.width, i + 1, 0));
            }
            thickLines++;
        }
        Handles.EndGUI();
        GUI.EndGroup();

    }
}
