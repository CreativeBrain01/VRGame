using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource mainTheme;
    [SerializeField] AudioSource midGameTheme;

    public static GameManager Instance { get => instance; }

    private static GameManager instance;

    public GameObject plane;

    public enum eState
    {
        off,
        start,
        on,
        end
    }

    private static eState state = eState.off;

    public static eState State { get => state; }

    static Vector3 startPosition;

    public TMP_Text[] timerTexts;

    public TMP_Text scoreText;

    [Min(0)] public float maxTimeSeconds;

    private float timer;

    public static int score { get; set; }

    public int minutes { get => (int)(timer / 60); }
    public int seconds { get => (int)(timer % 60); }

    void Start()
    {
        instance = this;
        startPosition = gameObject.transform.position;
        timer = maxTimeSeconds;

        midGameTheme.Play();
        midGameTheme.Pause();

        UpdateTimers();
    }

    void Update()
    {
        switch (state)
        {
            case eState.off:
                if (GetComponent<OVRGrabbable>().isGrabbed)
                {
                    state = eState.start;
                    mainTheme.UnPause();
                }
                break;
            case eState.start:
                timer = maxTimeSeconds;
                score = 0;
                state = eState.on;
                mainTheme.Pause();
                midGameTheme.UnPause();
                break;
            case eState.on:
                timer -= Time.deltaTime;
                UpdateTimers();
                
                scoreText.text = "Score: " + score;
                if(timer <= 0)
                {
                    state = eState.end;
                    foreach (TMP_Text timeText in timerTexts)
                    {
                        timeText.text = "GAME OVER.";
                    }
                }
                break;
            case eState.end:
                midGameTheme.Pause();
                state = eState.off;
                break;
            default:
                break;
        }
    }


    private void UpdateTimers()
    {
        foreach (TMP_Text timeText in timerTexts)
        {
            timeText.text = minutes + " : " + string.Format("{0:D2}", seconds);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject == plane)
        {
            transform.position = startPosition;
            transform.rotation = Quaternion.identity;
        }
    }
}
