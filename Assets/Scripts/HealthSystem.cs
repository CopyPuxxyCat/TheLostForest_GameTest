using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public static int health = 3;
    [SerializeField] public GameObject live3;
    [SerializeField] public GameObject live2;
    [SerializeField] public GameObject live1;
    [SerializeField] public GameObject live0;
    [SerializeField] public GameObject pauseScene;
    public void Update() 
    {
        switch (health)
        {
            case 3:
                live3.SetActive(true);
                live2.SetActive(false);
                live1.SetActive(false);
                live0.SetActive(false);
            break;
            case 2:
                live3.SetActive(false);
                live2.SetActive(true);
                live1.SetActive(false);
                live0.SetActive(false);
            break;
            case 1:
                live3.SetActive(false);
                live2.SetActive(false);
                live1.SetActive(true);
                live0.SetActive(false);
            break;
            case 0:
                live3.SetActive(false);
                live2.SetActive(false);
                live1.SetActive(false);
                live0.SetActive(true);
                StartCoroutine(DeathPanel());
                StartCoroutine(DeathTime());
            break;
        }
    }
    IEnumerator DeathTime(){
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
    IEnumerator DeathPanel(){
        yield return new WaitForSeconds(0.9f);
        pauseScene.SetActive(true);
    }

}
