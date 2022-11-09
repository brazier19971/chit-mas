using DiscordMessenger;

namespace CHIT_MAS
{
    public class DiscordMsg
    {
        static string profileLink;
        public static void SendMsg(string artist, string station, string track, string severity, string stationImage,string artistImage)
        {
            if (severity == "16776960") { profileLink = "<Insert Image Link>"; } //set yellow
            if (severity == "15105570") { profileLink = "<Insert Image Link>"; } //set amber
            if (severity == "15548997") { profileLink = "<Insert Image Link>"; } //set red
            new DiscordMessage()
                .SetUsername("MAS Test Message....")
                .SetAvatar(profileLink)

                .AddEmbed()
                    .SetThumbnail(stationImage)
                    .SetTimestamp(DateTime.Now)
                    .SetTitle(track)
                    .SetAuthor(artist, artistImage, artistImage)
                    .SetColor(Int32.Parse(severity))
                    .SetDescription("Now playing on " + station)
                    .SetFooter("Powered by CHIT MAS, Version 2.6")
                .Build()
                .SendMessage("<Discord Webhook>");


            //Only send a message to the general channel if it's red.
            if (severity == "15548997")
            {
                new DiscordMessage()
               .SetUsername("MAS Test Message....")
               .SetContent("Please see the MAS Channel for all alerts.")
               .SetAvatar(profileLink)

               .AddEmbed()
                   .SetThumbnail(stationImage)
                   .SetTimestamp(DateTime.Now)
                   .SetTitle(track)
                   .SetAuthor(artist, artistImage, artistImage)
                   .SetColor(Int32.Parse(severity))
                   .SetDescription("Now playing on " + station)
                   .SetFooter("Powered by CHIT MAS, Version 2.6")
               .Build()
               .SendMessage("<Discord Webhook>");
            } //set red
        }
    }
}
