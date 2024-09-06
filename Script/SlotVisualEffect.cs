using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotVisualEffect : MonoBehaviour
{
    [SerializeField] Material material;
    
    public void SetHighlight() {
        GetComponent<RawImage>().material = material;
    }
}
