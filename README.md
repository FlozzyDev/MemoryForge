# Overview - MemoryForge

MemoryForge is a C# application that allows users to import datasets that they can then test their memorization knowledge of. Items in the dataset are represented in a graphic interface as cards and a user must select the appropriate pair to match them together. The intention of MemoryForge is to create a fast paced game that allows users to quickly jump in and test their skill with little setup.

I chose to make MemoryForge as I had a variety of information I wanted to memorize, but I couldn't find many tools that I found useful. I found that I enjoyed this game method when using a similar application on Quizzlet, but I did not like the flow of the game. So I decided to make my own!

{Provide a link to your YouTube demonstration. It should be a 4-5 minute demo of the software running and a walkthrough of the code. Focus should be on sharing what you learned about the language syntax.}

[Memory Forge Video](https://www.youtube.com/watch?v=tUT6BvJYvjo)

# Development Environment

I used Visual Studio Code along with a host of extensions, such as C#, C# Dev Kit, ESLint, Spellchecker, and Prettier.

For this project I decided to use C# along with a very fun open source development library called Raylib_cs. Raylib originally was developed for C, but has since been ported over to a variety of languages including C#. It's helpful in creating graphic interfaces as well as making it easy to import audio and visual effects, making it ideal for making games. I chose C# as I enjoy the language structure and wanted to get a bit more experience with a more simple library before I move on to Unity.

# Useful Websites

- [Github - Raylib Repo](https://github.com/raylib-cs/raylib-cs)
- [Microsoft - C# reference guide](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/)
- [C# Programming Yellow Book](https://static1.squarespace.com/static/5019271be4b0807297e8f404/t/5824ad58f7e0ab31fc216843/1478798685347/CSharp+Book+2016+Rob+Miles+8.2.pdf)
- [Youtube - Video of a similar project](https://www.youtube.com/watch?v=frDKP-A71W0) - This is very different (in visual studio using forms) but useful to watch
- [Youtube - Unity Project](https://www.youtube.com/watch?v=J2mja7s4SFg&list=PLZhNP5qJ2IA2DA4bzDyxFMs8yogVQSrjW&index=7)

# Future Work

{Make a list of things that you need to fix, improve, and add in the future.}

- Add additional Datasets
- Add additional game types, such as speed tests and different memory formats (maps or fill in the blank)
- Provide users with a format they can then fill out and upload as a dataset
- Add an abstract Card class that allows me to make difference card types for a more dynamic gameplay (bonus points or something like that)
