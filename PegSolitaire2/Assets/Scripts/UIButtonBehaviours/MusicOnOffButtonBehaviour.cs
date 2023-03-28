using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicOnOffButtonBehaviour : MonoBehaviour
{   
    bool isMusicPlaying;
    public Sprite offMusicImage;
    public Sprite onMusicImage;
    // Start is called before the first frame update
    void Start()
    {   
        isMusicPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick(){
        if(isMusicPlaying == true){
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<AudioSource>().volume = 0;
            gameObject.GetComponent<Image>().sprite = onMusicImage;
            isMusicPlaying = false;
        }
        else{
            GameObject.FindGameObjectWithTag("Canvas").GetComponent<AudioSource>().volume = 0.04f;
            gameObject.GetComponent<Image>().sprite = offMusicImage;
            isMusicPlaying = true;
        }
    }
}
