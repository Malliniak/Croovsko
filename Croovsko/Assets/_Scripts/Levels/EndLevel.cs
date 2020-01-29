using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IInputResponder>() != null) SceneManager.LoadScene(0);
    }
}