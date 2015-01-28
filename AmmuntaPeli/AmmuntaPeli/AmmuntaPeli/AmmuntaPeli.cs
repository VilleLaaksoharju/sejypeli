using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class AmmuntaPeli : PhysicsGame
{
    Double nopeusYlos = 200;
    Double nopeusAlas =  -200;
    Double nopeusVasen = -200;
    Double nopeusOikea = 200;

    Image Pelaajankuva1 = LoadImage("Pelaaja1");
    Image Pelaajankuva2 = LoadImage("Pelaaja2");
    //Image Pelaajankuva1 = LoadImage("Pelaaja12");
    //Image Pelaajankuva2 = LoadImage("Pelaaja22");
    Image Taustakuva = LoadImage("War Game");

    PlatformCharacter pelaaja1;
    PlatformCharacter pelaaja2;

    PhysicsObject vasenReuna;
    PhysicsObject oikeaReuna;

    public override void Begin()
    {
        IsFullScreen = true;
LuoKentta();
AsetaOhjaimet();


    }

void LuoKentta()

{
    Level.Background.Image = Taustakuva;
    Level.Background.FitToLevel();
    Level.BackgroundColor = Color.Black;

    pelaaja1 = LuoPelaaja1(Level.Left + 90.0, -70.0);
    pelaaja2 = LuoPelaaja2(Level.Left + 800.0, -70.0);
     
    vasenReuna = Level.CreateLeftBorder();
    vasenReuna.Restitution = 1.0;
    vasenReuna.KineticFriction = 0.0;
    vasenReuna.IsVisible = false;

    oikeaReuna = Level.CreateRightBorder();
    oikeaReuna.Restitution = 1.0;
        oikeaReuna.KineticFriction = 0.0;
        oikeaReuna.IsVisible = false;

        PhysicsObject ylaReuna = Level.CreateTopBorder();
        ylaReuna.Restitution = 1.0;
        ylaReuna.KineticFriction = 0.0;
        ylaReuna.IsVisible = false;

        PhysicsObject alaReuna = Level.CreateBottomBorder();
        alaReuna.Restitution = 1.0;
        alaReuna.IsVisible = false;
        alaReuna.KineticFriction = 0.0;
        Camera.ZoomToLevel();

}

PlatformCharacter LuoPelaaja1(double x, double y)
{
    pelaaja1 = new PlatformCharacter(150.0, 150.0);
    pelaaja1.Image = Pelaajankuva1;
    pelaaja1.X = x;
    pelaaja1.Y = y;
    pelaaja1.Restitution = 1.0;
    pelaaja1.KineticFriction = 0.0;
    Add(pelaaja1);
    return pelaaja1;
}

PlatformCharacter LuoPelaaja2(double x, double y)
{
    pelaaja2 = new PlatformCharacter(150.0, 150.0);
    pelaaja2.Image = Pelaajankuva2;
    pelaaja2.X = x;
    pelaaja2.Y = y;
    pelaaja2.Restitution = 1.0;
    pelaaja2.KineticFriction = 0.0;
    Add(pelaaja2);
    return pelaaja2;
}
void AsetaOhjaimet()
{
    Keyboard.Listen(Key.W, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa ylös", pelaaja1, nopeusYlos);
    Keyboard.Listen(Key.W, ButtonState.Released, AsetaNopeus, null, pelaaja1, Double.Zero);
    Keyboard.Listen(Key.S, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa alas", pelaaja1, nopeusAlas);
    Keyboard.Listen(Key.S, ButtonState.Released, AsetaNopeus, null, pelaaja1, Double.Zero);
    Keyboard.Listen(Key.A, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa ylös", pelaaja1, nopeusVasen);
    Keyboard.Listen(Key.A, ButtonState.Released, AsetaNopeus, null, pelaaja1, Double.Zero);
    Keyboard.Listen(Key.D, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa alas", pelaaja1, nopeusOikea);
    Keyboard.Listen(Key.D, ButtonState.Released, AsetaNopeus, null, pelaaja1, Double.Zero);

    Keyboard.Listen(Key.Up, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa ylös", pelaaja2, nopeusYlos);
    Keyboard.Listen(Key.Up, ButtonState.Released, AsetaNopeus, null, pelaaja2, Double.Zero);
    Keyboard.Listen(Key.Down, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa alas", pelaaja2, nopeusAlas);
    Keyboard.Listen(Key.Down, ButtonState.Released, AsetaNopeus, null, pelaaja2, Double.Zero);
    Keyboard.Listen(Key.Left, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa ylös", pelaaja2, nopeusVasen);
    Keyboard.Listen(Key.Left, ButtonState.Released, AsetaNopeus, null, pelaaja2, Double.Zero);
    Keyboard.Listen(Key.Right, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa alas", pelaaja2, nopeusOikea);
    Keyboard.Listen(Key.Right, ButtonState.Released, AsetaNopeus, null, pelaaja2, Double.Zero);

    PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
    Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

}
void AsetaNopeus(PlatformCharacter pelaaja, Double nopeus)
{
    if ((nopeus.Y < 0) && (pelaaja.Bottom < Level.Bottom))
    {
        pelaaja.Walk = Double.Zero;
        return;
    }
    if ((nopeus.Y > 0) && (pelaaja.Top > Level.Top))
    {
        pelaaja.Velocity = Double.Zero;
        return;
    }
    if ((nopeus.X > 0) && (pelaaja.Right > Level.Right))
    {
        pelaaja.Velocity = Double.Zero;
        return;
    }
    if ((nopeus.X < 0) && (pelaaja.Left < Level.Left))
    {
        pelaaja.Velocity = Double.Zero;
        return;
    }

    pelaaja.Velocity = nopeus;
}




  //      PhysicsObject Pelaaja1 = new PhysicsObject( 40, 20 );
//Pelaaja1.Shape = Shape.Rectangle;
//Add(Pelaaja1);
        // TODO: Kirjoita ohjelmakoodisi tähän

        
    
}

