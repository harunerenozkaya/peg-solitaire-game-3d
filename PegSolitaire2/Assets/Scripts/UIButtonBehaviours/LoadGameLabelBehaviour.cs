using UnityEngine;
using TMPro;

public class LoadGameLabelBehaviour : MonoBehaviour
{   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick(){
        //Load game
        string gameName = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().loadGame(gameName);

        //Destroy label prefab objects
        GameObject[] labels = GameObject.FindGameObjectsWithTag("SavedGameLabel");
        for(int i=0;i<labels.Length;i++){
            Destroy(labels[i]);
        }

        //Change menu and show game
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().setUiState(-1);
        GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>().changeMenu();

        
    }
}
