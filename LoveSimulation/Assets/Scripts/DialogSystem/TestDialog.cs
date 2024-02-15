using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialog : MonoBehaviour
{
    public PagingSystem pagingSystem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if(false == pagingSystem.isActive)
                pagingSystem.Show();
            else
                pagingSystem.Hide();
        }
    }
}
