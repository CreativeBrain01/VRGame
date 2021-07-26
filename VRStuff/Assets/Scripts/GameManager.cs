using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance { get => instance; }

    private GameManager instance;

    Vector3 startPosition;

    public TMP_Text timerText;

    [Min(0)] public float maxTimeSeconds;

    private float timer;

    public int minutes { get => (int)(timer / 60); }
    public int seconds { get => (int)(timer % 60); }

    void Start()
    {
        instance = this;
        startPosition = gameObject.transform.position;
        timer = maxTimeSeconds;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = minutes + " : " + string.Format("{0:D2}", seconds);
    }
}
