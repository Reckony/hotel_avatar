using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class AvatarHelper
    {
        public Models.Action handleRequest(string message)
        {
            var culture = CultureInfo.InvariantCulture;
            if (culture.CompareInfo.IndexOf(message, "jump", CompareOptions.IgnoreCase) >= 0)
            {
                return Models.Action.Jump;
            }
            //to do: wywalić listę słów "na zewnątrz"
            List<string> greetingsWords = new List<string> { "how are you", "what's" };
            List<string> greetingsWords2 = new List<string> { "hi", "hey", "hello", "good morning", "good afternoon" };
            List<string> helpWords = new List<string> { "help" };
            List<string> mapWords = new List<string> { "map", "location", "direction" };
            List<string> resetWords = new List<string> { "back", "thanks", "escape" };
            List<string> offertWords = new List<string> { "offert" };
            List<string> roomWords = new List<string> { "room" };
            List<string> galleryWords = new List<string> { "gallery", "photos", "pictures" };
            List<string> reviewsWords = new List<string> { "review", "opinion" };
            List<string> foodWords = new List<string> { "menu", "food", "eat" };

            //to do: ogarnąć ten kod
            var result = resetWords.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.Reset;
            }

            result = offertWords.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.ShowOffert;
            }

            result = foodWords.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.ShowMenu;
            }

            result = reviewsWords.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.ShowReviews;
            }

            result = roomWords.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.ShowRooms;
            }

            result = galleryWords.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.ShowGallery;
            }

            result = mapWords.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.ShowMap;
            }

            result = greetingsWords.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0) &&
                greetingsWords2.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.GreetWithAnswer;
            }

            result = greetingsWords.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.OnlyAnswer;
            }

            result = greetingsWords2.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.GreetWithoutAnswer;
            }

            result = helpWords.Any(w => culture.CompareInfo.IndexOf(message, w, CompareOptions.IgnoreCase) >= 0);
            if (result)
            {
                return Models.Action.OfferingHelp;
            }

            return Models.Action.None;
        }
    }
}
