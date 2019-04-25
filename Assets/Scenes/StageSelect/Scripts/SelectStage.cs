using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectStage
{
    //カーソルと重なった時に実行
    void OnSelect();
    //カーソルが離れた時に実行
    void RemoveSelect();
    //選択された状態でボタンを押したらscene遷移s
}
