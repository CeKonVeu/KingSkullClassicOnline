# King Skull Classic Online

## Membres
Alexandre Jaquier, Stéphane Marengo, Loris Marzullo, Loïc Rosset, Géraud Silvestri

## Contexte
Comme son nom l’indique, l’application « King Skull Classic Online » permet de jouer au jeu de cartes Skull King en ligne. Cela peut s’avérer utile pour y jouer avec des joueurs à distance ou si on ne possède pas le jeu physiquement (mais préalablement acheté 🙂).
De plus, certains aspects du jeu sont parfois fastidieux, comme le calcul et le relevé des points, et pourraient être automatisés.
L’idée de base est de recréer intégralement le jeu en version informatisée, en gérant toutes les situations possibles durant une partie typique. Dans un second temps, il serait appréciable d’ajouter un menu permettant de personnaliser certaines règles et le deck de cartes.

## Description du projet
Le jeu est accessible directement depuis une page web. Le premier joueur, le créateur de la partie, peut y créer une room. Cela va générer un URL qui peut être envoyé à ses amis pour qu’ils rejoignent cette même room. Chaque joueur doit s'attribuer un nom, puis le créateur peut décider de commencer la partie.

Dans les grandes lignes, le jeu se présente comme suit : il y a 10 manches, et à chaque manche les joueurs reçoivent autant de cartes que le numéro de la manche. Donc manche 1, ils reçoivent une carte, manche 2, ils reçoivent deux cartes, et ainsi de suite. Au début de chaque manche, les joueurs parient sur le nombre de plis qu’ils pensent gagner, puis jouent une carte chacun leur tour. Celui qui joue la carte la plus haute remporte le pli, et la manche continue jusqu’à ce que toutes les cartes aient été jouées. Les valeurs et types des cartes, ainsi que la manière de compter les points sont spécifiés plus bas dans ce document.

Une fois la partie finie, le nom du gagnant est affiché et le créateur peut directement relancer une partie s’il le souhaite.

## Besoins fonctionnels
### Minimum requis
- Le nombre de joueurs est restreint de 2 à 6
- Un joueur doit pouvoir créer une room
- Les autres joueurs doivent pouvoir rejoindre cette room via un URL
- Les joueurs doivent s’identifier avec un pseudo
- Le créateur doit pouvoir lancer la partie
- Chaque joueur doit avoir un point de vue différent sur la partie (pour voir uniquement leurs cartes)
- Les joueurs doivent pouvoir voter en début de chaque manche
- Les votations doivent avoir un temps limité
- Les joueurs doivent pouvoir jouer des cartes chacun leur tour
- Les cartes doivent être grisées si elles ne peuvent pas être jouées
- Le score est calculé automatiquement et peut être affiché en tout temps
- Les joueurs peuvent afficher les règles en tout temps
- Le déroulement de la partie doit être correct et les règles respectées (voir section "Spécification du Skull King" pour la liste complète des contraintes du jeu qui doivent être gérées)
- Le joueur commençant la manche doit changer systématiquement
- Diverses informations doivent être affichées en tout temps à l’écran (voir mockup pour plus de détails)
### Contenu additionnel (si le temps le permet)
- Avant de commencer la partie, le créateur de la partie peut modifier les règles telles que :
  - Le deck de jeu (nombre de chaque type de carte)
  - Le nombre de joueurs (possibilité de dépasser la limite de 6)
  - Aux manches 5 et 10 tout le monde doit parier 0
  - Dès la manche 5, le joueur en tête doit parier 0
- Les joueurs peuvent visualiser le tableau des scores des parties précédentes
- Un système de drag and drop pour jouer une carte
- Ajout d'animations pour rendre le jeu plus attrayant
## Besoins non-fonctionnels
- Le jeu doit être fluide
- Les joueurs ne doivent pas subir de déconnexion, dans la mesure du possible
- Plusieurs parties peuvent être jouées en simultané
- L’interface de jeu doit être intuitive à utiliser

## Mockups
En annexe.

## Landing Page
En annexe.

## Méthodologie de développement
Le projet se trouve sur un repository GitHub, nous allons donc utiliser les divers outils à disposition sur cette plateforme afin d’améliorer notre processus de travail.

L’une des premières étapes du projet est de formaliser les user stories en se basant sur les fonctionnalités requises et les contraintes inhérentes au projet.

Ces users stories sont ensuite séparées en une ou plusieurs issues sur GitHub, ainsi un membre de l’équipe pourra s’attribuer une tâche à effectuer. Cependant, nous ne trouvons pas nécessaire d’avoir un kanban pour afficher l’état global du projet.

Une nouvelle branche est ainsi créée pour chaque fonctionnalité, fix, etc. réalisé par un membre de l’équipe.

Nomenclature pour les branches :
Fix : fix-....
Fonctionnalité : feat-
Documentation : doc-

Nous nous plions à la recommandation “Commit early, commit often” et lorsqu’un membre 
souhaite merge sa branche à la branche principale, il sera nécessaire que sa branche soit review et approved par un autre membre du groupe.

Une batterie de tests (unitaires, …) sont effectués sur le projet à chaque pull request avec NUnit.

Aussi, à chaque fois que le build est mis à jour, celui-ci est instantanément transféré au serveur Azure afin d’avoir toujours la dernière version de l’application à disposition.

La documentation disponible sur notre repository GitHub permet ainsi à une personne tierce d’installer notre solution en local, de posséder la marche à suivre pour qu’elle puisse contribuer au projet.

## Choix des technologies
### Backend
- C# ASP.NET
- Serveur sur Microsoft Azure
### Communication avec SignalR
- Frontend
- Blazor

## Spécifications du Skull King
### But du jeu
Le jeu se déroule en 10 manches durant lesquelles les joueurs peuvent gagner ou perdre des points. Le joueur avec le plus de points à la fin desdites manches remporte la partie.

### Distribuation des cartes
- A chaque manche, tous les joueurs recoivent le nombre de cartes défini par le numéro de la manche.
- Les cartes sont complètement mélangées entre les manches.

### Choix du pari
- A chaque manche, tous les joueurs parient sur le nombre de plis qu'ils pensent gagner. Cette valeur se situe entre 0 et le nombre de cartes en mains.
- Un délai de 10 secondes est imposé pour emettre un pari.
  - Si aucune valeur valide n'est entrée une fois le délai passé, le vote de 0 est émis par défaut.

### Règles générales
- La première carte couleur jouée définit l’atout (elle n'est pas forcément jouée par le premier joueur du pli).
- Dans le cas où plusieurs cartes spéciales de même niveau sont jouées et sont gagnantes, le joueur ayant joué une de ces dernières en premier gagne le pli.

### Informations techniques
Par défaut, le jeu comporte 66 cartes :
- 52 cartes numérotées de 1 à 13 et de 4 couleurs différentes (rouge, bleu, jaune et noir)
- 5 cartes “Escape”
- 5 cartes pirate
- 2 cartes sirène
- 1 carte “Scary Mary”
- 1 carte “Skull King”

### Puissances de cartes
De la plus forte à la moins forte :
- Skull King
  - SAUF si une Sirène est jouée dans le même pli (peu importe si avant ou après)
- Pirate ou Scary Mary (pirate)
- Sirène
- Cartes noires, de 13 à 1
- Cartes de la couleur de l'atout, de 13 à 1
- Escape, Scary Mary (escape) ou cartes d'une autre couleur que l'atout

### Cartes grisées
- Les cartes spéciales sont jouables en tout temps et ne peuvent donc pas être grisées
- S'il n'y a pas d'atout, toutes les cartes couleurs sont jouables
- S'il y a un atout :
  - Si le joueur possède des cartes de la couleur de l'atout, les cartes des autres couleurs sont grisées (noires y compris)
  - Si le joueur ne possède aucune carte de la couleur de l'atout, toutes les cartes sont jouables

### Ordre de passage
- Au début du jeu, le créateur commence à jouer.
- A chaque pli suivant, le gagnant du pli précédent commence à jouer.
- A chaque manche suivante, les joueurs commencent à jouer l’un après l’autre.

### Calcul du score
- Si on parie 0 :
  - Si on fait 0 pli, on gagne le numéro de la manche * 10
  - Si on fait 1 pli ou plus, on perd le numéro de la manche * 10
- Si on parie 1 ou plus :
  - Si on fait le bon nombre de plis, on gagne la valeur pariée * 20
  - Si on fait plus ou moins de plis, on perd le nombre de pli d’écart * 10
- Bonus (seulement si on a fait le bon nombre de plis) :
  - Si un Skull King gagne le pli, 30 points bonus sont obtenus par pirate joué
  - Si une Sirène gagne le pli, 50 points bonus sont obtenus si le Skull King est aussi joué

### Déroulement d’une manche
- Chaque joueur reçoit le même nombre de cartes tirées aléatoirement (incrémenté à chaque pli)
- Les joueurs entrent leur vote (entre 0 et le numéro de la manche), puis valident
- Lorsque tous les joueurs ont validé, chaque joueur joue l’une de ses cartes, l’une après l’autre.
- Le joueur remportant le pli est défini et peut commencer le pli suivant, jusqu’à ce qu’il n’y ait plus de cartes.
- Les points sont finalement ajoutés ou retirés à chaque joueur en fonction de leur vote et des plis remportés.
- Après 10 manches, le joueur ayant le plus de points remporte la partie

## Déploiement et pipeline de livraison

### Outils utilisés
- GitHub
- GitHub Actions
- Microsoft Azure

L'application est accessible sur [kingskullclassiconline.azurewebsites.net](https://kingskullclassiconline.azurewebsites.net/)

### Mise en place de l'environnement de déploiement
Lors de la mise à jour de l'application en production, et donc après avoir merge une pull request sur la branche main, un premier job est démarré sur github. Il va effectuer diverses github actions.

1. Build le projet avec une commande dotnet afin d'obtenir une représentation intermédiaire du code (web assembly, binaire et exécutable). 
2. Utiliser la commande Publish de dotnet pour publier l’application et ses dépendances dans un dossier pour le déploiement sur un système d’hébergement.
3. Upload les artefacts afin de partager le projet publier avec le prochain job.

Ensuite un autre job s'occupant du transfert de la solution sur le serveur de production Azure est démarré.

1. Récupération des fichiers du job précédent avec download artifact
2. Déploiement sur Azure Web app

Ce workflow est mit à disposition par Azure.

Pour plus d'informations :

https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build

https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish

https://github.com/actions/upload-artifact

https://github.com/actions/download-artifact
