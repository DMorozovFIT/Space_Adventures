using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Piano
{
    public enum Note
    {
        C,
        D,
        E,
        F,
        G,
        A,
        B,
        CSharp,
        DSharp,
        FSharp,
        GSharp,
        ASharp,
        C2
    }

    public enum Note_light
    {
        C,
        D,
        E,
        F,
        G,
        A,
        B
    }

    private readonly Dictionary<Note, AudioClip> notes = new Dictionary<Note, AudioClip>();
    private readonly Dictionary<Note_light, AudioClip> notes2 = new Dictionary<Note_light, AudioClip>();

    public Piano()
    {
        foreach (var note in (Note[])Enum.GetValues(typeof(Note)))
        {
            AudioClip player;
            player = Resources.Load<AudioClip>(note.ToString());
            notes[note] = player;
        }

        foreach (var note in (Note_light[])Enum.GetValues(typeof(Note)))
        {
            AudioClip player;
            player = Resources.Load<AudioClip>(note.ToString());
            notes2[note] = player;
        }
    }

    public void PlayNote(AudioSource audioSource,Note note)
    {
        audioSource.PlayOneShot(notes[note]);
    }

    public void PlayNote(AudioSource audioSource, Note_light note)
    {
        audioSource.PlayOneShot(notes2[note]);
    }


    private void PlayNoteWithDuration(Note note, TimeSpan duration)
    {
        //var player = notes[note];
        //player.Play();
        //Thread.Sleep(duration);
    }
}


public class Task
{
    int[] rightAns;
    int[] ans;
    Piano.Note[,] notesArr;

    public  Task()
    {
        rightAns = new int[5];
        ans = new int[5];
        notesArr = new Piano.Note[5, 2];
        this.InitNotes();
    }

    private void InitNotes()
    {
        int first_index=0, second_index=0;
        System.Random rand = new System.Random();
        for(int i=0; i<5; i++)
        {
            while (first_index == second_index)
            {
                first_index = rand.Next(0, 12);
                second_index = rand.Next(0, 12);
            }
            this.rightAns[i] = (first_index > second_index) ?  1 : 2;
            this.notesArr[i,0] = (Piano.Note)Enum.GetValues(typeof (Piano.Note)).GetValue(first_index);
            this.notesArr[i, 1] = (Piano.Note)Enum.GetValues(typeof(Piano.Note)).GetValue(second_index);
            first_index = second_index;
        }
    }

    public (Piano.Note, Piano.Note) GetNotes(int i)
    {
        return (this.notesArr[i, 0], this.notesArr[i, 1]);
    }

    public int getRightAnswer(int i)
    {
        return rightAns[i];
    }

    public void setAnswer(int count, int answer)
    {
        this.ans[count] = answer;
    }

    public int getResult()
    {
        int rightAnsCount = 0;
        for (int i = 0; i < 5; i++)
            if (ans[i] == rightAns[i]) rightAnsCount++;
        return rightAnsCount;
    }
}



public class Level_1_logic : MonoBehaviour
{
    [SerializeField]
    public AudioSource _musicAudioSource = null;
    static Piano pianoObj = null;
    public Task task = null;
    int completedTasks = 0;
    [SerializeField]
    public Button next_btn;
    [SerializeField]
    public GameObject control_panel;
    [SerializeField]
    public Image note_1;
    [SerializeField]
    public Image note_2;
    [SerializeField]
    public Image note_3;
    [SerializeField]
    public Image note_4;
    [SerializeField]
    public Image note_5;
    [SerializeField]
    public Image Result;
    [SerializeField]
    public Canvas main_canvas;
    [SerializeField]
    public Button next_btn_main;
    [SerializeField]
    public Button prev_btn_main;
    [SerializeField]
    public Button next_lvl_btn;
    private bool pressed;
    bool started;
    bool played1, played2;
    int startTime;
    public void next_level()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void on_menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + - 2);
    }

    public void On_first_click()
    {
        task.setAnswer(completedTasks, 1);
        completedTasks++;
        pressed = true;
        played1 = false;
        played2 = false;
        startTime = System.Environment.TickCount;
        if (task.getRightAnswer(completedTasks) == 1)//все ок
        {
            UpdateInterfaseNotes(true);
        }
        else //ошибка
        {
            UpdateInterfaseNotes(false);
        }
        
        next_btn_main.enabled = false;
        prev_btn_main.enabled = false;
        //pianoObj.PlayNote(_musicAudioSource, Piano.Note.D);
    }

    void UpdateInterfaseNotes(bool isRight)
    {
        if(isRight)
        switch (completedTasks)
        {
            case 1: note_1.sprite = Resources.Load<Sprite>("note_ok"); break;
            case 2: note_2.sprite = Resources.Load<Sprite>("note_ok"); break;
            case 3: note_3.sprite = Resources.Load<Sprite>("note_ok"); break;
            case 4: note_4.sprite = Resources.Load<Sprite>("note_ok"); break;
            case 5: note_5.sprite = Resources.Load<Sprite>("note_ok"); break;
        }
        else
            switch (completedTasks)
            {
                case 1: note_1.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 2: note_2.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 3: note_3.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 4: note_4.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 5: note_5.sprite = Resources.Load<Sprite>("note_fail"); break;
            }
    }

    public void On_second_click()
    {
        task.setAnswer(completedTasks, 2);
        pressed = true;
        played1 = false;
        played2 = false;
        startTime = System.Environment.TickCount;
        completedTasks++;
        if (task.getRightAnswer(completedTasks) == 2)//все ок
        {
            UpdateInterfaseNotes(true);
        }
        else //ошибка
        {
            UpdateInterfaseNotes(false);
        }

        next_btn_main.enabled = false;
        prev_btn_main.enabled = false;
    }



    // Start is called before the first frame update
    void Start()
    {
        task = new Task();//создаем задания
        pianoObj = new Piano();//инициализируем пианино
        pressed = false;
        started = false;
        played1 = false;
        played2 = false;
        startTime = System.Environment.TickCount;
    }

    private void level_end()
    {
        main_canvas.enabled = false;
        int result = task.getResult();
        if (result >= 3)
        {
            Result.sprite = Resources.Load<Sprite>("Ok");
            
        }
        else
        {
            Result.sprite = Resources.Load<Sprite>("Fail");
            next_lvl_btn.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (completedTasks == 5)
            level_end();
        if(System.Environment.TickCount - startTime > 60 && !played1 && !started)
        {
            played1 = true;
            startTime = System.Environment.TickCount;
            pianoObj.PlayNote(_musicAudioSource, task.GetNotes(completedTasks).Item1);

        }
        else
        if (System.Environment.TickCount - startTime > 800 && !played2 && !started && played1)
        {
            played2 = true;
            started = true;
            pianoObj.PlayNote(_musicAudioSource, task.GetNotes(completedTasks).Item2);
        }
        if (System.Environment.TickCount - startTime > 60 && pressed && started && !played1)
        {
            played1 = true;
            startTime = System.Environment.TickCount;
            pianoObj.PlayNote(_musicAudioSource, task.GetNotes(completedTasks).Item1);

        }
        else
        if (System.Environment.TickCount - startTime > 800 && !played2 && started && played1 && pressed)
        {
            played2 = true;
            pressed = false;
            pianoObj.PlayNote(_musicAudioSource, task.GetNotes(completedTasks).Item2);
            next_btn_main.enabled = true;
            prev_btn_main.enabled = true;
        }


    }
}
