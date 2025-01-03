using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuList : MonoBehaviour
{
    public GameObject menuList; 
    public GameObject optionsList; 
    public MonoBehaviour playerController; 
    [SerializeField] private bool menuKeys = true;
    [SerializeField] private AudioSource bgmSound; 

    void Update()
    {
        if (menuKeys)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        menuList.SetActive(true);
        menuKeys = false;
        Time.timeScale = 0; 
        bgmSound.Pause(); 
        Cursor.lockState = CursorLockMode.None; 
        Cursor.visible = true; 

        if (playerController != null)
        {
            playerController.enabled = false; 
        }
    }

    public void ResumeGame()
    {
        menuList.SetActive(false);
        menuKeys = true;
        Time.timeScale = 1; 
        bgmSound.Play(); 
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;      

        if (playerController != null)
        {
            playerController.enabled = true; 
        }
    }

    public void ReturnToGame() // 返回游戏
    {
        ResumeGame();
    }

    public void RestartGame() // 重新开始
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void StartGame() // 开始游戏
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void QuitGame() // 退出游戏
    {
        Application.Quit();
    }

    public void OpenOptionsMenu() 
    {
        menuList.SetActive(false); 
        optionsList.SetActive(true); 
    }

    public void ReturnToPauseMenu() 
    {
        optionsList.SetActive(false); 
        menuList.SetActive(true); 
    }
}
