using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class AmmuntaPeli : PhysicsGame
{

    Vector pelaaja1alku;
    Vector pelaaja2alku;
    Vector pelaaja3alku;
    Vector pelaaja4alku;
    Double nopeusVasen = -200;
    Double nopeusOikea = 200;

    Image Pelaajankuva1 = LoadImage("Pelaaja1");
    Image Pelaajankuva2 = LoadImage("Pelaaja2");
    Image Pelaajankuva3 = LoadImage("Pelaaja3");
    Image Pelaajankuva4 = LoadImage("Pelaaja4");
    Image Tiilinkuva = LoadImage("Tiili");
    Image Taustakuva = LoadImage("War Game");
    Image PointNotCapturenKuva = LoadImage("PointNotCapture");
    Image Luotikuva = LoadImage("Luoti");


    PlatformCharacter pelaaja1;
    PlatformCharacter pelaaja2;
    PlatformCharacter pelaaja3;
    PlatformCharacter pelaaja4;


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
    MultiSelectWindow alkuValikko = new MultiSelectWindow("Ammunta Peli",
"Taisteluun>:)", "Parhaat pisteet :P ", "Poistu Pelistä >:(");
    alkuValikko.Color = Color.Blue;


    Add(alkuValikko);
    Level.Background.Image = Taustakuva;
    Level.Background.FitToLevel();
    //Level.BackgroundColor = Color.Black;
    //1. Luetaan kuva uuteen ColorTileMappiin, kuvan nimen perässä ei .png-päätettä.
    ColorTileMap ruudut = ColorTileMap.FromLevelAsset("kentta");

    //2. Kerrotaan mitä aliohjelmaa kutsutaan, kun tietyn värinen pikseli tulee vastaan kuvatiedostossa.
    ruudut.SetTileMethod(Color.FromHexCode("000CFF"), LuoPelaaja1);
    ruudut.SetTileMethod(Color.Black, LuoTaso);
    ruudut.SetTileMethod(Color.FromHexCode("00FF21"), LuoPointNotCapture);
    ruudut.SetTileMethod(Color.Red, LuoPelaaja2);
    ruudut.SetTileMethod(Color.FromHexCode("00FFFF"), LuoPelaaja3);
    ruudut.SetTileMethod(Color.FromHexCode("FF00DC"), LuoPelaaja4);
   
    
    

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
        ylaReuna.IsVisible = true;

        PhysicsObject alaReuna = Level.CreateBottomBorder();
        alaReuna.Restitution = 1.0;
        alaReuna.IsVisible = true;
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

void LuoPointNotCapture(Vector paikka, double leveys, double korkeus)
{
    PhysicsObject pointNotCapture = PhysicsObject.CreateStaticObject(50, 50);
    pointNotCapture.Position = paikka;
    pointNotCapture.Image = PointNotCapturenkuva;
    //pointNotCapture.CollisionIgnoreGroup = 1;
    Add(pointNotCapture,2);
}


void LuoPelaaja1(Vector paikka, double korkeus, double leveys)
{
    
    pelaaja1 = new PlatformCharacter(75.0, 75.0);
    pelaaja1.Weapon = new AssaultRifle(50, 50);
    pelaaja1.Weapon.InfiniteAmmo = true;
    pelaaja1.Weapon.IsVisible = false;
    pelaaja1.Weapon.X = 1.0;
    pelaaja1.Weapon.FireRate = 3.0;
    pelaaja1.Weapon.Y = 15.0;
    pelaaja1.Weapon.ProjectileCollision = AmmusOsui;
    pelaaja1.Weapon.CanHitOwner = false;
    pelaaja1.Image = Pelaajankuva1;
    pelaaja1.Position = paikka;
    pelaaja1.Tag = "p1";
    Add(pelaaja1);a1
    pelaajalku=paikka;
 
}

void LuoPelaaja2(Vector paikka, double korkeus, double leveys)
{
    pelaaja2 = new PlatformCharacter(75.0, 75.0);
    pelaaja2.Weapon.InfiniteAmmo = true;
    pelaaja2.Image = Pelaajankuva2;
    pelaaja2.Position = paikka;
    pelaaja2.Weapon = new AssaultRifle(50, 50);
    pelaaja2.Weapon.Ammo.Value = 60;
    pelaaja2.Weapon.FireRate = 3.0;
    pelaaja2.Weapon.IsVisible = false;
    pelaaja2.Weapon.X = 1.0;
    pelaaja2.Weapon.Y = 15.0;
    pelaaja2.Weapon.ProjectileCollision = AmmusOsui;
    pelaaja2.Tag = "p2";
    Add(pelaaja2);
    pelaaja2alku = paikka;

}

void LuoPelaaja3(Vector paikka, double korkeus, double leveys)
{
    pelaaja3 = new PlatformCharacter(75.0, 75.0);
    pelaaja3.Weapon.InfiniteAmmo = true;
    pelaaja3.Image = Pelaajankuva3;
    pelaaja3.Position = paikka;
    pelaaja3.Weapon = new AssaultRifle(50, 50);
    pelaaja3.Weapon.Ammo.Value = 60;
    pelaaja3.Weapon.FireRate = 3.0;
    pelaaja3.Weapon.IsVisible = false;
    pelaaja3.Weapon.X = 1.0;
    pelaaja3.Weapon.Y = 15.0;
    pelaaja3.Weapon.ProjectileCollision = AmmusOsui;
    pelaaja3.Tag = "p3";
    Add(pelaaja3);
    pelaaja3alku = paikka;

}
void LuoPelaaja4(Vector paikka, double korkeus, double leveys)
{
    pelaaja4 = new PlatformCharacter(75.0, 75.0);
    pelaaja4.Weapon.InfiniteAmmo = true;
    pelaaja4.Image = Pelaajankuva4;
    pelaaja4.Position = paikka;
    pelaaja4.Weapon = new AssaultRifle(50, 50);
    pelaaja4.Weapon.Ammo.Value = 60;
    pelaaja4.Weapon.IsVisible = false;
    pelaaja4.Weapon.FireRate = 3.0;
    pelaaja4.Weapon.X = 1.0;
    pelaaja4.Weapon.Y = 15.0;
    pelaaja4.Weapon.ProjectileCollision = AmmusOsui;
    pelaaja4.Tag = "p4";
    Add(pelaaja4);
    pelaaja4alku = paikka;
}
void AsetaOhjaimet()
{
    Keyboard.Listen(Key.W, ButtonState.Down, Hyppy, "Pelaaja 1: Liikuta mailaa ylös", pelaaja1);
 
    Keyboard.Listen(Key.A, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa ylös", pelaaja1, nopeusVasen);
 
    Keyboard.Listen(Key.D, ButtonState.Down, AsetaNopeus, "Pelaaja 1: Liikuta mailaa alas", pelaaja1, nopeusOikea);

    Keyboard.Listen(Key.S, ButtonState.Down, AmmuAseella, "Ammu", pelaaja1);

    Keyboard.Listen(Key.Up, ButtonState.Down, Hyppy, "Pelaaja 2: Liikuta mailaa ylös", pelaaja2);

    Keyboard.Listen(Key.Left, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa ylös", pelaaja2, nopeusVasen);
   
    Keyboard.Listen(Key.Right, ButtonState.Down, AsetaNopeus, "Pelaaja 2: Liikuta mailaa alas", pelaaja2, nopeusOikea);

    Keyboard.Listen(Key.Down, ButtonState.Down, AmmuAseella, "Ammu", pelaaja2);

    Keyboard.Listen(Key.I, ButtonState.Down, Hyppy, "Pelaaja 3: Liikuta mailaa ylös", pelaaja3);

    Keyboard.Listen(Key.J, ButtonState.Down, AsetaNopeus, "Pelaaja 3: Liikuta mailaa ylös", pelaaja3, nopeusVasen);

    Keyboard.Listen(Key.L, ButtonState.Down, AsetaNopeus, "Pelaaja 3: Liikuta mailaa alas", pelaaja3, nopeusOikea);

    Keyboard.Listen(Key.K, ButtonState.Down, AmmuAseella, "Ammu", pelaaja3);

    Keyboard.Listen(Key.T, ButtonState.Down, Hyppy, "Pelaaja 4: Liikuta mailaa ylös", pelaaja4);

    Keyboard.Listen(Key.F, ButtonState.Down, AsetaNopeus, "Pelaaja 4: Liikuta mailaa ylös", pelaaja4, nopeusVasen);

    Keyboard.Listen(Key.H, ButtonState.Down, AsetaNopeus, "Pelaaja 4: Liikuta mailaa alas", pelaaja4, nopeusOikea);

    Keyboard.Listen(Key.G, ButtonState.Down, AmmuAseella, "Ammu", pelaaja4);


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
void AmmuAseella(PlatformCharacter pelaaja1)
{
    PhysicsObject ammus = pelaaja1.Weapon.Shoot();
    
  
    if(ammus != null)
    {
        ammus.Size *= 3;
        ammus.Image = Luotikuva;
        ammus.MaximumLifetime = TimeSpan.FromSeconds(2.0);
        
        }
}
void AmmusOsui(PhysicsObject ammus, PhysicsObject kohde)
{
   
    ammus.Destroy();
    
   
    if (kohde.Tag == "p1")
  
    {
 kohde.Position=pelaaja1alku;
    }
    if (kohde.Tag == "p2")
    {
        kohde.Position = pelaaja2alku;
    }
    if (kohde.Tag == "p3")
    {
        kohde.Position = pelaaja3alku;
    }
    if (kohde.Tag == "p4")
    {
        kohde.Position = pelaaja4alku;
    }
}



  //      PhysicsObject Pelaaja1 = new PhysicsObject( 40, 20 );
//Pelaaja1.Shape = Shape.Rectangle;
//Add(Pelaaja1);
        // TODO: Kirjoita ohjelmakoodisi tähän

        
    
}

