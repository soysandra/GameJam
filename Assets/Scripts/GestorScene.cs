using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorScene : MonoBehaviour
{
    public void cargarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}

