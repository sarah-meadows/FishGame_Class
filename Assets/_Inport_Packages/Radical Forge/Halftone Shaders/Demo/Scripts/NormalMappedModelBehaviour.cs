using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NormalMappedModelBehaviour : ModelBehaviour
{

    public Texture2D[] normalMaps;

    public override void SetMaterial(int idx)
    {
        currentMaterial = idx;

        for (int i = 0; i < renderers.Length; ++i)
        {
            var mat = new Material(materials[currentMaterial]);
            if (mat.HasProperty("_Normal_Map"))
            {
                mat.SetTexture("_Normal_Map", normalMaps[i]);
            }
            renderers[i].sharedMaterial = mat;
        }
        
        ui.materialText.text = materialNames[currentMaterial];
        ui.HideUnavailableValues(materials[currentMaterial]);
    }
}
