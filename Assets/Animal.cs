using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    float lastFall = 0;

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.name == gameObject.name)
        {
            if (!enabled)
            {
                Debug.Log("animal is inactive, skip collision handling");
                return;
            } else {
                ScoreBoard.score += 10;
                Destroy(other.gameObject);
                Destroy(gameObject);
                FindObjectOfType<Dropper>().dropNext();
            }
           
        }
    }

    bool isValidGridPos() {        
        foreach (Transform child in transform) {
            Vector2 v = PlayField.roundVec2(child.position);

            
            if (!PlayField.insideBorder(v))
                return false;

            
            if (PlayField.grid[(int)v.x, (int)v.y] != null &&
                PlayField.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }
    
    void updateGrid() {
        
        for (int y = 0; y < PlayField.h; ++y)
            for (int x = 0; x < PlayField.w; ++x)
                if (PlayField.grid[x, y] != null)
                    if (PlayField.grid[x, y].parent == transform)
                        PlayField.grid[x, y] = null;

        
        foreach (Transform child in transform) {
            Vector2 v = PlayField.roundVec2(child.position);
            PlayField.grid[(int)v.x, (int)v.y] = child;
        }        
    }
    

    // Update is called once per frame
    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            
            transform.position += new Vector3(-1, 0, 0);
        
            
            if (isValidGridPos()){
                updateGrid();
            } else {
                transform.position += new Vector3(1, 0, 0);
            }
                
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            
            transform.position += new Vector3(1, 0, 0);
        
            if (isValidGridPos()){
                updateGrid();
            } else {
                transform.position += new Vector3(-1, 0, 0);
            }
                
        }

        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {

            transform.Rotate(0, 0, -90);
            
            if (isValidGridPos()){
                updateGrid();
            } else {
                transform.Rotate(0, 0, 90);
            }
                
        }

        // Move Downwards and Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1) {
           
            transform.position += new Vector3(0, -1, 0);

            if (isValidGridPos()) {

                updateGrid();

            } else {
                transform.position += new Vector3(0, 1, 0);
                
                FindObjectOfType<Dropper>().dropNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
    }
}
