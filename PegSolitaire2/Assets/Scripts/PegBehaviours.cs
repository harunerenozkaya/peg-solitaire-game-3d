using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegBehaviours : MonoBehaviour
{   
    private GameObject gameManager;
    public bool isSelected = false;

    public Material white;
    public Material red;


    // Start is called before the first frame update
    void Start()
    {
       gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelected == false){
            gameObject.GetComponent<MeshRenderer> ().material = white;
        }
        
    }

    public void OnMouseDown()
    {   
        if(isSelected == false){
            if(gameManager.GetComponent<GameManager>().selectedPeg == null){
                isSelected = true;
                changeMaterialToRed();
                gameManager.GetComponent<GameManager>().selectedPeg = gameObject;
            }
        }
        else{
            isSelected = false;
            changeMaterialToWhite();
            gameManager.GetComponent<GameManager>().selectedPeg = null;
            gameManager.GetComponent<GameManager>().selectedCell = null;
        }
    }

    public void changeMaterialToWhite(){
        gameObject.GetComponent<MeshRenderer> ().material = white;
    }

    public void changeMaterialToRed(){
        gameObject.GetComponent<MeshRenderer> ().material = red;
    }
}
