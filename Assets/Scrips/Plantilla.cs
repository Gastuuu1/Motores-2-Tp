using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plantilla : MonoBehaviour {

    public List<GameObject> ListaPiso;

    public int _AnchoDeNivel;
    public int _LargoDeNivel;

    public GameObject Piso;
    public GameObject Pared;
    public GameObject Puerta;
        
    void Start ()
    {
        Piso = GameObject.Find("Plane");
        Pared = GameObject.Find("Pared");
        Puerta = GameObject.Find("Puerta");       
    }
		
	void Update ()
    {
		
	}
}
