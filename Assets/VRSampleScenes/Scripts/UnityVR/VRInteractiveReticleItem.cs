using System;
using UnityEngine;

namespace VRStandardAssets.Utils
{
// This class should be added to any gameobject in the scene
// that should react to input based on the user's gaze.
// It contains events that can be subscribed to by classes that
// need to know about input specifics to this gameobject.
public class VRInteractiveReticleItem : VRInteractiveItem
{
    public SelectionRadial m_SelectionRadial;
    public bool Active = true;

    // The below functions are called by the VREyeRaycaster when the appropriate input is detected.
    // They in turn call the appropriate events should they have subscribers.

    void Start()
    {
        m_SelectionRadial = SelectionRadial.Instance;
    }

    public override void Over()
    {
        if (!Active) return;
        base.Over();
        //print("Over VRInteractiveItem");
        if (m_SelectionRadial != null) m_SelectionRadial.Show();
    }


    public override void Out()
    {
        if (!Active) return;
        base.Out();
        //print("Out VRInteractiveItem");
        if (m_SelectionRadial != null) m_SelectionRadial.Hide();
    }

    public void Activate()
    {
        Active = true;
        if(m_IsOver)
            Over();
    }

    public void Deactivate()
    {
        Out();
        Active = false;
    }
}
}