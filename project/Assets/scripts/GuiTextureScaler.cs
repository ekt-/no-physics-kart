using UnityEngine;

//http://wiki.unity3d.com/index.php/GUIScaler
[RequireComponent(typeof(GUITexture))]
class GuiTextureScaler : MonoBehaviour
{    
    static private float m_size = 0;
    private const int MinAtScreenHeight = 384; // at what screen height should the texture be it's preset size (as setup in the inspector)?    
    private const int MaxAtScreenHeight = 768; // at what screen height should the texture be fully scaled by maxFactor? 
    private const int MaxFactor = 2;

    void Awake () 
    {
	    var mySize = GetSize();
	    var gui = GetComponent<GUITexture>();
        var rect = gui.pixelInset;
        rect.x *= mySize;
        rect.y *= mySize;
        rect.width *= mySize;
        rect.height *= mySize;
        gui.pixelInset = rect;
    }

    float GetSize()
    {
        if (m_size == 0)
        {
            var factor = Mathf.InverseLerp(MinAtScreenHeight, MaxAtScreenHeight, Screen.height);
            m_size = Mathf.Lerp(1, MaxFactor, factor);
        }
        return m_size;
    }
}


 
