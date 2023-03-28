using UnityEngine;
using TMPro;

public class DeleteSavedGameButtonBehaviour : MonoBehaviour
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
        string gameName = gameObject.transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        UIManager.deleteSavedGame(gameName);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
