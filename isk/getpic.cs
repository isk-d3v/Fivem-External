using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using isk;
using KeyAuth;
using Newtonsoft.Json.Linq;

namespace mydick
{
    internal class getpic
    {
        private const string DiscordApiBaseUrl = "https://discord.com/api/v9/users/";
        private const string BotToken = "";
        public string GetDiscordUserIdFromKeyAuth()
        {
            return Form2.KeyAuthApp.user_data.username;
        }

        public async Task<string> GetDiscordProfilePictureUrl()
        {
            string userId = GetDiscordUserIdFromKeyAuth();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bot {BotToken}");
                    client.DefaultRequestHeaders.Add("User-Agent", "DiscordProfilePictureViewer");
                    string url = DiscordApiBaseUrl + userId;
                    string jsonResponse = await client.GetStringAsync(url);

                    JObject discordUser = JObject.Parse(jsonResponse);
                    string avatarId = discordUser["avatar"]?.ToString();
                    string discriminator = discordUser["discriminator"]?.ToString();

                    if (!string.IsNullOrEmpty(avatarId))
                    {
                        return $"https://cdn.discordapp.com/avatars/{userId}/{avatarId}.png?size=512";
                    }
                    else
                    {
                        string defaultAvatarId = (Convert.ToInt32(discriminator) % 5).ToString();
                        return $"https://cdn.discordapp.com/embed/avatars/{defaultAvatarId}.png";
                    }
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }

        public async Task<System.Drawing.Image> LoadImageFromUrl(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] imageBytes = await client.GetByteArrayAsync(url);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        return System.Drawing.Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
