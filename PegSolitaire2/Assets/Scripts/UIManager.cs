using UnityEngine;
using System.IO;
using TMPro;


public class UIManager : MonoBehaviour
{   
    private int uiState;
    private GameObject[] menus;

    public GameObject savedGamelabel;

    public int getUiState(){
        return uiState;
    }

    public void setUiState(int newUiState){
        this.uiState = newUiState;
    }

    // Start is called before the first frame update
    void Start()
    {   
        uiState = 0;
        getMenus();
        changeMenu();
    }

    // Update is called once per frame
    void Update()
    {
        showHideInGameMenu();
    }

    //Make deactive all menus except the opening one
    public void changeMenu(){
        
        for (int i = 0; i < menus.Length; ++i){
            if(i == uiState)
                menus[i].SetActive(true);
            else
                menus[i].SetActive(false);
        }
    }

    //Fetch all menus
    private void getMenus(){

        int childCount = transform.childCount;

        menus = new GameObject[childCount];

        for (int i = 0; i < childCount; ++i)
            menus[i] =  transform.GetChild(i).gameObject;

    }

    //Fecth saved games and instantiate saved game labels on load game menu
    public void initializeLoadGameMenu(){
        string[] gameNames = fetchSavedGames();

        GameObject savedGamesContainer = GameObject.FindGameObjectWithTag("SavedGames");
        savedGamesContainer.transform.DetachChildren();

        for(int i=0;i<gameNames.Length;i++){
            GameObject label = GameObject.Instantiate(savedGamelabel,savedGamesContainer.transform.position, Quaternion.identity);
            label.transform.SetParent(savedGamesContainer.transform,false);
            label.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gameNames[i].Split('.')[0];
        }
        
    }

    //If 'ESC' is pressed when playing match show  a menu which we can return home or save game
    private void showHideInGameMenu(){
        if(Input.GetKeyDown(KeyCode.Escape) && (uiState == -1 || uiState == 4)){
            if(menus[4].activeSelf == true)
                uiState = -1;
            else
                uiState = 4;
        }
        changeMenu();
    }

    //Change position of score menu to see board
    public void showHideScoreMenu(){
        if(menus[5].transform.localPosition.x > 3.3f ){
            menus[5].transform.localPosition = new Vector3(-2.92f,0.70f,-7.84f);
        }
        else{
            menus[5].transform.localPosition = new Vector3(3.35f,2.28f,-3.46f);
        }
    }

    //Quit game
    public void quitGame(){
        Application.Quit();
    }

    //Fetch all saved game's names
    public static string[] fetchSavedGames(){
        string[] savedGameNames;
        
        //Get Path
        string savingPath = Path.Combine(Application.persistentDataPath, "data");

        //Access path
        DirectoryInfo info = new DirectoryInfo(savingPath);

        //Fetch information of files
        FileInfo[] files = info.GetFiles();

        //Define
        savedGameNames = new string[files.Length];

        //Add file names to array
        for(int i=0;i<files.Length;i++){
            savedGameNames[i] = files[i].Name;
        }

        return savedGameNames;
    }
    
    //Reach file and delete
    public static void deleteSavedGame(string gameName){
        //Get Path
        string path = Path.Combine(Application.persistentDataPath, "data");
        path = Path.Combine(path, gameName + ".txt");

        File.Delete(path);
    }

}
