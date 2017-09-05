using UnityEngine;

public class DontDestroy : MonoBehaviour {

	void Start ()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }

}
