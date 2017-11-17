using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public class VentanaDePuertas : EditorWindow {

    public int caca;

    [ExecuteInEditMode]
    [MenuItem("Terrain/Botonera")]


    static void CreateWindow()
    {
        ((VentanaDePuertas)GetWindow(typeof(VentanaDePuertas))).Show();
    }

    private void OnGUI()
    {
        
    }
}
