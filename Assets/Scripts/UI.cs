using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    public void SwitchScene()
    {
        SceneManager.LoadScene("Markerless");
    }
}
