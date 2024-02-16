using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public void press()
    {
        SceneManager.LoadScene(0);
    }
}
