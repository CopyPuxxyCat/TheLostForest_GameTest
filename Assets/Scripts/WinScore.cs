using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _starText;
    [SerializeField] private TextMeshProUGUI _killText;
    [SerializeField] private TextMeshProUGUI _deathText;
    [SerializeField] private TextMeshProUGUI _totalText;
    private AudioSource audioSource;
    private Score score;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        score = FindObjectOfType<Score>();
        audioSource.Play();
        _starText.text = score.score.ToString("000");
        _killText.text = Enemy.killcounter.ToString("000");
        _deathText.text = Death.deathcounter.ToString("000");
        _totalText.text = (score.score*10 + Enemy.killcounter*15 - Death.deathcounter*10).ToString();
    }
}
