using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBehaviour : MonoBehaviour
{   
    private GameObject gameManager;
    public bool isEmpty = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if(isEmpty == true && gameManager.GetComponent<GameManager>().selectedPeg != null){
            gameManager.GetComponent<GameManager>().selectedCell = gameObject;
        }
    }
}
