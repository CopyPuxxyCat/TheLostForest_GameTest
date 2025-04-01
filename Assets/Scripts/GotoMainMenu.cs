using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoMainMenu : MonoBehaviour
{
    private Score score;

    private void Start()
    {
        score = FindObjectOfType<Score>();
    }

    public void GoMainMenu()
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel(){
        yield return new WaitForSeconds(1.5f);
        Score.carrot = 0;
        HealthSystem.health = 3;
        score.score = 0;
        Enemy.killcounter = 0;
        Death.deathcounter = 0;
        SceneManager.LoadScene(0);
    }
}
