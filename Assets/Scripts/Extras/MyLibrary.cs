using UnityEngine;

public static class MyLibrary {
    public static bool CheckLayer(int layer, LayerMask objectMask)
    {
        return ((1 << layer) & objectMask) != 0;
    }
}