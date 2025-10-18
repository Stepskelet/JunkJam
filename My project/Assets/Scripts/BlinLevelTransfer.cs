using UnityEngine;
using UnityEngine.SceneManagement;

public class BlinLevelTransfer : MonoBehaviour
{
    private int sceneIndex;
    [SerializeField] private MeshCollider blinCollider;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private HintManager hint;
    [SerializeField] private BlinCounterScript blinCounter;

    private float interactDistance = 3f;
    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

    }
    private void Update()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = playerCamera.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            if (hit.collider == blinCollider)
            {
                hint.ShowHint("E");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    GoToScene(sceneIndex);
                }
            }
            else
            {
                hint.HideHint();
            }
        }
        else
        {
            hint.HideHint();
        }
    }

    private void GoToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex+1);
        blinCounter.UpdateCounter(1);
    }
}
