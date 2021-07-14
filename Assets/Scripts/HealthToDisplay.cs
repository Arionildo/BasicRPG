using UnityEngine;
using UnityEngine.UI;

public class HealthToDisplay : MonoBehaviour
{
    public Slider slider;
    public Status status;

    // Start is called before the first frame update
    void Start()
    {
        TrySetSlider();
        TrySetStatus();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBar();
        LookAtMainCamera();
    }

    private void LookAtMainCamera()
    {
        Vector3 worldPosition = slider.transform.position + Camera.main.transform.rotation * Vector3.back;
        Vector3 worldUp = Camera.main.transform.rotation * Vector3.down;

        slider.transform.LookAt(worldPosition, worldUp);
    }

    private void UpdateBar()
    {
        slider.value = status.currentHealth / status.maxHealth;
    }

    private void TrySetStatus()
    {
        if (status == null)
        {
            status = GetComponent<Status>();
            if (status == null)
            {
                throw new MissingComponentException("Please add a Status component to this HealthToDisplay");
            }
        }
    }

    private void TrySetSlider()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
            if (slider == null)
            {
                throw new MissingComponentException("Please add a Slider component to this HealthToDisplay");
            }
        }
    }
}
