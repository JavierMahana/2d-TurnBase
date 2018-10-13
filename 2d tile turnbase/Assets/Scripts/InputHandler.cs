using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputHandler {

    private static Comand rClick = new StartMoveComand();
    private static Comand rClickRelease = new EndMoveComand();

    public static Comand handleInput()
    {
        if (Input.GetMouseButton(1))
        {
            return rClick;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            return rClickRelease;
        }
        else
            return null;
    }
}
