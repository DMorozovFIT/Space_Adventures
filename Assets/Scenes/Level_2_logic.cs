using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task_2
{
    Piano.Note_light[] ans;
    Piano.Note_light[] notesArr;
    public static int N = 15;

    public Task_2()
    {
        System.Random R = new System.Random();
        notesArr = new Piano.Note_light[N];
        ans = new Piano.Note_light[N];
        int note;
        for(int i=0; i<N; i++)
        {
            note = R.Next() % 7;
            notesArr[i] = (Piano.Note_light)Enum.GetValues(typeof(Piano.Note_light)).GetValue(note);
        }
    }


    public Piano.Note_light GetNote(int i)
    {
        return notesArr[i];
    }

    public void SetAnswer(int i, Piano.Note_light note)
    {
        this.ans[i] = note;
    }

    public int GetResult()
    {
        int result = 0;
        for (int i=0; i<N; i++)
        {
            if (notesArr[i] == ans[i]) result++;
        }
        return result;
    }
}

public class Level_2_logic : MonoBehaviour
{
    [SerializeField]
    public Button C_btn;
    [SerializeField]
    public Button D_btn;
    [SerializeField]
    public Button E_btn;
    [SerializeField]
    public Button F_btn;
    [SerializeField]
    public Button G_btn;
    [SerializeField]
    public Button A_btn;
    [SerializeField]
    public Button B_btn;
    Task_2 task;
    [SerializeField]
    public Image right;
    [SerializeField]
    public Image Fail;
    [SerializeField]
    public AudioSource source;
    [SerializeField]
    public GameObject KB;
    [SerializeField]
    public Image Note1;
    [SerializeField]
    public Image Note2;
    [SerializeField]
    public Image Note3;
    [SerializeField]
    public Image Note4;
    [SerializeField]
    public Image Note5;
    [SerializeField]
    public Image Note6;
    [SerializeField]
    public Image Note7;
    [SerializeField]
    public Image Note8;
    [SerializeField]
    public Image Note9;
    [SerializeField]
    public Image Note10;
    [SerializeField]
    public Image Note11;
    [SerializeField]
    public Image Note12;
    [SerializeField]
    public Image Note13;
    [SerializeField]
    public Image Note14;
    [SerializeField]
    public Image Note15;
    [SerializeField]
    public Canvas result;
    [SerializeField]
    public Image result_img;
    [SerializeField]
    public Button next_lvl;
    int completed_task = 0;
    (float, Piano.Note_light)[] btns_posX;
    Piano pianoObg;
    int startTime;
    bool pressed, started;

    public void C_click(string sender)
    {
        Piano.Note_light note;
        switch (sender)
        {
            case "C": note = Piano.Note_light.C; break;
            case "D": note = Piano.Note_light.D;  break;
            case "E": note = Piano.Note_light.E; break;
            case "F": note = Piano.Note_light.F; break;
            case "G": note = Piano.Note_light.G; break;
            case "A": note = Piano.Note_light.A; break;
            case "B": note = Piano.Note_light.B; break;
            default: note = Piano.Note_light.C; break;
        }
        task.SetAnswer(completed_task, note);
        if (note != task.GetNote(completed_task))
        {
            Fail.enabled = true;
            for (int i = 0; i < 7; i++)
            {
                if (note == btns_posX[i].Item2)
                {
                    Fail.transform.localPosition = new Vector3(btns_posX[i].Item1, right.transform.localPosition.y);
                    break;

                }
            }
        }
        for (int i = 0; i < 7; i++)
        {
            if (task.GetNote(completed_task) == btns_posX[i].Item2)
            {
                right.transform.localPosition = new Vector3(btns_posX[i].Item1, right.transform.localPosition.y);
                right.enabled = true;
                break;
            }
        }
        startTime = System.Environment.TickCount;
        completed_task++;
        pressed = true;
    }

    void UpdateInterface()
    {
        Fail.enabled = false;
        right.enabled = false;
        //pressed = false;
    }

    void PlayNextNote()
    {
        pianoObg.PlayNote(source, task.GetNote(completed_task));
        pressed = false;
    }

    void StartGame()
    {
        KB.SetActive(true);
        pianoObg.PlayNote(source, task.GetNote(completed_task));
        started = true; ;
    }

    public void On_Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    public void On_Next_Lvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        task = new Task_2();
        pianoObg = new Piano();
       // completed_task = 0;
        right.enabled = false;
        Fail.enabled = false;
        next_lvl.enabled = false;
        pressed = false;
        result.enabled = false;
        KB.SetActive(false);
        btns_posX = new (float, Piano.Note_light)[7];
        btns_posX[0] = (C_btn.transform.localPosition.x, Piano.Note_light.C);
        btns_posX[1] = (D_btn.transform.localPosition.x, Piano.Note_light.D);
        btns_posX[2] = (E_btn.transform.localPosition.x, Piano.Note_light.E);
        btns_posX[3] = (F_btn.transform.localPosition.x,Piano.Note_light.F);
        btns_posX[4] = (G_btn.transform.localPosition.x,Piano.Note_light.G);
        btns_posX[5] = (A_btn.transform.localPosition.x, Piano.Note_light.A);
        btns_posX[6] = (B_btn.transform.localPosition.x, Piano.Note_light.B);
        started = false;
        startTime = System.Environment.TickCount;
    }

    void level_end()
    {
        int res = task.GetResult();
        if(res>7)
        {
            result_img.sprite = Resources.Load<Sprite>("Ok");
        }
        else
        {
            Destroy(this.next_lvl);
        }
        result.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (System.Environment.TickCount - startTime > 360 && !started) StartGame();
        if (System.Environment.TickCount - startTime > 360 && pressed) UpdateInterface();
        if (System.Environment.TickCount - startTime > 490 && pressed) PlayNextNote();
        if (completed_task == 14)
            level_end();
        switch (completed_task)
        {
            case 1: Note1.sprite = Resources.Load<Sprite>("note"); break;
            case 2: Note2.sprite = Resources.Load<Sprite>("note"); break;
            case 3: Note3.sprite = Resources.Load<Sprite>("note"); break;
            case 4: Note4.sprite = Resources.Load<Sprite>("note"); break;
            case 5: Note5.sprite = Resources.Load<Sprite>("note"); break;
            case 6: Note6.sprite = Resources.Load<Sprite>("note"); break;
            case 7: Note7.sprite = Resources.Load<Sprite>("note"); break;
            case 8: Note8.sprite = Resources.Load<Sprite>("note"); break;
            case 9: Note9.sprite = Resources.Load<Sprite>("note"); break;
            case 10: Note10.sprite = Resources.Load<Sprite>("note"); break;
            case 11: Note11.sprite = Resources.Load<Sprite>("note"); break;
            case 12: Note12.sprite = Resources.Load<Sprite>("note"); break;
            case 13: Note13.sprite = Resources.Load<Sprite>("note"); break;
            case 14: Note14.sprite = Resources.Load<Sprite>("note"); break;
            case 15: Note15.sprite = Resources.Load<Sprite>("note"); break;
            }
    }
}
