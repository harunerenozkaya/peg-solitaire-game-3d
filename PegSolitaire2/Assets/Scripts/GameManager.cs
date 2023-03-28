using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;


public class GameManager : MonoBehaviour
{   
    public Board gameData;
    private bool gameStatus { get; set; }
    
    private int uiState { get; set; } 
    private bool isInitialized { get; set; } 
    private bool isNewGame  { get; set; } 

    public Canvas canvas;
    public GameObject cellWithPegPrefab;
    public GameObject cellPrefab;

    public GameObject selectedPeg;
    public GameObject selectedCell;

    private bool isMoveable { get; set;}
    private Vector3 middleCellLocation { get; set; } 

    //Default Constructor
    public GameManager(){}

    //Copy Constructor
    public GameManager(GameManager manager){
        this.gameData = manager.gameData;
        this.gameStatus = manager.gameStatus;
        this.uiState = manager.uiState;
        this.isInitialized = manager.isInitialized;
        this.isNewGame = manager.isNewGame;
        this.canvas = manager.canvas;
        this.cellWithPegPrefab = manager.cellWithPegPrefab;
        this.cellPrefab = manager.cellPrefab;
        this.selectedPeg = manager.selectedPeg;
        this.selectedCell = manager.selectedCell;
        this.isMoveable = manager.isMoveable;
        this.middleCellLocation = manager.middleCellLocation;
    }
    
    // Start is called before the first frame update
    void Start()
    {   
        gameData = new Board();
        selectedPeg = null;
        selectedCell = null;
        gameStatus = true;
        isInitialized = false;
        isNewGame = true;
        isMoveable = false;
        middleCellLocation = Vector3.negativeInfinity;
    }

    // Update is called once per frame
    void Update()
    {   
        //Get uiState
        uiState = canvas.GetComponent<UIManager>().getUiState();
        
        //When Ui is not showing
        if(uiState == -1){
            
            //If board is not initialized yet
            if(isInitialized == false){
                
                //If game is new game no loaded
                if(isNewGame == true)
                    initializeBoard();

                //Create board on table
                createBoard();

                isInitialized = true;
            }


            //If player is computer
            if(gameData.getPlayerType() == 0)
                generateMovement();
            

            //If cell and peg is selected
            if(selectedCell != null && selectedPeg != null)
                isValidMovement();


            //If Movement is moveable
            if(isMoveable){
                movePeg();
                checkIsGameFinished();
            }    
            

            //If game is finish
            if(gameStatus == false)
                showScore();
                
        }
    }

    // If game is new game then fill board array according to selected board type
    private void initializeBoard(){
        switch (gameData.getBoardType()){
            case 1:
                List<CellTypes>[] board1 = {
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Empty,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out}
                };

                gameData.setBoard(board1);
                break;
            
            case 2:
                List<CellTypes>[] board2 = {
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Empty,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out}
                };
                
                //Board is too big so change start corner rather than 0,0,0 to fit in world
                gameData.setSpawnStartPoint(new Vector3(-1.3f,0,-0.6f));
                gameData.setBoard(board2);
                break;
            
            case 3:
                List<CellTypes>[] board3 = {
        
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Empty,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                };

                //Board is too big so change start corner rather than 0,0,0 to fit in world
                gameData.setSpawnStartPoint(new Vector3(0,0,-0.6f));
                gameData.setBoard(board3);
                break;
            
            case 4:
                List<CellTypes>[] board4 = {
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Empty,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out}
                };

                gameData.setBoard(board4);
                break;
            
            case 5:
                List<CellTypes>[] board5 = {
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Empty,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Peg,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out},
                    new List<CellTypes>{CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Peg,CellTypes.Out,CellTypes.Out,CellTypes.Out,CellTypes.Out}
                };
                
                //Board is too big so change start corner rather than 0,0,0 to fit in world
                gameData.setSpawnStartPoint(new Vector3(-1.3f,0,-0.6f));
                gameData.setBoard(board5);
                break;
        }
    }

    //Instantiate objects in world according to board array
    private void createBoard(){
        int[] location = new int[2];

        for(int i=0;i<gameData.getBoard().Count;i++){
            for(int j=0;j<gameData.getBoard()[0].Count;j++){

                location[0] = i;
                location[1] = j;

                switch(gameData.getBoard()[i][j]){
                    case CellTypes.Peg:
                        GameObject.Instantiate(cellWithPegPrefab,Translator.arrayLocationToVector(i,j,gameData.getSpawnStartPoint()),Quaternion.identity);
                        break;
                    case CellTypes.Empty:
                        GameObject.Instantiate(cellPrefab,Translator.arrayLocationToVector(i,j,gameData.getSpawnStartPoint()),Quaternion.identity);
                        break;
                }
            }
        }
    }
    
    //Generate random movement when computer plays
    private void generateMovement(){
        bool isFound = false;
        int x = 0,y = 0,i = 0,j = 0;

        GameObject[] spawnedAllCells = GameObject.FindGameObjectsWithTag("Cell");

        //Find a movemenent moveable
        while(isFound == false){
            x = UnityEngine.Random.Range(0,gameData.getBoard().Count);
            y = UnityEngine.Random.Range(0,gameData.getBoard().Count);

            i = UnityEngine.Random.Range(0,gameData.getBoard().Count);
            j = UnityEngine.Random.Range(0,gameData.getBoard().Count);

            //If first coordinate is peg and second one is empty cell
            if(gameData.getBoard()[x][y] == CellTypes.Peg && gameData.getBoard()[i][j] == CellTypes.Empty)
            {   
                //If selected coordinate's vertical values are same and middle of them is peg so moveable movement is found
                if(x==i && gameData.getBoard()[x][(y+j)/2] == CellTypes.Peg){
                    isFound = true;
                }

                //If selected coordinate's horiizontal values are same and middle of them is peg so moveable movement is found
                else if(y==j && gameData.getBoard()[(x+i)/2][y] == CellTypes.Peg){
                    isFound = true;
                }
            }
        }
        
        //Reach game object
        for(int k=0;k<spawnedAllCells.Length;k++){
            //If looking gameobject's position is same with selected peg in array then it is selected peg object
            if(Vector3.Distance(spawnedAllCells[k].transform.position,Translator.arrayLocationToVector(x,y,gameData.getSpawnStartPoint())) < 0.1f){
                selectedPeg = spawnedAllCells[k].transform.GetChild(0).gameObject;
                selectedPeg.GetComponent<PegBehaviours>().isSelected = true;
                selectedPeg.GetComponent<PegBehaviours>().changeMaterialToRed();
            }
            //If looking gameobject's position is same with selected cell in array then it is selected cell object
            else if(Vector3.Distance(spawnedAllCells[k].transform.position,Translator.arrayLocationToVector(i,j,gameData.getSpawnStartPoint())) < 0.1f){
                selectedCell = spawnedAllCells[k].gameObject;
            }
        }
    }

    //Check the movement if it is moveable
    private void isValidMovement(){
        //If distance with selected peg and cell is 2 block
        if(Vector3.Distance(selectedCell.transform.position,selectedPeg.transform.position) > 2.39 && Vector3.Distance(selectedCell.transform.position,selectedPeg.transform.position) < 2.42){

            GameObject[] spawnedPegs = GameObject.FindGameObjectsWithTag("Peg");

            //Is there middle peg ? if there is then save its position
            for(int i=0;i<spawnedPegs.Length;i++){
                if(Vector3.Distance(spawnedPegs[i].transform.position,new Vector3((selectedCell.transform.position.x + selectedPeg.transform.position.x)/2,0.3f,(selectedCell.transform.position.z + selectedPeg.transform.position.z)/2)) < 0.3f){
                    middleCellLocation = spawnedPegs[i].transform.parent.position;
                    isMoveable = true;
                    break;
                }
            }
        }

        //If it is not moveable movement so reinitialize values
        if(isMoveable == false){
            selectedCell = null;
            selectedPeg.GetComponent<PegBehaviours>().isSelected = false;
            selectedPeg = null;
            middleCellLocation = Vector3.negativeInfinity;
        }
    }
    
    //Move peg
    private void movePeg(){
        int[] position = new int[2];
        GameObject[] spawnedPegs = GameObject.FindGameObjectsWithTag("Peg");

        //Change board array properties
        position = Translator.vectorToArrayLocation(selectedPeg.transform.parent.position,gameData.getSpawnStartPoint());
        gameData.getBoard()[position[0]][position[1]] = CellTypes.Empty;
        
        position = Translator.vectorToArrayLocation(selectedCell.transform.position,gameData.getSpawnStartPoint());
        gameData.getBoard()[position[0]][position[1]] = CellTypes.Peg;

        position = Translator.vectorToArrayLocation(middleCellLocation,gameData.getSpawnStartPoint());
        gameData.getBoard()[position[0]][position[1]] = CellTypes.Empty;

        //Change peg location and be empty cells
        selectedPeg.transform.parent.GetComponent<CellBehaviour>().isEmpty = true;
        selectedCell.GetComponent<CellBehaviour>().isEmpty = false;
        selectedPeg.transform.parent.DetachChildren();
        selectedPeg.transform.SetParent(selectedCell.transform);
        selectedPeg.transform.position = new Vector3(selectedCell.transform.position.x,0.2f,selectedCell.transform.position.z);

        //Find middle peg and destroy
        for(int i=0;i<spawnedPegs.Length;i++){
            if(Vector3.Distance(spawnedPegs[i].transform.parent.position,middleCellLocation) < 0.1f){
                spawnedPegs[i].transform.parent.GetComponent<CellBehaviour>().isEmpty = true;
                Destroy(spawnedPegs[i]);
                break;
            }
        }

        //Play movement sound
        selectedPeg.GetComponent<AudioSource>().Play();

        //reinitialize values
        selectedCell = null;
        selectedPeg.GetComponent<PegBehaviours>().isSelected = false;
        selectedPeg = null;
        isMoveable = false;
        middleCellLocation = Vector3.negativeInfinity;
    }
    
    //Check the game is finished
    private void checkIsGameFinished(){
        int vertical = gameData.getBoard().Count;
        int horizontal = gameData.getBoard()[0].Count;
        bool isFinish = true;

        for(int i=0; i<vertical && isFinish == true;i++){
            for(int j=0; j<horizontal && isFinish == true; j++){
                
                //If this element is peg
                if(gameData.getBoard()[i][j] == CellTypes.Peg){
                    
                    //If element is not too close the left side then check left side of element
                    if(j<=horizontal-3){

                        if(gameData.getBoard()[i][j+1] == CellTypes.Peg && gameData.getBoard()[i][j+2] == CellTypes.Empty)
                            isFinish = false;
                        
                    }

                    //If element is not too close the right side then check right side of element
                    if(j>=2){

                        if(gameData.getBoard()[i][j-1] == CellTypes.Peg && gameData.getBoard()[i][j-2] == CellTypes.Empty)
                            isFinish = false;
                            
                    }

                    //If element is not too close the down side then check down side of element
                    if(i >= 2){

                        if(gameData.getBoard()[i-1][j] == CellTypes.Peg && gameData.getBoard()[i-2][j] == CellTypes.Empty)
                            isFinish = false;

                    }

                    //If element is not too close the top side then check top side of element
                    if(i <= vertical-3){

                        if(gameData.getBoard()[i+1][j] == CellTypes.Peg && gameData.getBoard()[i+2][j] == CellTypes.Empty)
                            isFinish = false;

                    }
                }
            }
        }

        gameStatus = !isFinish;

    }

    //Calculate total score
    private int calculateScore(){
        int totalCellCount = GameObject.FindGameObjectsWithTag("Cell").Length - 1;
        int remainPegCount = GameObject.FindGameObjectsWithTag("Peg").Length;

        return (totalCellCount - remainPegCount)*10;
    }

    //Show score
    private void showScore(){
        canvas.GetComponent<UIManager>().setUiState(5);
        canvas.GetComponent<UIManager>().changeMenu();
        GameObject.FindGameObjectWithTag("PointText").GetComponent<TextMeshProUGUI>().SetText(calculateScore().ToString());
    }
    
    //Save game progress
    public void saveGame(){
        string gameName = GameObject.FindGameObjectWithTag("SaveGameInput").GetComponent<TMP_InputField>().text;
        gameData.setGameName(gameName);
    
        //Get Path
        string savingPath = Path.Combine(Application.persistentDataPath, "data");
        savingPath = Path.Combine(savingPath, gameName + ".txt");

    
        //If directory is not exist then create directory
        if (!Directory.Exists(Path.GetDirectoryName(savingPath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savingPath));
        }
          
        StreamWriter sw = new StreamWriter(savingPath);

        //Write
        try
        {  
            sw.WriteLine("gameName" + ":" + gameData.getGameName());
            sw.WriteLine("playerType" + ":" + gameData.getPlayerType());
            sw.WriteLine("boardType" + ":" + gameData.getBoardType());
            sw.WriteLine("spawnStartPoint" + ":" + gameData.getSpawnStartPoint().x + "." + gameData.getSpawnStartPoint().y + "." + gameData.getSpawnStartPoint().z);
            sw.WriteLine("boardWidth" + ":" + gameData.getBoard()[0].Count);
            sw.Write("board" + ":");
            

            for(int i = 0; i < gameData.getBoard().Count;i++){
                for(int j = 0; j < gameData.getBoard()[0].Count;j++){
                    switch(gameData.getBoard()[i][j]){
                        case CellTypes.Peg:
                            sw.Write("p");
                            break;
                        case CellTypes.Empty:
                            sw.Write(".");
                            break;
                        case CellTypes.Out:
                            sw.Write("?");
                            break;
                    }
                }
            }

            

            sw.Flush();
            sw.Close();

        }catch (Exception e)
        {
            Debug.LogWarning("Failed To Write Board Data to: " + savingPath);
            Debug.LogWarning("Error: " + e.Message);
        }
    }

    //Load saved game
    public void loadGame(string gameName){
        int boardWidth = 0;
        float x = 0,y=0,z = 0;
        List<CellTypes>[] board;
        List<CellTypes> line;

        //Get Path
        string loadingPath = Path.Combine(Application.persistentDataPath, "data");
        loadingPath = Path.Combine(loadingPath, gameName + ".txt");
        

        //Read
        try
        {  
            StreamReader sr = new StreamReader(loadingPath);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
              
            // To read the whole file line by line
            while (!sr.EndOfStream) 
            {   
                string data = sr.ReadLine();

                switch(data.Split(':')[0]){

                    case "gameName":
                        gameData.setGameName(data.Split(':')[1]);
                        break;

                    case "playerType":
                        gameData.setPlayerType(data.Split(':')[1][0] - '0');
                        break;

                    case "boardType":
                        gameData.setBoardType(data.Split(':')[1][0] - '0');
                        break;

                    case "spawnStartPoint":
                        x = float.Parse(data.Split(':')[1].Split('.')[0]);
                        y = float.Parse(data.Split(':')[1].Split('.')[1]);
                        z = float.Parse(data.Split(':')[1].Split('.')[2]);
                        
                        gameData.setSpawnStartPoint(new Vector3(x,y,z));               
                        break;

                    case "boardWidth":
                        boardWidth = data.Split(':')[1][0] - '0';
                        break;
                        
                    case "board":
                        board = new List<CellTypes>[data.Split(':')[1].Length/boardWidth];
    
                        for(int i=0;i<data.Split(':')[1].Length/boardWidth;i++){
                            line = new List<CellTypes>();

                            for(int j=0; j < boardWidth; j++){
                                switch(data.Split(':')[1][boardWidth*i + j]){
                                    case 'p':
                                        line.Add(CellTypes.Peg);
                                        break;
                                    case '.':
                                        line.Add(CellTypes.Empty);
                                        break;
                                    case '?':
                                        line.Add(CellTypes.Out);
                                        break;
                                }
                            }
                            
                            board[i] = line;
                        }

                        gameData.setBoard(board);
                        break;
                }
            }

            sr.Close();
            isNewGame = false;

        }catch (Exception e)
        {
            Debug.LogWarning("Failed To Read Board Data from: " + loadingPath);
            Debug.LogWarning("Error: " + e.Message);
        }
    }

    //Destroy all game objects
    public void destroyGame(){
        GameObject[] spawnedAllCells = GameObject.FindGameObjectsWithTag("Cell");

        for(int i=0;i<spawnedAllCells.Length;i++)
            Destroy(spawnedAllCells[i]);
        
        selectedPeg = null;
        selectedCell = null;
        gameStatus = true;
        isInitialized = false;
        isNewGame = true;
        isMoveable = false;
        middleCellLocation = Vector3.negativeInfinity;
        
        gameData.clearBoard();
    }

}
