using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class changeScenes : MonoBehaviour
{
    Button curBtn;
    public int sceneVal;
    void Start()
    {
        curBtn = this.gameObject.GetComponent<Button>();
        curBtn.onClick.AddListener(LoadScene);
    }
    public void LoadScene()
    {
        if (sceneVal == 0) { SceneManager.LoadScene(0); }
        if (sceneVal == 1) { SceneManager.LoadScene(1); }
        if (sceneVal == 2) { SceneManager.LoadScene(2); }
        if (sceneVal == 3) { SceneManager.LoadScene(3); }

        string nameOfButton = EventSystem.current.currentSelectedGameObject.name;

    }

}
