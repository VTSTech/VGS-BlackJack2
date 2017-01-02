using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * v0.0.1-r05
 * Written by Veritas83
 * www.NigelTodman.com
 * /Scripts/QuitOnClick.cs
 */

public class QuitOnClick : MonoBehaviour
{

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}