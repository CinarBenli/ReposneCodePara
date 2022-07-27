using Rocket.API;

namespace ReposneCodePara
{
    public class Config : IRocketPluginConfiguration
    {
        public bool Uconomy;
        public string Logo;
        public void LoadDefaults()
        {
            Uconomy = false;
            Logo = "https://cdn.discordapp.com/attachments/949058399155400824/950704886813712434/gfhjjf.png";
        }
    }
}