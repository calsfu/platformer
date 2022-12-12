using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _playerScore;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _clockText;
    // Start is called before the first frame update
    void Start()
    {
        _playerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _clockText.text = DateTime.Now.ToString();
        UpdateScore();
    }

    public void UpdateScore()
    {
        _playerScore += 0.004f;
        int score = (int)_playerScore;
        _scoreText.text = "Time: " + score.ToString();
    }
}
