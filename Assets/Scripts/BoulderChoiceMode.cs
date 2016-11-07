using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BoulderChoiceMode : ChoiceMode
{
    [SerializeField]
    private int testBoulderType = 1;

    protected override List<int[]> pickArrays {
        get {
            if(testBoulderType > -1) {
                return new List<int[]> { new int[] { testBoulderType } };
            }
            if (_pickArrays == null || _pickArrays.Count == 0) {
                _pickArrays = new List<int[]> {
                    new int[] { 0 },
                    new int[] { 0, 0, 1, 1 },
                    new int[] { 0, 2, 2, 1, 1 },
                    new int[] { 0, 2, 1, 2, 1, 3, 3 },
                    new int[] { 0, 2, 4, 2, 2, 3, 3, 1, 0, 0, 4, 2, 0, 4, 4 },
                };
            }
            return _pickArrays;
        }
    }
}
