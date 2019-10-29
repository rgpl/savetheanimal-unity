using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAnimal: MonoBehaviour
{
    private Dropper dropper;
    private GameObject currentAnimal;
    private int currentAnimalId; 

    void Awake () {
        dropper = FindObjectOfType<Dropper>();
	}


    void createStoppedAnimal () {
        
        currentAnimal = dropper.createAnimal(transform.position);
        currentAnimalId = dropper.nextId;
        
        var animal = (Animal) currentAnimal.GetComponent(typeof(Animal));
        
        animal.enabled = false;
    }


    void deleteCurrentAnimal() {
        Destroy(currentAnimal);
    }

    void Start() {
        createStoppedAnimal();
    }
	
	// Update is called once per frame
	void Update () {
        if (currentAnimalId != dropper.nextId) {
            deleteCurrentAnimal();
            createStoppedAnimal();
        }
	}
}
