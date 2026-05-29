namespace CybersecurityBotWPF
{
    public static class ResponseEngine
    {
        private static readonly Random Rng = new();
        private static string _lastTopic = string.Empty;

        private static readonly Dictionary<string, List<string>> RandomResponses = new()
        {
            ["phishing"] = new List<string>
            {
                "🎣 Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
                "🎣 Always hover over links before clicking. The real URL might be very different from what is displayed.",
                "🎣 Legitimate companies will never ask for your password via email. Always verify the sender address carefully.",
                "🎣 Watch out for urgent language like 'Act now!' or 'Your account will be suspended!' — these are classic phishing tactics."
            },
            ["password"] = new List<string>
            {
                "🔑 Use at least 12 characters mixing letters, numbers and symbols. The longer the better!",
                "🔑 Never reuse passwords across multiple sites. If one gets hacked, all your accounts are at risk.",
                "🔑 Use a password manager like Bitwarden or 1Password to generate and store strong passwords safely.",
                "🔑 Avoid using personal info like birthdays or names in your passwords — hackers can guess these easily."
            },
            ["scam"] = new List<string>
            {
                "🚨 If something feels too good to be true online, it probably is. Always verify before clicking or paying.",
                "🚨 Never send money to someone you have not met in person, even if they seem trustworthy online.",
                "🚨 Scammers often create fake urgency. Take your time and verify the source before acting.",
                "🚨 Research any unknown company or person before sharing personal or financial details."
            },
            ["privacy"] = new List<string>
            {
                "🔏 Review your social media privacy settings regularly. Limit who can see your personal information.",
                "🔏 Be mindful of what you share publicly online — even small details can be used against you.",
                "🔏 Use a VPN when browsing on public Wi-Fi to keep your data private.",
                "🔏 Read privacy policies before signing up to new services to understand how your data is used."
            },
            ["malware"] = new List<string>
            {
                "🦠 Keep your operating system and software updated — patches fix security vulnerabilities.",
                "🦠 Only download software from trusted, official sources. Avoid cracked or pirated software.",
                "🦠 Use reputable antivirus software and keep it updated at all times.",
                "🦠 Be careful what you plug into your computer — even USB drives can carry malware."
            },
            ["ransomware"] = new List<string>
            {
                "💀 Back up your data regularly to an external drive or cloud storage. Ransomware cannot touch backups.",
                "💀 Never open unexpected email attachments, even from people you know — their account may be compromised.",
                "💀 Keep all your software patched and updated to close security loopholes ransomware exploits.",
                "💀 If infected, do NOT pay the ransom. Contact cybersecurity professionals for help instead."
            },
            ["2fa"] = new List<string>
            {
                "🔐 Enable 2FA on all your important accounts. It adds a second layer of protection beyond your password.",
                "🔐 Use an authenticator app like Google Authenticator or Authy instead of SMS for stronger 2FA.",
                "🔐 Even if someone has your password, 2FA stops them from getting in without your device.",
                "🔐 Most major platforms support 2FA — check your security settings today and enable it everywhere."
            },
            ["two-factor"] = new List<string>
            {
                "🔐 Two-factor authentication means even a stolen password will not let attackers in without your second device.",
                "🔐 Set up 2FA on your email first — it is the master key to all your other accounts.",
                "🔐 Authenticator apps are safer than SMS codes, which can be intercepted via SIM swapping.",
                "🔐 Backup codes are important — store them safely in case you lose access to your authenticator app."
            },
            ["social engineering"] = new List<string>
            {
                "🎭 Be suspicious of unexpected requests for sensitive information, even from seemingly trusted sources.",
                "🎭 Always verify identities before sharing data — call back on official numbers to confirm.",
                "🎭 Attackers exploit emotions like fear and urgency. Pause and think before acting on any pressure.",
                "🎭 Train yourself to recognise manipulation tactics — knowledge is your best defence."
            },
            ["browsing"] = new List<string>
            {
                "🌐 Always look for HTTPS in the address bar before entering any personal or payment information.",
                "🌐 Avoid using public Wi-Fi for sensitive tasks like banking. Use a VPN if you must.",
                "🌐 Clear your browser cookies and cache regularly to remove tracking data.",
                "🌐 Use a privacy-focused browser like Firefox or Brave for better protection online."
            }
        };

        private static readonly Dictionary<string, string> FixedResponses = new()
        {
            ["how are you"] =
                "I am running smoothly and fully patched! 😄 Ready to help you stay secure online.",
            ["your purpose"] =
                "My purpose is to raise cybersecurity awareness! Ask me about passwords, phishing, scams, privacy, and more.",
            ["what can i ask"] =
                "You can ask me about:\n  • Password safety\n  • Phishing\n  • Scams\n  • Privacy\n  • 2FA / Two-Factor Authentication\n  • Malware\n  • Ransomware\n  • Social Engineering\n  • Safe Browsing",
            ["hello"] =
                "Hello! 👋 Great to see you here. Type 'help' to see what topics I can help you with.",
            ["hi"] =
                "Hi there! 👋 Ready to help you stay safe online. Type 'help' to get started.",
            ["thank"] =
                "You are welcome! 😊 Stay safe out there. Feel free to ask anything else.",
            ["help"] =
                "📋 Topics you can ask about:\n" +
                "  🔑  password\n" +
                "  🎣  phishing\n" +
                "  🚨  scam\n" +
                "  🔏  privacy\n" +
                "  🔐  2fa / two-factor\n" +
                "  🦠  malware\n" +
                "  💀  ransomware\n" +
                "  🎭  social engineering\n" +
                "  🌐  browsing"
        };

        public static string GetResponse(string input, User user)
        {
            string lower = input.ToLower().Trim();

            // Follow-up handling
            if (lower.Contains("another tip") || lower.Contains("tell me more") ||
                lower.Contains("explain more") || lower.Contains("more info") ||
                lower.Contains("give me more"))
            {
                if (!string.IsNullOrEmpty(_lastTopic) && RandomResponses.ContainsKey(_lastTopic))
                    return $"Here is another tip on {_lastTopic}:\n\n" +
                           RandomResponses[_lastTopic][Rng.Next(RandomResponses[_lastTopic].Count)];

                if (!string.IsNullOrEmpty(user.FavouriteTopic) &&
                    RandomResponses.ContainsKey(user.FavouriteTopic))
                    return $"Since you are interested in {user.FavouriteTopic}, here is a tip:\n\n" +
                           RandomResponses[user.FavouriteTopic][Rng.Next(RandomResponses[user.FavouriteTopic].Count)];
            }

            // Memory recall
            if (lower.Contains("what do you remember") ||
                lower.Contains("what do you know about me"))
            {
                string memory = $"Here is what I remember about you, {user.Name}:\n";
                memory += $"  • You have asked {user.SessionQuestions} question(s) this session.\n";
                if (!string.IsNullOrEmpty(user.FavouriteTopic))
                    memory += $"  • Your favourite topic is: {user.FavouriteTopic}.";
                return memory;
            }

            // Interest memory
            if (lower.Contains("i'm interested in") || lower.Contains("i am interested in"))
            {
                foreach (var topic in RandomResponses.Keys)
                {
                    if (lower.Contains(topic))
                    {
                        user.FavouriteTopic = topic;
                        _lastTopic = topic;
                        return $"Great! I will remember that you are interested in {topic}. 🧠\n\n" +
                               $"Here is a tip to get you started:\n\n" +
                               RandomResponses[topic][Rng.Next(RandomResponses[topic].Count)];
                    }
                }
            }

            // Fixed responses
            foreach (var entry in FixedResponses)
            {
                if (lower.Contains(entry.Key))
                    return entry.Value;
            }

            // Random keyword responses
            foreach (var entry in RandomResponses)
            {
                if (lower.Contains(entry.Key))
                {
                    _lastTopic = entry.Key;
                    string prefix = !string.IsNullOrEmpty(user.FavouriteTopic) &&
                                    user.FavouriteTopic == entry.Key
                        ? $"As someone interested in {user.FavouriteTopic}, here is a tip:\n\n"
                        : string.Empty;

                    return prefix + entry.Value[Rng.Next(entry.Value.Count)];
                }
            }

            return string.Empty;
        }
    }
}
