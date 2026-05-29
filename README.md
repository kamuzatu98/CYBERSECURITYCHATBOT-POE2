# 🔒 Cybersecurity Awareness Bot

A chatbot built in C# that teaches you how to stay safe online. 
It started as a console app and has been upgraded to a full 
Windows desktop app using WPF.

---

## What Does It Do?

You open the app, type your name, and then chat with the bot 
about cybersecurity topics. It gives you tips on things like 
passwords, phishing scams, malware, and more.

The bot also picks up on how you're feeling. If you say you're 
worried or confused, it responds in a supportive way before 
giving you advice.

---

## Features

-  Cyberpunk themed desktop interface (dark + neon green/blue)
-  Voice greeting when the app opens
-  Responds to cybersecurity questions with random varied tips
-  Remembers your name and favourite topic during the chat
-  Handles follow-up questions like "tell me more" or "another tip"
-  Detects your mood and adjusts its response
-  Handles unknown inputs without crashing

---

## Topics You Can Ask About

| Topic | What You'll Learn |
|---|---|
| password | How to create strong passwords |
| phishing | How to spot fake emails and links |
| scam | How to avoid online scams |
| privacy | How to protect your personal info |
| 2fa | How two-factor authentication works |
| malware | How to protect your device |
| ransomware | How to avoid ransomware attacks |
| social engineering | How attackers manipulate people |
| browsing | How to browse the web safely |

---

## How to Run It

Make sure you have the .NET 10 SDK installed.

---

## Project Structure

CybersecurityBotWPF/
├── App.xaml                 → App startup and styling
├── App.xaml.cs              → App entry point
├── MainWindow.xaml          → The UI layout
├── MainWindow.xaml.cs       → The UI logic
├── User.cs                  → Stores user info during chat
├── ResponseEngine.cs        → All the chatbot responses
├── SentimentDetector.cs     → Detects user mood
├── AudioManager.cs          → Plays the voice greeting
└── greeting.wav             → The audio file (optional)
Download it here: https://dotnet.microsoft.com/download

Then open a terminal in the project folder and run:

---

## How the Bot Works

1. App opens and plays a voice greeting
2. Bot asks for your name
3. You type questions or topics
4. Bot detects keywords and picks a random relevant tip
5. If you say you're worried or confused it responds with empathy first
6. It remembers your favourite topic and brings it up later
7. You can ask for more tips anytime by typing "tell me more"

---

## Requirements

- Windows 10 or 11
- .NET 10 SDK
- That's it

---

## Built With

- C# 13
- .NET 10
- WPF (Windows Presentation Foundation)
- System.Media.SoundPlayer for audio

---

## Author: Jeremiah Tanyaradzwa Kamuzatu

Made by [Your Name] as part of a cybersecurity awareness 
project. Feel free to use it, improve it, or build on top of it.
