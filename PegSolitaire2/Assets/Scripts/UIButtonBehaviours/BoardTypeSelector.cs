using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTypeSelector : MonoBehaviour
{   
    public int boardType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick(){
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().gameData.setBoardType(boardType);
    }
}
