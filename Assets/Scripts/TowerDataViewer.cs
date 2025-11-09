using UnityEngine;

public class TowerDataViewer : MonoBehaviour
{
    private void Awake()
    {
        OffPanel();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel()
    {
        gameObject.SetActive(true);
    }
    public void OffPanel()
    {
        gameObject.SetActive(false);
    }
}
