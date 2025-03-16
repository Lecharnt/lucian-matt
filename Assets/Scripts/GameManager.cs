using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public VariableManager variableManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject); // Destroy this instance if another one exists
            return;
        }

        // Set the instance to this
        instance = this;

        // Prevent this GameObject from being destroyed when switching scenes
        DontDestroyOnLoad(this.gameObject);
    }

}
