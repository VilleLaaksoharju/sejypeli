using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class pong2 : PhysicsGame
    PhysicsObject pallo;
{
    public override void Begin()
    {
        
        LuoKentta();

        // TODO: Kirjoita ohjelmakoodisi tähän

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
}


void LuoKentta()
    {
PhysicsObject pallo = new PhysicsObject(40.0, 40.0);
        Add(pallo);
        pallo.Shape = Shape.Circle;
        pallo.X = -200.0;
        pallo.Y = 0.0;
        Vector impulssi = new Vector(500.0, 0.0);
        pallo.Hit(impulssi);
        Level.CreateBorders();
        pallo.Restitution = 1.0;
        Level.CreateBorders();
        Level.CreateBorders(1.0, false);
        Level.Background.Color = Color.Black;
        Camera.ZoomToLevel();

}