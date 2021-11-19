using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    public GameObject environmentSettings, shaderSettings;
    private Light sunLight;
    public ColorPicker picker;
    public GameObject colourPickerObject;
    private UnityAction<Color> currentColorChangeAction;
    public Image sunColorImage;
    public Transform shaderButtonList;
    public GameObject ShaderBallButtonPrefab;

    public List<GameObject> shaderProperties = new List<GameObject>();
    public List<Material> materials = new List<Material>();

    private ObjectHandler objectHandler;

    public Text materialText;

    public Image baseColorImage;
    public GameObject baseColorField;
    public Image stepColor1Image;
    public GameObject stepColor1Field;
    public Image stepColor2Image;
    public GameObject stepColor2Field;
    public Image edgeColorImage;
    public GameObject edgeColorField;
    public GameObject step1FloatField;
    public GameObject step2Field;
    public GameObject edgeStepField;
    public GameObject edgeWidthField;
    public GameObject ceilsField;
    public GameObject halfTonePowerField;
    public GameObject uvScaleField;
    
    public Toggle envPropertyToggle, shaderPropertyToggle;

    public GameObject panelsProps, panelsShaders;

    void Awake()
    {
        sunLight = FindObjectOfType<Light>();
        objectHandler = FindObjectOfType<ObjectHandler>();
        panelsProps.SetActive(false);
        panelsShaders.SetActive(false);
    }

    void Start()
    {
        SetShaderSettingsDims();
        StartCoroutine(ShowPanels());
    }

    IEnumerator ShowPanels()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        panelsProps.SetActive(true);
        panelsShaders.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            panelsProps.SetActive(!panelsProps.activeInHierarchy);
            panelsShaders.SetActive(!panelsShaders.activeInHierarchy);
        }
    }
    
    public void ToggleEnvSettings(bool value)
    {
        environmentSettings.SetActive(value);
    }

    public void ToggleShaderSettings(bool value)
    {
        shaderSettings.SetActive(value);
        if(value)
            SetShaderSettingsDims();
    }

    void SetShaderSettingsDims()
    {
        //var sd = shaderSettings.GetComponent<RectTransform>().sizeDelta;
        //sd.y = shaderProperties.Count(x => x.gameObject.activeInHierarchy) * 67.62f;
        //shaderSettings.GetComponent<RectTransform>().sizeDelta = sd;
    }
    
    public void SetSunColor()
    {
        colourPickerObject.SetActive(true);
        if(currentColorChangeAction != null)
            picker.onValueChanged.RemoveListener(currentColorChangeAction);;
        currentColorChangeAction = ChangeSunColor;
        picker.CurrentColor = sunLight.color;
        picker.onValueChanged.AddListener(currentColorChangeAction);
    }

    public void CloseColorPicker()
    {
        picker.onValueChanged.RemoveListener(currentColorChangeAction);
        colourPickerObject.SetActive(false);
    }

    private void ChangeSunColor(Color col)
    {
        sunLight.color = col;
        sunColorImage.color = col;
    }

    public void SetSunIntensity(float value)
    {
        sunLight.intensity = value;
    }

    public void SetMaterialBallList(List<Material> mats)
    {
        var children = new List<GameObject>();
        foreach (Transform child in shaderButtonList) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        if(objectHandler.shaderBallsRenderers != null)
            objectHandler.shaderBallsRenderers.Clear();
        for (int i = 0; i < mats.Count; ++i)
        {
            var go = Instantiate(ShaderBallButtonPrefab, shaderButtonList);
            go.GetComponentInChildren<MeshFilter>().GetComponent<Renderer>().sharedMaterial = mats[i];
            int idx = i;
            go.GetComponent<Button>().onClick.AddListener(() =>
                                                          {
                                                              objectHandler.CurrentTarget.GetComponent<ModelBehaviour>()
                                                                           .SetMaterial(idx);
                                                          });
            objectHandler.shaderBallsRenderers.Add(go.GetComponentInChildren<MeshRenderer>());
        }

        objectHandler.CurrentTarget.GetComponent<ModelBehaviour>().SetMaterial(0);

    }

    public void SetMaterial(int idx)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial = materials[idx]);
    }

    public void SetUVScale(float value)
    {
        objectHandler.currentRenderers.ForEach(x =>x.sharedMaterial.SetFloat("_Half_Tone_UVs", value));
    }

    public void SetUVPower(float value)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial.SetFloat("_Half_Tone_Power", value));
    }

    public void SetBaseColor()
    {
        colourPickerObject.SetActive(true);
        if (currentColorChangeAction != null)
            picker.onValueChanged.RemoveListener(currentColorChangeAction); ;
        currentColorChangeAction = SetBaseColor;
        picker.CurrentColor = objectHandler.currentRenderers[0].sharedMaterial.GetColor("_Color_Base");
        picker.onValueChanged.AddListener(currentColorChangeAction);
    }

    public void SetBaseColor(Color col)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial.SetColor("_Color_Base", col));
        baseColorImage.color = col;
    }

    public void SetColor1()
    {
        colourPickerObject.SetActive(true);
        if (currentColorChangeAction != null)
            picker.onValueChanged.RemoveListener(currentColorChangeAction); ;
        currentColorChangeAction = SetColor1;
        picker.CurrentColor = objectHandler.currentRenderers[0].sharedMaterial.GetColor("_Color_Step_1");
        picker.onValueChanged.AddListener(currentColorChangeAction);
    }

    public void SetColor1(Color col)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial.SetColor("_Color_Step_1", col));
        stepColor1Image.color = col;
    }

    public void SetColor2()
    {
        colourPickerObject.SetActive(true);
        if (currentColorChangeAction != null)
            picker.onValueChanged.RemoveListener(currentColorChangeAction); ;
        currentColorChangeAction = SetColor2;
        picker.CurrentColor = objectHandler.currentRenderers[0].sharedMaterial.GetColor("_Color_Step_2");
        picker.onValueChanged.AddListener(currentColorChangeAction);
    }

    public void SetColor2(Color col)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial.SetColor("_Color_Step_2", col));
        stepColor2Image.color = col;
    }

    public void SetStep1(float value)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial.SetFloat("_Step_1", value));
    }

    public void SetStep2(float value)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial.SetFloat("_Step_2", value));
    }

    public void SetEdgeColour()
    {
        colourPickerObject.SetActive(true);
        if (currentColorChangeAction != null)
            picker.onValueChanged.RemoveListener(currentColorChangeAction); ;
        currentColorChangeAction = SetEdgeColour;
        picker.onValueChanged.AddListener(currentColorChangeAction);
    }

    public void SetEdgeColour(Color col)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial.SetColor("_Edge_Color", col));
        edgeColorImage.color = col;
    }

    public void SetEdgeStep(float value)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial.SetFloat("_Edge_Step", value));
    }

    public void SetEdgeWidth(float value)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial.SetFloat("_Edge_Width", value));
    }

    public void SetCeils(float value)
    {
        objectHandler.currentRenderers.ForEach(x => x.sharedMaterial.SetFloat("_Ceils", value));
    }

    public void HideUnavailableValues(Material mat)
    {
        step1FloatField.SetActive(mat.HasProperty("_Step_1"));
        step2Field.SetActive(mat.HasProperty("_Step_2"));
        stepColor1Field.SetActive(mat.HasProperty("_Color_Step_1"));
        stepColor2Field.SetActive(mat.HasProperty("_Color_Step_2"));
        edgeColorField.SetActive(mat.HasProperty("_Edge_Color"));
        baseColorField.SetActive(mat.HasProperty("_Color_Base"));
        ceilsField.SetActive(mat.HasProperty("_Ceils"));
        edgeStepField.SetActive(mat.HasProperty("_Edge_Step"));
        edgeWidthField.SetActive(mat.HasProperty("_Edge_Width"));
        halfTonePowerField.SetActive(mat.HasProperty("_Half_Tone_Power"));
        uvScaleField.SetActive(mat.HasProperty("_Half_Tone_UVs"));
        SetInitialValues(mat);
    }

    public void SetInitialValues(Material mat)
    {
        if (mat.HasProperty("_Step_1"))
            step1FloatField.GetComponentInChildren<Slider>().value = mat.GetFloat("_Step_1");
        if (mat.HasProperty("_Step_2"))
            step2Field.GetComponentInChildren<Slider>().value = mat.GetFloat("_Step_2");
        if (mat.HasProperty("_Color_Step_1")) 
            stepColor1Field.GetComponentInChildren<Button>().GetComponent<Image>().color = mat.GetColor("_Color_Step_1");
        if (mat.HasProperty("_Color_Step_2"))
            stepColor2Field.GetComponentInChildren<Button>().GetComponent<Image>().color = mat.GetColor("_Color_Step_2");
        if (mat.HasProperty("_Edge_Color"))
            edgeColorField.GetComponentInChildren<Button>().GetComponent<Image>().color = mat.GetColor("_Edge_Color");
        if (mat.HasProperty("_Color_Base"))
            baseColorField.GetComponentInChildren<Button>().GetComponent<Image>().color = mat.GetColor("_Color_Base");
        if (mat.HasProperty("_Ceils"))
            ceilsField.GetComponentInChildren<Slider>().value = mat.GetFloat("_Ceils");
        if (mat.HasProperty("_Edge_Width"))
            edgeWidthField.GetComponentInChildren<Slider>().value = mat.GetFloat("_Edge_Width");
        if (mat.HasProperty("_Edge_Step"))
            edgeStepField.GetComponentInChildren<Slider>().value = mat.GetFloat("_Edge_Step");
        if (mat.HasProperty("_Half_Tone_Power"))
            halfTonePowerField.GetComponentInChildren<Slider>().value = mat.GetFloat("_Half_Tone_Power");
        if (mat.HasProperty("_Half_Tone_UVs"))
            uvScaleField.GetComponentInChildren<Slider>().value = mat.GetFloat("_Half_Tone_UVs");
    }
}
