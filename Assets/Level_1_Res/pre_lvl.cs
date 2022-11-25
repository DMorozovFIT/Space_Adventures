using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pre_lvl : MonoBehaviour
{
    [SerializeField]
    public AudioSource aS;
    [SerializeField]
    public Button next_btn;
    int startTime;

    // Start is called before the first frame update

    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -1);
    }
    void Start()
    {
        //startTime = System.Environment.TickCount;
        //next_btn.enabled = false;
        aS.PlayOneShot(Resources.Load<AudioClip>("1.4"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //  if (System.Environment.TickCount - startTime > 10000) next_btn.enabled = true;
        
    }
}
