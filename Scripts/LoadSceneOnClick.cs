using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.UI;
/* 
 * v0.0.1-r05
 * Written by Veritas83
 * www.NigelTodman.com
 * /Scripts/LoadSceneonClick.cs
 */
public class LoadSceneOnClick : MonoBehaviour
{
    public void LoadByIndex(int sceneIndex)
    {
        DestroyImmediate(GameObject.Find("GameOverPanel(Clone)"));
        SceneManager.LoadScene(sceneIndex);
    }
}