using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadProgressBar : MonoBehaviour
{
    private float currentProgress = 0;
    public float dampening = 5f;
    public float changeSpeed = .5f;
    private float timeToReload;
    float timeout = 0f;
    float activationpoint;
    Material mat;
    float fillTarget = 0f;
    float delta = 0f;

    public float TimeToReload { get => timeToReload; set => timeToReload = value; }

    public void displayReloadBar()
    {
        currentProgress = (Time.fixedTime - activationpoint) / timeToReload;
    }

    void Awake()
    {
        activationpoint = Time.time;
        Renderer rend = gameObject.GetComponent<Renderer>();
        Image img = gameObject.GetComponent<Image>();
        if (rend != null)
        {
            mat = new Material(rend.material);
            rend.material = mat;
        }
        else if (img != null)
        {
            mat = new Material(img.material);
            img.material = mat;
        }
        else
        {
            Debug.LogWarning("No Renderer or Image attached to " + name);
        }


    }

    void Update()
    {
        displayReloadBar();
        //  timeout += Time.deltaTime * changeSpeed;
        //  if (timeout > 1.0f)
        //  {
        //      timeout = 0f;

        // Choose new fill value 
        float newFill = currentProgress;
        // Modify delta by how much fillTarget will change
        delta -= fillTarget - newFill;
        fillTarget = newFill;

        //  }

        // The main idea of animating the bar this way is 
        // 1. Set "_Fill" to whatever value the bar actually has [0, 1]
        // 2. Gradually bring "_Delta" to zero

        // For a slightly different effect, 
        // 1. Keep "_Delta" at zero 
        // 2. Lerp "_Fill" to the target value [0, 1]

        // Also: See the included shader for more information about other properties.

        delta = Mathf.Lerp(delta, 0, Time.deltaTime * dampening);

        mat.SetFloat("_Delta", delta);
        mat.SetFloat("_Fill", fillTarget);
    }

}
