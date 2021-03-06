﻿using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class Ammuntapeli2 : PhysicsGame
{

    Vector pelaaja1alku;
    Vector pelaaja2alku;
    Vector pelaaja3alku;
    Vector pelaaja4alku;
  //  Vector pelaaja5alku;
    Double nopeusVasen = -200;
    Double nopeusOikea = 200;

    Image Pelaajankuva1 = LoadImage("Pelaaja1");
    Image Pelaajankuva2 = LoadImage("Pelaaja2");
    Image Pelaajankuva3 = LoadImage("Pelaaja3");
    Image Pelaajankuva4 = LoadImage("Pelaaja4");
    Image Tiilinkuva = LoadImage("Tiili");
    Image Taustakuva = LoadImage("War Game");
    Image PointBlueCapturenKuva = LoadImage("PointBlueCapture");
    Image PointRedCapturenKuva = LoadImage("PointRedCapture");
    Image PointNotCapturenKuva = LoadImage("PointNotCapture");
    Image Luotikuva = LoadImage("Luoti");
    //Image Pelaajankuva5 = LoadImage("Pelaaja5");
    Image Tiilin2kuva = LoadImage("Tiili2");
    Image pelaajan1kavelu1 = LoadImage("Pelaaja1kavelu1");
    Image pelaajan2kavelu = LoadImage("Pelaaja2kavelu");
    Image pelaajan3kavelu = LoadImage("Pelaaja3kavelu");
    Image pelaajan4kavelu = LoadImage("Pelaaja4kavelu");

    SoundEffect captureAani = LoadSoundEffect("capture");
    SoundEffect hyppyAani = LoadSoundEffect("hyppy");

    IntMeter pisteLaskuri;
    IntMeter pisteLaskuri2;
    PhysicsObject PointNotCapture;

    PlatformCharacter pelaaja1;
    PlatformCharacter pelaaja2;
    PlatformCharacter pelaaja3;
    PlatformCharacter pelaaja4;
   // PlatformCharacter pelaaja5;


    PhysicsObject vasenReuna;
    PhysicsObject oikeaReuna;
    //PhysicsObject PointNotCapture;
    private Image[] kaveluAnimaatio = LoadImages("Pelaaja1", "Pelaaja1kavelu1");
    private Image[] paikallaanAnimaatio = LoadImages("Pelaaja1");
    private Image[] hyppyAnimaatio = LoadImages("pelaaja1hyppy");
    private Image[] laskeutumisAnimaatio = LoadImages("pelaaja1hyppy2");

    private Image[] kaveluAnimaatio2 = LoadImages("Pelaaja2", "Pelaaja2kavelu");
    private Image[] paikallaanAnimaatio2 = LoadImages("Pelaaja2");
    private Image[] hyppyAnimaatio2 = LoadImages("pelaaja2hyppy");
    private Image[] laskeutumisAnimaatio2 = LoadImages("pelaaja2hyppy2");

    private Image[] kaveluAnimaatio3 = LoadImages("Pelaaja3", "Pelaaja3kavelu");
    private Image[] paikallaanAnimaatio3 = LoadImages("Pelaaja3");
    private Image[] hyppyAnimaatio3 = LoadImages("pelaaja3hyppy");
    private Image[] laskeutumisAnimaatio3 = LoadImages("pelaaja3hyppy2");

    private Image[] kaveluAnimaatio4 = LoadImages("Pelaaja4", "Pelaaja4kavelu");
    private Image[] paikallaanAnimaatio4 = LoadImages("Pelaaja4");
    private Image[] hyppyAnimaatio4 = LoadImages("pelaaja4hyppy");
    private Image[] laskeutumisAnimaatio4 = LoadImages("pelaaja4hyppy2");


    public override void Begin()
    {
        IsFullScreen = true;
        LuoKentta();
        AsetaOhjaimet();
        LuoPistelaskuri();
       // LisaaLaskurit();
        LuoPistelaskuri2();
        
        
        

    }

    void LuoKentta()
    {
        MultiSelectWindow alkuValikko = new MultiSelectWindow("Ammunta Peli",
    "Taisteluun", "Poistut pelistä Jos painat ESC (PELI KUITENKIN ALKAA JOS PAINAT TÄSTÄ)");
        alkuValikko.Color = Color.Gray;


        Add(alkuValikko);
        Level.Background.Image = Taustakuva;
        Level.Background.FitToLevel();
        //Level.BackgroundColor = Color.Black;
        //1. Luetaan kuva uuteen ColorTileMappiin, kuvan nimen perässä ei .png-päätettä.
        ColorTileMap ruudut = ColorTileMap.FromLevelAsset("kentta");

        //2. Kerrotaan mitä aliohjelmaa kutsutaan, kun tietyn värinen pikseli tulee vastaan kuvatiedostossa.
        ruudut.SetTileMethod(Color.FromHexCode("000CFF"), LuoPelaaja1);
        ruudut.SetTileMethod(Color.Black, LuoTaso);
        ruudut.SetTileMethod(Color.FromHexCode("808080"), LuoPointNotCapture);
        ruudut.SetTileMethod(Color.Red, LuoPelaaja2);
        ruudut.SetTileMethod(Color.FromHexCode("00FFFF"), LuoPelaaja3);
        ruudut.SetTileMethod(Color.FromHexCode("FF00DC"), LuoPelaaja4);
       // ruudut.SetTileMethod(Color.FromHexCode("808080"), LuoPelaaja5);
        ruudut.SetTileMethod(Color.FromHexCode("404040"), LuoTaso2);
        ruudut.SetTileMethod(Color.FromHexCode("FF6A00"), LuoTaso3);


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
        taso.CollisionIgnoreGroup = 0;
        Add(taso);
        

    }

    void LuoTaso2(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso2 = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso2.Position = paikka;
        taso2.Image = Tiilin2kuva;
        taso2.CollisionIgnoreGroup = 1;
        Add(taso2);

    }

    void LuoTaso3(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject taso3 = PhysicsObject.CreateStaticObject(leveys, korkeus);
        taso3.Position = paikka;
        taso3.Image = Tiilin2kuva;
        taso3.CollisionIgnoreGroup = 2;
        Add(taso3);

    }

    void LuoPointNotCapture(Vector paikka, double leveys, double korkeus)
   {
        PointNotCapture = PhysicsObject.CreateStaticObject(95,95);
        PointNotCapture.Position = paikka;
        PointNotCapture.Image = PointNotCapturenKuva;
        //PointNotCapture.CollisionIgnoreGroup = 1;
        Add(PointNotCapture);
        AddCollisionHandler(PointNotCapture,PNCtormaa);
    }

    void LuoPistelaskuri()
    {
        pisteLaskuri = new IntMeter(0);

        Label pisteNaytto = new Label();
        pisteNaytto.IntFormatString = "Pisteitä: {0:D1}";
        pisteNaytto.X = Screen.Left + 100;
        pisteNaytto.Y = Screen.Top - 100;
        pisteNaytto.TextColor = Color.White;
        pisteNaytto.Color = Color.Blue;

        pisteNaytto.BindTo(pisteLaskuri);
        Add(pisteNaytto);
        pisteLaskuri.MaxValue = 100;
        pisteLaskuri.UpperLimit += TiimiBlueVoittaa;
        

    }
    void TiimiBlueVoittaa()
    {
        Label tekstikentta = new Label(1000,100," Pelaaja1 ja Pelaaja3 Voittaa!       pelaaja2 ja pelaaja 4 on surkeita  ");
        Add(tekstikentta);
        tekstikentta.TextColor = Color.Blue;
        tekstikentta.Font = Font.DefaultLarge;
        
        

    }

    

    void LuoPistelaskuri2()
    {
        pisteLaskuri2 = new IntMeter(0);

        Label pisteNaytto2 = new Label();
        pisteNaytto2.IntFormatString = "Pisteitä: {0:D1}";
        pisteNaytto2.X = Screen.Right - 100;
        pisteNaytto2.Y = Screen.Top - 100;
        pisteNaytto2.TextColor = Color.White;
        pisteNaytto2.Color = Color.Red;

        pisteNaytto2.BindTo(pisteLaskuri2);
        Add(pisteNaytto2);
        pisteLaskuri2.MaxValue = 100;
        pisteLaskuri2.UpperLimit += TiimiRedVoittaa;
    }

    void TiimiRedVoittaa()
    {
        Label tekstikentta2 = new Label(1000, 100, " Pelaaja2 ja Pelaaja4 Voittaa!       pelaaja1 ja pelaaja 3 on surkeita  ");
        Add(tekstikentta2);
        tekstikentta2.TextColor = Color.Red;
        tekstikentta2.Font = Font.DefaultLarge;



    }
    
      

    




    void LuoPelaaja1(Vector paikka, double korkeus, double leveys)
    {

        pelaaja1 = new PlatformCharacter(75.0, 75.0);
        pelaaja1.Weapon = new AssaultRifle(50, 50);
        
       // pelaaja1.Weapon.InfiniteAmmo = true;
        pelaaja1.Weapon.IsVisible = false;
        pelaaja1.Weapon.X = 1.0;
        pelaaja1.Weapon.FireRate = 3.0;
        pelaaja1.Weapon.Y = 15.0;

        Grenade kranaatti = new Grenade(4.0);

        pelaaja1.Weapon.CanHitOwner = false;

       // pelaaja1.Granade = new Granade(4.0);
        pelaaja1.Image = Pelaajankuva1;
        pelaaja1.Position = paikka;
        pelaaja1.Tag = "p1";
        Add(pelaaja1);
        pelaaja1alku = paikka;
        pelaaja1.Weapon.ProjectileCollision = AmmusOsui;
        pelaaja1.CollisionIgnoreGroup = 1;

        pelaaja1.AnimWalk = new Animation(kaveluAnimaatio);
        pelaaja1.AnimIdle = new Animation(paikallaanAnimaatio);
        pelaaja1.AnimJump = new Animation(hyppyAnimaatio);
        pelaaja1.AnimFall = new Animation(laskeutumisAnimaatio);
        pelaaja1.AnimWalk.FPS = 100;
        //pelaaja1.WalkOnAir = false;
    }

    void LuoPelaaja2(Vector paikka, double korkeus, double leveys)
    {
        pelaaja2 = new PlatformCharacter(75.0, 75.0);
       // pelaaja2.Weapon.InfiniteAmmo = true;
        pelaaja2.Image = Pelaajankuva2;
        pelaaja2.Position = paikka;
        pelaaja2.Weapon = new AssaultRifle(50, 50);
        
        pelaaja2.Weapon.FireRate = 3.0;
        pelaaja2.Weapon.IsVisible = false;
        pelaaja2.Weapon.X = 1.0;
        pelaaja2.Weapon.Y = 15.0;
        
        pelaaja2.Tag = "p2";
        Add(pelaaja2);
        pelaaja2alku = paikka;
        pelaaja2.Weapon.ProjectileCollision = AmmusOsui;
        pelaaja2.CollisionIgnoreGroup = 2;

        pelaaja2.AnimWalk = new Animation(kaveluAnimaatio2);
        pelaaja2.AnimIdle = new Animation(paikallaanAnimaatio2);
        pelaaja2.AnimJump = new Animation(hyppyAnimaatio2);
        pelaaja2.AnimFall = new Animation(laskeutumisAnimaatio2);

        pelaaja2.AnimWalk.FPS = 100;

    }

    void LuoPelaaja3(Vector paikka, double korkeus, double leveys)
    {
        pelaaja3 = new PlatformCharacter(75.0, 75.0);
       // pelaaja3.Weapon.InfiniteAmmo = true;
        pelaaja3.Image = Pelaajankuva3;
        pelaaja3.Position = paikka;
        pelaaja3.Weapon = new AssaultRifle(50, 50);
        
        pelaaja3.Weapon.FireRate = 3.0;
        pelaaja3.Weapon.IsVisible = false;
        pelaaja3.Weapon.X = 1.0;
        pelaaja3.Weapon.Y = 15.0;
        
        pelaaja3.Tag = "p3";
        Add(pelaaja3);
        pelaaja3alku = paikka;
        pelaaja3.Weapon.ProjectileCollision = AmmusOsui;
        pelaaja3.CollisionIgnoreGroup = 1;

        pelaaja3.AnimWalk = new Animation(kaveluAnimaatio3);
        pelaaja3.AnimIdle = new Animation(paikallaanAnimaatio3);
        pelaaja3.AnimJump = new Animation(hyppyAnimaatio3);
        pelaaja3.AnimFall = new Animation(laskeutumisAnimaatio3);
        pelaaja3.AnimWalk.FPS = 100;
    }
    void LuoPelaaja4(Vector paikka, double korkeus, double leveys)
    {
        pelaaja4 = new PlatformCharacter(75.0, 75.0);
       // pelaaja4.Weapon.InfiniteAmmo = true;
        pelaaja4.Image = Pelaajankuva4;
        pelaaja4.Position = paikka;
        pelaaja4.Weapon = new AssaultRifle(50, 50);
        pelaaja4.Weapon.IsVisible = false;
        pelaaja4.Weapon.FireRate = 3.0;
        pelaaja4.Weapon.X = 1.0;
        pelaaja4.Weapon.Y = 15.0;
        pelaaja4.Tag = "p4";
        Add(pelaaja4);
        pelaaja4alku = paikka;
        pelaaja4.Weapon.ProjectileCollision = AmmusOsui;
        pelaaja4.CollisionIgnoreGroup = 2;

        pelaaja4.AnimWalk = new Animation(kaveluAnimaatio4);
        pelaaja4.AnimIdle = new Animation(paikallaanAnimaatio4);
        pelaaja4.AnimJump = new Animation(hyppyAnimaatio4);
        pelaaja4.AnimFall = new Animation(laskeutumisAnimaatio4);
        pelaaja4.AnimWalk.FPS = 100;
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


        Keyboard.Listen(Key.T, ButtonState.Down, Hyppy, "Pelaaja 3: Liikuta mailaa ylös", pelaaja3);

        Keyboard.Listen(Key.F, ButtonState.Down, AsetaNopeus, "Pelaaja 3: Liikuta mailaa ylös", pelaaja3, nopeusVasen);

        Keyboard.Listen(Key.H, ButtonState.Down, AsetaNopeus, "Pelaaja 3: Liikuta mailaa alas", pelaaja3, nopeusOikea);

        Keyboard.Listen(Key.G, ButtonState.Down, AmmuAseella, "Ammu", pelaaja3);




        Keyboard.Listen(Key.I, ButtonState.Down, Hyppy, "Pelaaja 4: Liikuta mailaa ylös", pelaaja4);

        Keyboard.Listen(Key.J, ButtonState.Down, AsetaNopeus, "Pelaaja 4: Liikuta mailaa ylös", pelaaja4, nopeusVasen);

        Keyboard.Listen(Key.L, ButtonState.Down, AsetaNopeus, "Pelaaja 4: Liikuta mailaa alas", pelaaja4, nopeusOikea);

        Keyboard.Listen(Key.K, ButtonState.Down, AmmuAseella, "Ammu", pelaaja4);


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


        if (ammus != null)
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
            kohde.Position = pelaaja1alku;
            pisteLaskuri2.Value += 1;
            
            
        }
        if (kohde.Tag == "p2")

        {
            kohde.Position = pelaaja2alku;
            pisteLaskuri.Value += 1;
        }
        if (kohde.Tag == "p3")
        {
            kohde.Position = pelaaja3alku;
            pisteLaskuri2.Value += 1;
        }
        if (kohde.Tag == "p4")
        {
            kohde.Position = pelaaja4alku;
            pisteLaskuri.Value += 1;
        }

    }
    void PNCtormaa(PhysicsObject PNC, PhysicsObject Ukko)
    {
        if (Ukko.Tag == "p1")
        {
            Ukko.Position = pelaaja1alku;
            pisteLaskuri.Value += 5;
            captureAani.Play();
            PointNotCapture.Image = PointBlueCapturenKuva;

        }

        if (Ukko.Tag == "p2")
        {
            Ukko.Position = pelaaja2alku;
            pisteLaskuri2.Value += 5;
            captureAani.Play();
            PointNotCapture.Image = PointRedCapturenKuva;
        }

        if (Ukko.Tag == "p3")
        {
            Ukko.Position = pelaaja3alku;
            pisteLaskuri.Value += 5;
            captureAani.Play();
            PointNotCapture.Image = PointBlueCapturenKuva;
        }

        if (Ukko.Tag == "p4")
        {
            Ukko.Position = pelaaja4alku;
            pisteLaskuri2.Value += 5;
            captureAani.Play();
            PointNotCapture.Image = PointRedCapturenKuva;
        }



    }


   

    


//      PhysicsObject Pelaaja1 = new PhysicsObject( 40, 20 );
    //Pelaaja1.Shape = Shape.Rectangle;
    //Add(Pelaaja1);
    // TODO: Kirjoita ohjelmakoodisi tähän



}
