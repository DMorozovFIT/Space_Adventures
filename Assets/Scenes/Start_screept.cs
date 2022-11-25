using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_screept : MonoBehaviour
{
    int startTime;
    void Start()
    {
        startTime = System.Environment.TickCount;
    }

    

    private void FixedUpdate()
    {
        if (System.Environment.TickCount - startTime > 500)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
