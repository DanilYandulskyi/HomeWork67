using UnityEngine;

public class Resource : MonoBehaviour
{
    public void StartFollow(Transform transform)
    {
        this.transform.parent = transform;
    }

    public void StopFollowing()
    {
        transform.parent = null;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}