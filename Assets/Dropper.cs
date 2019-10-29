using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public GameObject[] animals;
    public int nextId;

	
	void Start () {
        nextId = Random.Range(0, animals.Length);
        dropNext ();
	}
	

    public GameObject createAnimal(Vector3 v) {
        GameObject animal = Instantiate(animals[nextId], v, Quaternion.identity);
        return animal;
    }

	public void dropNext() {
        createAnimal(transform.position);
        nextId = Random.Range(0, animals.Length);
	}
}
