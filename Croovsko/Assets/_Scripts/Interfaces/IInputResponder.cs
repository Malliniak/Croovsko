//
//  Square
//
//  Created by Krzysztof Maliński, Adam Kolinski, 
// Damian Klabuhn, Mikolaj Mikolajczak
//  
// Code inspired by Croovsko, project by Krzysztof Malinski
// 


namespace DefaultNamespace
{
    public interface IInputResponder
    {
        void AfterHoldTouch();
        void NoInput();
        void OnTouchUp();
        void OnHold();
    }
}