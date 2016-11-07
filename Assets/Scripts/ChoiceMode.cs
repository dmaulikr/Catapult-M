using UnityEngine;
using System.Collections.Generic;

public class ChoiceMode
{
    //private DuckOriginals dos;
    private int index;
    private int duckIntensity;

    public void reset() {
        duckIntensity = 0;
        index = 0;
    }

    //public ChoiceMode(DuckOriginals dos) {
    //    //this.dos = dos;
    //}

    public int getPick() {
        int[] choices = indices;
        int result = choices[index];
        index = ++index % choices.Length;
        return result;
    }

    public void setIntensity(int i) {
        if (i > duckIntensity) {
            index = 0;
            duckIntensity = i;
            duckIntensity = duckIntensity > pickArrays.Count - 1 ? pickArrays.Count - 1 : duckIntensity;
        }
    }

    protected int[] indices {
        get {
            return pickArrays[duckIntensity];
        }
    }
    public int Length { get { return pickArrays.Count; } }

    protected List<int[]> _pickArrays;
    protected virtual List<int[]> pickArrays {
        get {
            if (_pickArrays == null || _pickArrays.Count == 0) {
                _pickArrays = new List<int[]> {
                    new int[] { 0 },
                    new int[] { 0, 1, 1, 0 },
                    new int[] { 2, 2, 1, 1, 0, 0 },
                    new int[] { 3, 3, 3, 2, 1, 2, 0, 0 },
                    new int[] { 4, 3, 3, 3, 2, 2, 1, 2, 0, 0 },
                    new int[] { 4, 3, 3, 3, 2, 2, 1, 2, 0, 0 },
                };
            }
            return _pickArrays;
        }
    }
}
