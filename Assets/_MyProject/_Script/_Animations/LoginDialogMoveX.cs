using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginDialogMoveX : MonoBehaviour
{
    public void MoveXToLeft(GameObject _goWindows)
    {
        LeanTween.moveX(_goWindows, -2000, 0.3f);
    }

    public void SetDialogLocation(GameObject _goWindows)
    {
        LeanTween.moveX(_goWindows, 20000, 0.3f);
    }
}
