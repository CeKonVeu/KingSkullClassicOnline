# KingSkullClassicOnline

### Auteurs

[Stéphane](https://github.com/BernardLhermite),
[Géraud](https://github.com/GeraudSilvestri),
[Loris](https://github.com/Loris199),
[Alexandre](https://github.com/AJaquier),
[Loïc](https://github.com/loicrheig)

## Introduction

Ce projet a été réalisé pour le cours PDG de l'HES d'été 2022.
Le but était de réaliser un projet libre de A à Z, en utilisant un pipeline CI/CD et de présenter notre résultat final.
Nous avons donc décidé de réaliser une adaptation en ligne d'un jeu de cartes dont nous sommes habitués, le [Skull King](https://www.schmidtspiele.de/files/Produkte/7/75024%20-%20Skull%20King/75024_Skull_King_DE_FR_IT_GB.pdf#page=3).

## Installation du projet

Ce projet est composé de 5 sous-programmes :

- Client qui gère les interactions avec l'utilisateur et contient le design des pages
- Server qui sert de WebAPI. Cette partie va résoudre la communication entre les différents utilisateurs d'une room.
- Engine qui est une librairie contenant toute la logique du jeu SkullKing.
- Engine.Tests contenant les tests unitaires.
- Shared qui est une librairie contenant des éléments partagés dans les différents sous-programmes.

### Prérequis

Pour compiler le projet, il est nécessaire d'installer le [SDK .NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0). Après son installation, un redémarrage peut être requis.

Une fois l'installation terminée, vérifiez que `dotnet` ait bien été ajouté à la variable `path`: [Voir la marche à suivre sous Windows](https://github.com/dotnet/sdk/issues/7428#issuecomment-270423072)

Facultatif : IDE ([Rider](https://www.jetbrains.com/fr-fr/rider/))

### Compilation & Lancement

#### En ligne de commande :

Depuis la racine du projet, lancez la commande :

`dotnet run --project ./src/Server`

Ceci aura pour effet de télécharger les dépendances requises, compiler les différents sous-programmes et lancer l'application.

#### Depuis RIDER : 

Après avoir cloné ce projet, celui-ci peut être exécuté en sélectionnant la configuration de run Server puis en pressant sur le bouton run.

![Rider run](./doc/ressources/readme/run-config.png)

#### Accès

L'application devrait être accessible sur [https://localhost:7114/](https://localhost:7114/). Notez que le port peut varier.

### Liste non exhaustive des dépendances

- Microsoft.AspNetCore.SignalR.Client
- MudBlazor
- NUnit
- LibSassBuilder

### Erreurs 

#### Port déjà utilisé

![Rider run](./doc/ressources/readme/address-error.png)

Cette erreur indique que le port défini dans la configuration est en cours d'utilisation. Pour corriger cela, libérez ou changez le port dans les fichiers [lauchSettings.json (Client)](https://github.com/CeKonVeu/KingSkullClassicOnline/blob/main/src/Client/Properties/launchSettings.json) et [lauchSettings.json (Server)](https://github.com/CeKonVeu/KingSkullClassicOnline/blob/main/src/Server/Properties/launchSettings.json) dans la partie `applicationUrl`.
