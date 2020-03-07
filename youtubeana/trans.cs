using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace youtubeana
{
   public static class trans
    {
        public static string TranCategory(string category)
        {
            string report = string.Empty;
            switch (category)
            {
                case "1":
                    report = "Film&Animation";
                    break;
                case "2":
                    report = "Autos&Vehicles";
                    break;
                case "10":
                    report = "Music";
                    break;
                case "15":
                    report = "Pets&Animals";
                    break;
                case "17":
                    report = "Sports";
                    break;
                case "18":
                    report = "Short Movies";
                    break;
                case "19":
                    report = "Travel&Events";
                    break;
                case "20":
                    report = "Gaming";
                    break;
                case "21":
                    report = "Videoblogging";
                    break;
                case "22":
                    report = "People&Blogs";
                    break;
                case "23":
                    report = "Comedy";
                    break;
                case "24":
                    report = "Entertainment";
                    break;
                case "25":
                    report = "News&Politics";
                    break;
                case "26":
                    report = "Howto&Style";
                    break;
                case "27":
                    report = "Education";
                    break;
                case "28":
                    report = "Science&Technology";
                    break;
                case "29":
                    report = "Nonprofits&Activism";
                    break;
                case "30":
                    report = "Movies";
                    break;
                case "31":
                    report = "Anime/Animation";
                    break;
                case "32":
                    report = "Action/Adventure";
                    break;
                case "33":
                    report = "Classics";
                    break;
                case "34":
                    report = "Comedy";
                    break;
                case "35":
                    report = "Documentary";
                    break;
                case "36":
                    report = "Drama";
                    break;
                case "37":
                    report = "Family";
                    break;
                case "38":
                    report = "Foreign";
                    break;
                case "39":
                    report = "Horror";
                    break;
                case "40":
                    report = "Sci-Fi/Fantasy";
                    break;
                case "41":
                    report = "Thriller";
                    break;
                case "42":
                    report = "Shorts";
                    break;
                case "43":
                    report = "Shows";
                    break;
                case "44":
                    report = "Trailers";
                    break;
            }
            return report;
        }
    }
}
