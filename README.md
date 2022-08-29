# KingSkullClassicOnline

### Auteurs

## Introduction

## Installation du projet

Ce projet est composé de 4 sous-programmes :

- /src/Client qui gère les interactions avec l'utilisateur.
- /src/Server qui sert de WebAPI. Cette partie va résoudre la communication entre les différents utilisateurs d'une room.
- /src/Engine qui est une librairie contenant toute la logique du jeu SkullKing.
- /src/Engine.Tests contenant les tests unitaires.

En ne faisant que cloner le repository, il n'est possible de faire tourner l'application qu'en local.

Afin de rendre l'accès au jeu publique, il est nécessaire de la faire tournée sur un serveur web.

### Mise en place du serveur

Cette partie ne traite que du cas où la partie Client et Server sont installés sur le même serveur.

#### Prérequis

- [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- 

#### Marche à suivre

Si vous ne possédez pas encore de compte [Azure](https://azure.microsoft.com/fr-fr/), vous devez vous en créer un.

Ensuite,