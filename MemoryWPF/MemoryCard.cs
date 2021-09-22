using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MemoryWPF
{
   
    class MemoryCard : Button
    {
        MemoryField game;
        
        int pictureID;
        
        Image pictureFront, pictureBack;
        
        int picturePos;
       
        bool upsideDown;
       
        bool stillInGame;
        
        public MemoryCard(string front, int pictureID,MemoryField game)
        {
            
           
            pictureFront = new Image();
            
            pictureFront.Source = new BitmapImage(new Uri("pictures/" + front, UriKind.Relative));
          
            pictureBack = new Image();
            
            pictureBack.Source = new BitmapImage(new Uri("pictures/verdeckt.bmp", UriKind.Relative));
           
            Content = pictureBack;
           
            this.pictureID = pictureID;
           
            upsideDown = false;
            stillInGame = true;
            
            this.game = game;
           
            Click += new RoutedEventHandler(ButtonClick);
        }

       
        public void ShowFrontSide()
        {
            Content = pictureFront ;
            upsideDown = true;
        }

        
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
           
            if (stillInGame == false || (game.PermittedMove() == false))
                return;
           
            if (upsideDown == false)
            {
                ShowFrontSide();
               
                game.OpenCard(this);
            }
        }
        
        public bool IsUpSideDown()
        {
            return upsideDown;
        }
      
        public bool IsStillinGame()
        {
            return stillInGame;
        }

       
        public void ShowBackside(bool takeOut)
        {
           
            if (takeOut == true)
            {
                
                Image takeOutPicture = new Image();
            

            takeOutPicture.Source = new BitmapImage(new Uri("pictures/aufgedeckt.bmp", UriKind.Relative));
            Content = takeOutPicture;
            stillInGame = false;
            }
            else
            {
              
              Content = pictureBack;
               upsideDown = false;
            }
        }

      
      public int GetPictureID()
      {
        return pictureID;
      }
     
      public int GetPicturePos()
      {
        return picturePos;
      }
       
      public void SetPicturePos(int picturePos)
      {    
       this.picturePos = picturePos;
      }
   }
}
