using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{

    public List<GameObject> targets;
    private int currentTarget = 0;
    private UIHandler ui;

    public GameObject CurrentTarget { get { return targets[currentTarget]; } }

    public List<Renderer> currentRenderers = new List<Renderer>();
    public List<Renderer> shaderBallsRenderers = new List<Renderer>();

    public Texture2D[] gradientTextures;
    private int currentGradTex = 0;

    void Start()
    {
        targets.ForEach(x => x.SetActive(false));
        targets[currentTarget].SetActive(true);
        ui = FindObjectOfType<UIHandler>();
        ui.SetMaterialBallList(targets[currentTarget].GetComponent<ModelBehaviour>().materials);
        currentRenderers.Clear();
        currentRenderers = CurrentTarget.GetComponentsInChildren<Renderer>().ToList();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.LeftArrow))
	    {
	        targets[currentTarget].SetActive(false);
	        currentTarget--;
	        if (currentTarget < 0)
	            currentTarget = targets.Count - 1;
	        targets[currentTarget].SetActive(true);
            ui.SetMaterialBallList(targets[currentTarget].GetComponent<ModelBehaviour>().materials);
	        currentRenderers.Clear();
	        currentRenderers = CurrentTarget.GetComponentsInChildren<Renderer>().ToList();
	        ApplyTex();

	    }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
	    {
	        targets[currentTarget].SetActive(false);
	        currentTarget++;
	        if (currentTarget >= targets.Count)
	            currentTarget = 0;
	        targets[currentTarget].SetActive(true);
	        ui.SetMaterialBallList(targets[currentTarget].GetComponent<ModelBehaviour>().materials);

	        currentRenderers.Clear();
	        currentRenderers = CurrentTarget.GetComponentsInChildren<Renderer>().ToList();
	        ApplyTex();
        }
	    else if (Input.GetKeyDown(KeyCode.UpArrow))
	    {
	        currentGradTex++;
	        if (currentGradTex >= gradientTextures.Length)
	            currentGradTex--;
	        ApplyTex();
        }
	    else if (Input.GetKeyDown(KeyCode.DownArrow))
	    {
	        currentGradTex--;
	        if (currentGradTex < 0)
	            currentGradTex = 0;
	        ApplyTex();

	    }
    }

    void ApplyTex()
    {
        currentRenderers.ForEach(x => x.sharedMaterial.SetTexture("_HalfTone_Texture", gradientTextures[currentGradTex]));
        shaderBallsRenderers.ForEach(x => x.sharedMaterial.SetTexture("_HalfTone_Texture", gradientTextures[currentGradTex]));
    }
}
