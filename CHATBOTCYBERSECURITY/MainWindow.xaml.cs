using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CybersecurityBotWPF
{
    public partial class MainWindow : Window
    {
        private User? _user;
        private bool _nameEntered = false;

        // Brushes
        private readonly SolidColorBrush _neonGreen = new(Color.FromRgb(0, 255, 128));
        private readonly SolidColorBrush _neonBlue = new(Color.FromRgb(0, 200, 255));
        private readonly SolidColorBrush _neonPurple = new(Color.FromRgb(180, 0, 255));
        private readonly SolidColorBrush _dimText = new(Color.FromRgb(100, 100, 120));
        private readonly SolidColorBrush _white = new(Color.FromRgb(255, 255, 255));

        // Delegate for processing messages
        private delegate string MessageProcessor(string input, User user);
        private readonly MessageProcessor _processMessage = ResponseEngine.GetResponse;

        public MainWindow()
        {
            InitializeComponent();
            AudioManager.PlayGreeting();
            ShowWelcome();
            InputBox.Focus();
        }

        private void ShowWelcome()
        {
            AppendDivider();
            AppendMessage("Bot", "Hello! Welcome to the Cybersecurity Awareness Bot. 🔒", _neonGreen);
            AppendMessage("Bot", "I am here to help you stay safe online.", _neonGreen);
            AppendMessage("Bot", "What is your name?", _neonGreen);
            AppendDivider();
        }

        private void HandleInput()
        {
            string input = InputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            InputBox.Clear();

            // First input is the user's name
            if (!_nameEntered)
            {
                _user = new User(input);
                _nameEntered = true;
                AppendMessage(input, input, _neonBlue);
                AppendDivider();
                AppendMessage("Bot", $"Nice to meet you, {_user.Name}! 👋", _neonGreen);
                AppendMessage("Bot",
                    "Type 'help' to see topics, or just ask me anything about cybersecurity!",
                    _neonGreen);
                AppendDivider();
                StatusLabel.Text = $"● ONLINE  |  Chatting as {_user.Name}";
                return;
            }

            // Show user message
            AppendMessage(_user!.Name, input, _neonBlue);

            // Sentiment detection
            var sentiment = SentimentDetector.Detect(input);
            string sentimentPrefix = SentimentDetector.GetSentimentPrefix(sentiment);
            if (!string.IsNullOrEmpty(sentimentPrefix))
                AppendMessage("Bot", sentimentPrefix, _neonGreen);

            // Process response via delegate
            string response = _processMessage(input, _user);
            _user.SessionQuestions++;

            if (!string.IsNullOrEmpty(response))
                AppendMessage("Bot", response, _neonGreen);
            else
                AppendMessage("Bot",
                    "I am not sure I understand. Could you try rephrasing? Type 'help' to see available topics.",
                    _neonGreen);
        }

        private void AppendMessage(string sender, string message, SolidColorBrush color)
        {
            var para = new Paragraph { Margin = new Thickness(0, 2, 0, 6) };

            // Timestamp
            para.Inlines.Add(new Run($"[{DateTime.Now:HH:mm}] ")
            {
                Foreground = _dimText,
                FontSize = 9
            });

            // Sender name
            para.Inlines.Add(new Run($"{sender}: ")
            {
                Foreground = color,
                FontWeight = FontWeights.Bold,
                FontSize = 11
            });

            // Message body
            para.Inlines.Add(new Run(message)
            {
                Foreground = _white,
                FontSize = 11
            });

            ChatBox.Document.Blocks.Add(para);
            ChatScrollViewer.ScrollToBottom();
        }

        private void AppendDivider()
        {
            var para = new Paragraph { Margin = new Thickness(0, 2, 0, 2) };
            para.Inlines.Add(new Run("══════════════════════════════════════════════════════════")
            {
                Foreground = _neonPurple,
                FontSize = 10
            });
            ChatBox.Document.Blocks.Add(para);
            ChatScrollViewer.ScrollToBottom();
        }

        // Event handlers
        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                HandleInput();
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e) => HandleInput();

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            if (_user != null)
                AppendMessage("Bot", ResponseEngine.GetResponse("help", _user), _neonGreen);
            else
                AppendMessage("Bot", "Please enter your name first!", _neonGreen);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ChatBox.Document.Blocks.Clear();
            ShowWelcome();
        }
    }
}
