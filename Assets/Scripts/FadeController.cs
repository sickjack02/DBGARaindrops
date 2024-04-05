using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public Animator animator;

    [SerializeField] private float fadeInSeconds = 3f;
    [SerializeField] private float fadeOutSeconds = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeInCall()
    {
        StartCoroutine(fadeIn());
    }

    IEnumerator fadeIn()
    {
        animator.SetTrigger("fade in");
        yield return new WaitForSeconds(fadeInSeconds);
    }

}
