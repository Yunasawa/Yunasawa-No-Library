using UnityEditor;
using UnityEngine;
using YNL.Attribute;

public class Test : MonoBehaviour
{
    public bool TestBool;
    [HideIfBool("TestBool", true)] public int A;
    [ShowIfBool("TestBool", false)] public int B;

    public EnumTest TestEnum;
    [HideIfEnum("TestEnum", (int)EnumTest.A)] public string EnumA;
    [ShowIfEnum("TestEnum", (int)EnumTest.B)] public string EnumB;

    public GameObject TestNull;
    [HideIfNull("TestNull", true)] public Color HideNull;
    [HideIfNull("TestNull", false)] public Vector3 HideNotNull;
    [ShowIfNull("TestNull", true)] public Vector3 ShowNull;
    [ShowIfNull("TestNull", false)] public Vector3 ShowNotNull;


}

[System.Serializable]
public enum EnumTest
{
    A, B, C
}