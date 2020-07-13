using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static ImpactManager shotInvoker;
    static UnityAction<int> shotListener;
    static ImpactManager cameraSwitchInvoker;
    static UnityAction<bool> cameraSwitchListener;

    static TrackSpawner reloadWeaponInvoker;
    static UnityAction reloadWeaponListener;

    public static void  AddShotInvoker(ImpactManager script) 
    {
        shotInvoker = script;
        if (shotListener != null) {
            shotInvoker.AddShotEventListener(shotListener);
        }
    }

    public static void AddShootListener(UnityAction<int> listener_to_register)
    {
        shotListener = listener_to_register;
        if (shotInvoker != null)
        {
            shotInvoker.AddShotEventListener(shotListener);
        }
    }
    public static void AddCameraSwitchInvoker(ImpactManager script)
    {
        cameraSwitchInvoker = script;
        if (cameraSwitchListener != null)
        {
            cameraSwitchInvoker.AddSwitchListener(cameraSwitchListener);
        }
    }

    public static void AddCameraSwitchtListener(UnityAction<bool> listener_to_register)
    {
        cameraSwitchListener = listener_to_register;
        if (cameraSwitchInvoker != null)
        {
            cameraSwitchInvoker.AddSwitchListener(cameraSwitchListener);
        }
    }



    public static void AddReloadWeaponInvoker(TrackSpawner script)
    {
        reloadWeaponInvoker = script;
        if (reloadWeaponListener != null)
        {
            reloadWeaponInvoker.AddResetWeaponListener(reloadWeaponListener);
        }
    }

    public static void AddReloadWeapontListener(UnityAction listener_to_register)
    {
        reloadWeaponListener = listener_to_register;
        if (reloadWeaponInvoker != null)
        {
            
            reloadWeaponInvoker.AddResetWeaponListener(reloadWeaponListener);
        }
    }
}
