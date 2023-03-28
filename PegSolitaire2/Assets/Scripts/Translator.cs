using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int[] vectorToArrayLocation(Vector3 vector , Vector3 spawnStartPoint){
        int[] arrayLocation = { ((int)((vector.x - spawnStartPoint.x)/1.2)) , ((int)((vector.z - spawnStartPoint.z)/1.2))};
        return arrayLocation;
    }

    public static Vector3 arrayLocationToVector(int i ,int j , Vector3 spawnPoint){
        Vector3 vector = new Vector3(i*1.2f + spawnPoint.x,0,j*1.2f + spawnPoint.z);
        return vector;
    }
}
