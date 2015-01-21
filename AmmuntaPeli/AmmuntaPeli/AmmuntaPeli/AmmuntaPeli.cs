using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class AmmuntaPeli : PhysicsGame
{
    public override void Begin()
    {
        PhysicsObject Pelaaja1 = new PhysicsObject( 40, 20 );
Pelaaja1.Shape = Shape.Rectangle;
Add(Pelaaja1);
        // TODO: Kirjoita ohjelmakoodisi tähän

        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
    }
}
