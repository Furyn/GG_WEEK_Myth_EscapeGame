using UnityEngine;
using UnityEngine.UI;

public class GoToCredits : MonoBehaviour
{
    //[HideInInspector]
    public bool goToCredits = false;

    public string nameSceneCredits = "Crédits";
    public GameObject panelTransition;

    private Image imgTransition;

    [SerializeField]
    private float smoothRalenti = 0.001f;
    [SerializeField]
    private float smoothNoir = 0.001f;

    private void Start()
    {
        imgTransition = panelTransition.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goToCredits)
        {
            panelTransition.SetActive(true);
            if (Time.timeScale > 0)
            {
                if (Time.timeScale - smoothRalenti < 0)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale -= smoothRalenti;
                }
            }
            else
            {
                imgTransition.color = new Color(imgTransition.color.r, imgTransition.color.g, imgTransition.color.b, imgTransition.color.a + smoothNoir);

                if(imgTransition.color.a >= 1)
                {

                    GameManager.Instance.LoadScene(nameSceneCredits);
                }
            }
        }
    }
}
