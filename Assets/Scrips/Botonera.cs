using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botonera : MonoBehaviour {

    public Plantilla _plantilla;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void CrearCuboForward()
    {
        // GameObject BalaReciente;
        // BalaReciente = Instantiate(Plantilla.Piso);
        // BalaReciente.name = _plantilla.ListaPiso[_plantilla.ListaPiso.Count].name;

        for (int i = 0; i < _plantilla._LargoDeNivel; i++)
        {
            for (int j = 0; j < _plantilla._AnchoDeNivel; j++)
            {
                GameObject Obj;
                Obj = Instantiate(_plantilla.Piso);
                Obj.transform.position = new Vector3(Obj.transform.position.x + (10*i),Obj.transform.position.y,Obj.transform.position.z +(10*j));
                Obj.name = "Piso De Indice " + i + " " + j; 
            }
        }               
    }
}
