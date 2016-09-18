using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoulderChoiceMode : ChoiceMode
{
   
    protected override List<int[]> pickArrays {
        get {
            if (_pickArrays == null || _pickArrays.Count == 0) {
                _pickArrays = new List<int[]> {
                    new int[] { 0 },
                    new int[] { 0, 0, 1, 1 },
                    new int[] { 2, 2, 1, 1 },
                    new int[] { 2, 1, 2, 1, 3, 3 },
                    //new int[] { 0 },
                    //new int[] { 0, 0, 1, 1 },
                    //new int[] { 2, 2, 1, 1 },
                    //new int[] { 2 },
                };
            }
            return _pickArrays;
        }
    }
}
