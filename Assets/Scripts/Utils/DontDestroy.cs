using UnityEngine;

public class DontDestroy : MonoBehaviour
{

    private void Awake()
    {
        GameObject[] obsj = GameObject.FindGameObjectsWithTag(tag);

        if (obsj.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
