//
//  Square
//
//  Created by Krzysztof Maliński, Adam Kolinski, 
// Damian Klabuhn, Mikolaj Mikolajczak
//  
// Code inspired by Croovsko, project by Krzysztof Malinski
// 


using System;
using UnityEngine;

[Serializable]
public class Vector2Reference : Reference<Vector2>
{
    public Vector2Variable _variable;

    public Vector2 Value => UseConstant ? ConstantValue : _variable._value;
}