using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ModelBehaviour : MonoBehaviour {
    
    public List<Material> materials = new List<Material>();
    public int currentMaterial = 0;
    public List<string> materialNames = new List<string>();
    public Renderer[] renderers;
    protected UIHandler ui;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>().ToArray();
        ui = FindObjectOfType<UIHandler>();
    }

    void OnEnable()
    {
        if(renderers.Length == 0)
            renderers = GetComponentsInChildren<Renderer>().ToArray();
        renderers.ToList().ForEach(x => x.sharedMaterial = materials[currentMaterial]);
        ui.materialText.text = materialNames[currentMaterial];
    }

    public virtual void SetMaterial(int idx)
    {
        currentMaterial = idx;
        renderers.ToList().ForEach(x => x.sharedMaterial = materials[currentMaterial]);
        ui.materialText.text = materialNames[currentMaterial];
        ui.HideUnavailableValues(materials[currentMaterial]);
        ui.shaderPropertyToggle.isOn = false;
        ui.envPropertyToggle.isOn = false;
        ui.ToggleEnvSettings(false);
        ui.ToggleShaderSettings(false);
    }
}
