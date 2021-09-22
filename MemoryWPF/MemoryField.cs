using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MemoryWPF
{
    class MemoryField
    {
        MemoryCard[] cards;
        
        Button cringeButton;
       
        string[] pictures =
        {"apfel.bmp", "birne.bmp",
         "blume.bmp", "TRex.png",
         "ente.bmp", "fisch.bmp",
         "fuchs.bmp", "igel.bmp",
         "kaenguruh.bmp", "katze.bmp",
         "LioMarvin.jpg", "maus1.bmp",
         "maus2.bmp", "maus3.bmp",
         "melone.bmp", "pilz.bmp",
         "ronny.bmp", "schmetterling.bmp",
         "sonne.bmp", "wolke.bmp",
         "maus4.bmp"};
        
        Label lab_Skill;
        Label human;
        int skliiIndex;
        SolidColorBrush skillBrush;
       
        string playerName;
        
        int playerPoints, computerPoints;
        Label humanPointsLabel, computerPointsLabel;
       
        int upSideDownCard;
        
        MemoryCard[] pair;
        
        int player;
        
        int[,] notedCard;
        
        UniformGrid field;
        System.Windows.Threading.DispatcherTimer timer;
       
        System.Windows.Threading.DispatcherTimer timerCringe;
       
        int comSkill;
        
        
        public MemoryField(UniformGrid field,string name,int seconds,int skillCom)
        {
          
            playerName = name;
            comSkill = skillCom;
            switch (skillCom)
            {
                case 6:
                    skliiIndex = 0;
                    skillBrush = Brushes.Green;
                    break;
                case 2:
                    skliiIndex = 1;
                    skillBrush = Brushes.Orange;
                    break;
                case 0:
                    skliiIndex = 2;
                    skillBrush = Brushes.Red;
                    break;
            }
            Random randomValue = new Random();
            int count = 0;
            cards = new MemoryCard[42];
            pair = new MemoryCard[2];
            notedCard = new int[2, 21];
            computerPoints = 0;
            upSideDownCard = 0;
            player = 0;
            this.field = field;
           
           


            for (int outside = 0; outside < 2; outside++)
                for (int inside = 0; inside < 21; inside++)
                    notedCard[outside, inside] = -1;
           
            for (int i = 0; i <= 41; i++)
            {
               
                cards[i] = new MemoryCard(pictures[count], count, this);
               
               
                //bei jeder zweiten Karte kommt auch ein neues Bild
                //Wenn i ohne rest durch 2 teilbar ist count++
                if ((i + 1) % 2 == 0)
                    count++;
            }

            //Die Karten Mischen und zufällig positionieren
            for (int i = 0; i <= 41; i++)
            {
                int temp1;
                MemoryCard temp2;
               
                temp1 = randomValue.Next(42);
               
                temp2 = cards[temp1];
                
                cards[temp1] = cards[i];
                cards[i] = temp2;
            }

            
            for (int i = 0; i <= 41; i++)
            {
               
                cards[i].SetPicturePos(i);
                
                field.Children.Add(cards[i]);

            }
            
            human = new Label();
            human.Content = name;
           
            field.Children.Add(human);
            humanPointsLabel = new Label();
            humanPointsLabel.Content = 0;
            field.Children.Add(humanPointsLabel);
            Label computer = new Label();
            computer.Content = "Computer";
            field.Children.Add(computer);
            computerPointsLabel = new Label();
            computerPointsLabel.Content = 0;
            field.Children.Add(computerPointsLabel);
            
            
           
           

            Label skill = new Label { Content = "Difficulty: ", 
                Height = 22, 
                Width = 64, 
                Background = skillBrush,
                FontSize = 10.0,
                VerticalContentAlignment = VerticalAlignment.Center, 
                Foreground = Brushes.White };

            lab_Skill = skill;
            field.Children.Add(skill);
           
            ComboBox skillOptions = new ComboBox();
            skillOptions.Height = 22;
            skillOptions.Width = 64;
            skillOptions.FontSize = 10.0;
            skillOptions.Foreground = Brushes.Black;
            skillOptions.SelectedIndex = skliiIndex;

            skillOptions.Items.Add("Easy");
            skillOptions.Items.Add("Medium");
            skillOptions.Items.Add("Hard");
            skillOptions.SelectionChanged += new SelectionChangedEventHandler(SkillOptionsChanged);
            field.Children.Add(skillOptions);

            
            Button cringe = new Button { Height = 22, 
                Width = 64, 
                Content = "Cringe", 
                Background = Brushes.DarkMagenta,
                Foreground = Brushes.White };

            cringe.Click += new RoutedEventHandler(CringeMode);
            
            cringeButton = cringe;
            field.Children.Add(cringe);
            
            timer = new System.Windows.Threading.DispatcherTimer();

            
            timer.Interval = TimeSpan.FromMilliseconds(seconds);
           
            timer.Tick += new EventHandler(Timer_Tick);
          
          
        }
       
       
      
        private void CringeMode(object sender, RoutedEventArgs e)
        {
           
                
                
                
                
                for (int i = 0; i < 42; i++)
                {
                    if (cards[i].IsStillinGame() == true)
                    {
                       
                        cards[i].ShowFrontSide();
                    }
                }
                
                timerCringe = new System.Windows.Threading.DispatcherTimer();
                timerCringe.Interval = TimeSpan.FromMilliseconds(5000);
                timerCringe.Tick += new EventHandler(TimerCringe_Tick);
                timerCringe.Start();
                
                
                
            
            
        }
        

        private void TimerCringe_Tick(object sender, EventArgs e)
        {
           
             timerCringe.Stop();

            for (int i = 0; i < 42; i++)
            {
                if (cards[i].IsStillinGame() == true)
                {
                    cards[i].ShowBackside(false);
                }
            }
           
        }
        private void SkillOptionsChanged(object sender, SelectionChangedEventArgs e)
        {
            string mode = (sender as ComboBox).SelectedItem as string;
            switch (mode)
            {
                case "Easy":
                    comSkill = 6;
                    lab_Skill.Background = Brushes.Green;
                    break;
                case "Medium":
                    comSkill = 2;
                    lab_Skill.Background = Brushes.Orange;
                    break;
                case "Hard":
                    comSkill = 0;
                    lab_Skill.Background = Brushes.Red;
                    break;
            }

        }

        //die Methode liefert, ob Züge des Spielers erlaubt sind
        //die Rückgabe ist false, wenn gerade der Computer zieht oder
        //wenn schon zwei Karten umgedreht sind
        //sonst ist die Rückgabe true
        public bool PermittedMove()
        {
            bool erlaubt = true;
           
            if (player == 1)
                erlaubt = false;
           
            if (upSideDownCard == 2)
                erlaubt = false;
            return erlaubt;
        }


        
        private void Timer_Tick(object sender, EventArgs e)
        {
           
            timer.Stop();
           
            CloseCard();
        }
        //die Methode übernimmt die wesentliche Steuerung des Spiels
        //Sie wird beim Anklicken einer Karte ausgeführt
        public void OpenCard(MemoryCard card)
        {
            cringeButton.IsEnabled = false;
            int cardID, cardPos;
            pair[upSideDownCard] = card;
           
            cardID = card.GetPictureID();
            cardPos = card.GetPicturePos();
            //die Karte in das Gedächtnis des Computers eintragen,
            //aber nur dann, wenn es noch keinen Eintrag an der
            //entsprechenden Stelle gibt
            if ((notedCard[0, cardID] == -1))
                 notedCard[0, cardID] = cardPos;
            else
           
            if (notedCard[0, cardID] != cardPos)
                notedCard[1, cardID] = cardPos;
           
            upSideDownCard++;
            
            //sind zwei Karten umgedreht worden?
            if (upSideDownCard == 2)
            {
               
                CheckPair(cardID);
               
                timer.Start();

            }
            //haben wir zusammen 21 Paare, dann ist das Spiel vorbei
            if (computerPoints + playerPoints == 21)
            {
               
                string winner = string.Empty;
               
                string announcement = string.Empty;
                
                string playerSkill = string.Empty;
               
                int pairs = 0;
                timer.Stop();
               
                switch (comSkill)
                {
                    case 6:
                        playerSkill = "Naja, das war aber auch leicht!!!";
                        break;
                    case 2:
                        playerSkill = "Wow, das war schon gar nicht so leicht!!!";
                        break;
                    case 0:
                        playerSkill = "Unglaublich das Sie das geschafft haben!!!";
                        break;
                }
               
                if (computerPoints > playerPoints)
                {
                    winner = "Computer";
                    playerSkill = string.Empty;
                    pairs = computerPoints;
                    announcement = "Viel Glück beim nächsten mal.";
                }
                if (computerPoints < playerPoints)
                {
                    winner = "Spieler " + playerName;
                    pairs = playerPoints;
                    announcement = "Herzlichen Glückwunsch.";
                }

               
                MessageBox.Show("Der " + winner + " hat mit " + pairs + " Paaren gewonnen. \nDas Spiel endet \n\n" + playerPoints + " : " + computerPoints + " für den " + winner + "\n\n" + announcement + "\n" + playerSkill);
               
                Application.Current.Shutdown();
            }

        }

        private void CheckPair(int cardID)
        {
            if (pair[0].GetPictureID() == pair[1].GetPictureID())
            {
                //die Punkte setzen
                FoundPair();
                //die Karten aus dem Gedächtnis löschen
                notedCard[0, cardID] = -2;
                notedCard[1, cardID] = -2;
            }
        }

        private void FoundPair()
        {
            //spielt gerade der Mensch?
            if (player == 0)
            {
                //************** Aufgabe 2 ***************

                cringeButton.IsEnabled = true;
                playerPoints++;
                humanPointsLabel.Content = playerPoints.ToString();
            }
            else
            {
                computerPoints++;
                computerPointsLabel.Content = computerPoints.ToString();
            }
        }

        private void CloseCard()
        {
            bool @out = false;
            //ist es ein Paar?
            if (pair[0].GetPictureID() == pair[1].GetPictureID())
                @out = true;
            //wenn es ein Paar war, nehmen wir die Karten aus
            //dem Spiel, sonst drehen wir sie nur wieder um
            pair[0].ShowBackside(@out);
            pair[1].ShowBackside(@out);
            //es ist keine Karte mehr geöffnet
            upSideDownCard = 0;
            //hat der Spieler kein Paar gefunden?
            if (@out == false)
                //dann wird der Spieler gewechselt
                ChangePlayer();
            else
            //hat der Computer ein Paar gefunden?
            //dann ist er noch einmal an der Reihe
            if (player == 1)
               ComMove();
        }

        private void ChangePlayer()
        {
            //wenn der Mensch an der Reihe war, kommt jetzt der Computer
            if (player == 0)
            {
                //*************** Aufgabe 2 ************
                //Den Schummel Button deaktivieren
                cringeButton.IsEnabled = false;
                player = 1;
                ComMove();
            }
            else
            {
                // Den Schummel Button aktivieren
                
                cringeButton.IsEnabled = true;
                player = 0;
            }
                
        }

        private void ComMove()
        {
            int cardCounter = 0;
            int random = 0;
            bool hit = false;
            Random randomNumber = new Random();
            if (randomNumber.Next(comSkill) == 0)
            {
                //erst einmal nach einem Paar suchen
                //dazu durchsuchen wir das Array gemerkteKarten, bis wir
                //in beiden Dimensionen einen Wert für eine Karte finden
                while ((cardCounter < 21) && (hit == false))
                {
                    //gibt es in beiden Dimensionen einen Wert größer oder
                    //gleich 0?

                    if ((notedCard[0, cardCounter] >= 0) && (notedCard[1, cardCounter] >= 0))
                    {
                        //dann haben wir ein Paar
                        hit = true;
                        //die erste Karte umdrehen durch einen simulierten
                        //Klick auf die Karte
                        //der simulierte Klick wird nicht mehr ausgeführt
                        //karten[gemerkteKarten[0,
                        //kartenZaehler]].RaiseEvent(new
                        //RoutedEventArgs(ButtonBase.ClickEvent));
                        //die Vorderseite zeigen
                        
                        cards[notedCard[0,cardCounter]].ShowFrontSide();
                        //und die Karte öffnen
                        OpenCard(cards[notedCard[0, cardCounter]]);


                        //die zweite Karte auch
                        //cards[notedCard[1, cardCounter]].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                        //die Vorderseite zeigen
                        cards[notedCard[1,cardCounter]].ShowFrontSide();
                        //und die Karte öffnen
                        OpenCard(cards[notedCard[1, cardCounter]]);
                    }
                    cardCounter++;
                }
            }
                //wenn wir kein Paar gefunden haben, drehen wir zufällig
                //zwei Karten um
                if (hit == false)
                {
                    //so lange eine Zufallszahl suchen, bis eine Karte
                    //gefunden wird, die noch im Spiel ist
                    do
                    {
                        random = randomNumber.Next(42);
                    } while (cards[random].IsStillinGame() == false);
                //die erste Karte umdrehen
                //die Vorderseite zeigen
                cards[random].ShowFrontSide();
                //und die Karte öffnen
                OpenCard(cards[random]);
                    //für die zweite Karte müssen wir außerdem prüfen, ob
                    //sie nicht gerade angezeigt wird
                    do
                    {
                        random = randomNumber.Next(42);

                    } while ((cards[random].IsStillinGame() == false) || (cards[random].IsUpSideDown() == true));
                    //und die zweite Karte umdrehen

                    //cards[random].RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                //die Vorderseite zeigen
                cards[random].ShowFrontSide();
                //und die karte öffnen
                OpenCard(cards[random]);
                }
            
           
        }
    }
}
