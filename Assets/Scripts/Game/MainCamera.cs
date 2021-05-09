using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    private Vector3 velocity = Vector3.zero;
    private bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.onPlayerDeath.AddListener(() => Lock());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (locked)
            return;

        transform.position = Vector3.Lerp(transform.position, target.position, 0.125f);
        transform.position += offset;
    }

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }
}
