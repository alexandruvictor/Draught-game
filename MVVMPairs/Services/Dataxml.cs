using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace MVVMPairs.Services
{
    class Dataxml
    {



        public void AdaugareSaveGame(int[,] board)
        {
            XDocument doc = XDocument.Load("saveGame.xml");
            XElement root = new XElement("Score");

            for (int index = 0; index < 8; index++)
            {
                for (int index2 = 0; index2 < 8; index2++)
                {
                    string i = "A" + index.ToString() + index2.ToString();
                    int nr;

                    root.Add(new XAttribute(i, board[index, index2].ToString()));
                }
            }
            doc.Element("SaveGame").Add(root);
            doc.Save("saveGame.xml");
        }

        public void ModificareStatistics(int player)
        {
            string word;
            if (player == 1)
                word = "Player1";
            else
                word = "Player2";

            XDocument doc = XDocument.Load("saveData.xml");
            var q = from node in doc.Descendants("Score")
                    
                    select node;

            int nr = Int32.Parse(q.ToList().ElementAt(0).Attribute(word).Value)+1;
           
            q.ToList().ElementAt(0).Attribute(word).Value =nr.ToString() ;
            doc.Save("saveData.xml");

            string player1 = q.ToList().ElementAt(0).Attribute("Player1").Value;
            string player2 = q.ToList().ElementAt(0).Attribute("Player2").Value;

            MessageBox.Show("SCORE:\nPlayer1:"+ player1 + "\nPlayer2:" +player2);
        }


        public void AfisareStatics()
        {
            XDocument doc = XDocument.Load("saveData.xml");
            
            var q = from node in doc.Descendants("Score")
                    select node;
            string player1 = q.ToList().ElementAt(0).Attribute("Player1").Value;
            string player2 = q.ToList().ElementAt(0).Attribute("Player2").Value;

            MessageBox.Show("SCORE:\nPlayer1:" + player1 + "\nPlayer2:" + player2);
        }

    }
}
