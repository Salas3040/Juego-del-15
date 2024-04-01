using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonController : MonoBehaviour
{
    //funciones de todos los botones
    public void jugar()
    {
        SceneManager.LoadScene("juego");
    }

    public void salir()
    {
        Application.Quit();
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void menu()
    {
        SceneManager.LoadScene("inicio");
    }
}
