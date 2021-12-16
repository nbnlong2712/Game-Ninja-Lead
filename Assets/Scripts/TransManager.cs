using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransManager : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(hidePanel());
    }

    IEnumerator hidePanel()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

    public void LoadScene(string name)
    {
        gameObject.SetActive(true);
        animator.SetTrigger("end");
        StartCoroutine(Load(name));
        SceneManager.LoadScene(name);
    }
    IEnumerator Load(string name)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(name);
    }
}
