using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float countdown = 10;
    public TextMeshProUGUI countText;
    private bool active = true;

    void Update()
    {
        if (active)
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0)
            {
                PlayerController playerController = transform.parent.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.DestroyPlayer();
                }

                active = false;
            }

            countText.text = Mathf.Ceil(countdown).ToString();
        }
    }
}
