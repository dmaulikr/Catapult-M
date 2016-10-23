using UnityEngine;
using System.Collections;


public class BoulderUpgrade : Upgrade
{
    public  BoulderType type;

    public override int level () {
        return (int)type;
    }
}