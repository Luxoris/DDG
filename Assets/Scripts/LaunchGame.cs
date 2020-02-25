using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LaunchGame : MonoBehaviour
{



    // the image you want to fade, assign in inspector
    public Text img;
    private int state;

    IEnumerator FadeImage(bool fadeAway, float time)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = time; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(img.color.r, img.color.g, img.color.b, i/time);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= time; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(img.color.r, img.color.g, img.color.b, i/time);
                yield return null;
            }
        }
        state++;
    }

// Start is called before the first frame update
    void Start()
    {
        state = 0;
        img = gameObject.GetComponent<Text>();
        StartCoroutine(FadeImage(false,3f));
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 1)
        {
            StartCoroutine(FadeImage(true, 1.5f));
        }else if(state == 2)
        {
            SceneManager.LoadScene("Game");
        }
    }
}
