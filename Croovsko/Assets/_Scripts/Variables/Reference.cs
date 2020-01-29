//
//  Square
//
//  Created by Krzysztof Maliński, Adam Kolinski, 
// Damian Klabuhn, Mikolaj Mikolajczak
//  
// Code inspired by Croovsko, project by Krzysztof Malinski
// 


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Reference<T>
{
    public bool UseConstant = true;
    public T ConstantValue;

    public Reference()
    { }

    public Reference(T value)
    {
        UseConstant = true;
        ConstantValue = value;
    }
}
