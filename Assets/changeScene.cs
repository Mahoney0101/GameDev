using UnityEngine;
using UnityEngine.SceneManagement;
     
public class changeScene : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void quitGame(){
        Application.Quit();
    }
}
