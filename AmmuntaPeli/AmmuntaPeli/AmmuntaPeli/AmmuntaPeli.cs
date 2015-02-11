using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class AmmuntaPeli : PhysicsGame
{

    Double nopeusVasen = -200;
    Double nopeusOikea = 200;

    Image Pelaajankuva1 = LoadImage("Pelaaja1");
    Image Pelaajankuva2 = LoadImage("Pelaaja2");
    Image Tiilinkuva = LoadImage("Tiili");
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
    //Level.BackgroundColor = Color.Black;
    //1. Luetaan kuva uuteen ColorTileMappiin, kuvan nimen perässä ei .png-päätettä.
    ColorTileMap ruudut = ColorTileMap.FromLevelAsset("kentta");

    //2. Kerrotaan mitä aliohjelmaa kutsutaan, kun tietyn värinen pikseli tulee vastaan kuvatiedostossa.
    ruudut.SetTileMethod(Color.FromHexCode("000CFF"), LuoPelaaja1);
    ruudut.SetTileMethod(Color.Black, LuoTaso);
   
    ruudut.SetTileMethod(Color.Red, LuoPelaaja2);

    //3. Execute luo kentän
    //   Parametreina leveys ja korkeus
    ruudut.Execute(25, 25);


    Gravity = new Vector(0, -1000);
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

void LuoTaso(Vector paikka, double leveys, double korkeus)
{
    PhysicsObject taso = PhysicsObject.CreateStaticObject(leveys, korkeus);
    taso.Position = paikka;
    taso.Image = Tiilinkuva;
    taso.CollisionIgnoreGroup = 1;
    Add(taso);
}


void LuoPelaaja1(Vector paikka, double korkeus, double leveys)
{
    pelaaja1 = new PlatformCharacter(75.0, 75.0);
    pelaaja1.Image = Pelaajankuva1;
    pelaaja1.Position = paikka;
    
    Add(pelaaja1);
 
}

void LuoPelaaja2(Vector paikka, double korkeus, double leveys)
{
    pelaaja2 = new PlatformCharacter(75.0, 75.0);
    pelaaja2.Image = Pelaajankuva2;
    pelaaja2.Position = paikka;
    
    Add(pelaaja2);

}
void AsetaOhjaimet()
{
    Keyboard.Listen(Key.W, ButtonState.Down, Hyppy, "Pelaaja 1: Liikuta mailaa ylös", pelaaja1);
  

 
    Keyboard.Listen(Key.A, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa ylös", pelaaja1, nopeusVasen);
 
    Keyboard.Listen(Key.D, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa alas", pelaaja1, nopeusOikea);
   

    Keyboard.Listen(Key.Up, ButtonState.Down, Hyppy, "Pelaaja 2: Liikuta mailaa ylös", pelaaja2);


   
    Keyboard.Listen(Key.Left, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa ylös", pelaaja2, nopeusVasen);
   
    Keyboard.Listen(Key.Right, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa alas", pelaaja2, nopeusOikea);


    PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
    Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");

}
void AsetaNopeus(PlatformCharacter pelaaja, Double nopeus)
{

    pelaaja.Walk(nopeus);
}

void Hyppy(PlatformCharacter pelaaja)
{
    pelaaja.Jump(700);

}



  //      PhysicsObject Pelaaja1 = new PhysicsObject( 40, 20 );
//Pelaaja1.Shape = Shape.Rectangle;
//Add(Pelaaja1);
        // TODO: Kirjoita ohjelmakoodisi tähän

        
    
}

