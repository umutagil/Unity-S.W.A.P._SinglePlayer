using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class UnityExtensions {

    public static Vector3 ScreenToWorldLength(this Camera camera, Vector3 position)
    {
        return camera.ScreenToWorldPoint(position) - camera.ScreenToWorldPoint(Vector3.zero);
    }

    // uint[] to Color32[]
    public static void toColor(UnityEngine.Color32[] mpix, uint[] pix)
    {        
        for (int i = 0; i < mpix.Length; i++)
        {
            mpix[i].r = (byte)(pix[i]);
            mpix[i].g = (byte)(pix[i] >> 8); //& 0xFF00);
            mpix[i].b = (byte)(pix[i] >> 16);  //& 0xFF0000);
            mpix[i].a = (byte)(pix[i] >> 24);  //& 0xFF000000);
        }
    }

    // Color32[] to uint[]
    public static void fColor(uint[] pix, UnityEngine.Color32[] mpix)
    {        
        for (int i = 0; i < mpix.Length; i++)
        {
            pix[i] = (uint)(mpix[i].r | (mpix[i].g << 8) | (mpix[i].b << 16) | (mpix[i].a << 24));
        }        
    }    

    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;            
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
