using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public CameraInfoSO cameraInfo;

    private Camera camComponent;
    private Vector3 velocity = Vector3.zero;
    private bool locked = false;
    private float originalSize;
    private float size;
    private bool sizeMustBeChanged = false;

    // Start is called before the first frame update
    private void Start()
    {
        camComponent = GetComponent<Camera>();
        originalSize = camComponent.orthographicSize;
        GameEvents.onPlayerDeath.AddListener(() => Lock());
        GameEvents.onCameraAreaEnter.AddListener(SetSize);
        GameEvents.onCameraAreaExit.AddListener(ResetSize);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (locked || target == null)
            return;

        if (sizeMustBeChanged)
            camComponent.orthographicSize = Mathf.Lerp(camComponent.orthographicSize, size, 0.1f);
        else
            camComponent.orthographicSize = Mathf.Lerp(camComponent.orthographicSize, originalSize, 0.1f);

        transform.position = Vector3.Lerp(transform.position, target.position, 0.125f);
        transform.position += cameraInfo.offset;
    }

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }

    private void SetSize(float size)
    {
        this.size = size;
        sizeMustBeChanged = true;
    }

    private void ResetSize()
    {
        sizeMustBeChanged = false;
    }
}
