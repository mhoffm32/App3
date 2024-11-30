using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string level;
    public string leaderboard;
    public string createPlayer;

    public AudioSource audioSource;
    public AudioClip clickSound;

    private void PlaySound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    public void StartGame()
    {
        StartCoroutine(PlaySoundAndSwitchScene(createPlayer));
    }

    public void Leaderboard()
    {
        StartCoroutine(PlaySoundAndSwitchScene(leaderboard));
    }

    public void QuitGame()
    {
        StartCoroutine(PlaySoundAndQuit());
    }

    private IEnumerator PlaySoundAndSwitchScene(string sceneName)
    {
        PlaySound();
        yield return new WaitForSeconds(clickSound.length); 
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator PlaySoundAndQuit()
    {
        PlaySound();
        yield return new WaitForSeconds(clickSound.length);
        Application.Quit();
    }
}
