using System.Linq;
using UnityEngine;

public class GlobalMouseHandler : MonoBehaviour
{
    private MouseUIChecker[] uiCheckers;

    private static GlobalMouseHandler instance;

    public static GlobalMouseHandler Instance
    {
        get { return instance; }
    }

    private bool mouseOverUI;


    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start ()
	{
	    uiCheckers = FindObjectsOfType<MouseUIChecker>().ToArray();
	}

    void Update()
    {
        mouseOverUI = uiCheckers.Any(x => x.isOver);
    }

    public bool IsOverUI()
    {
        return mouseOverUI;
    }
}
