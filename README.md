## Initialisation du projet

Naviguer dans le répertoire contenant le fichier : Si le script est dans un répertoire spécifique, se rendre dans ce répertoire avec la commande cd :

    cd chemin/vers/le/script

Connecter un projet local à un dépôt GitHub :

    git remote add origin [URL]

Cloner un projet depuis GitHub :

    git clone [URL]

## Gestion des branches

Créer et passer sur une branche spécifique :

    git checkout -b [nom_de_la_branche]

Vérifier les branches locales et distantes :

    git branch -a

Passer sur une branche existante :

    git checkout [nom_de_la_branche]

## Suivi des fichiers

Ajouter tous les fichiers pour le suivi :

    git add .

Enregistrer les modifications avec un message :

    git commit -m "Votre message ici"

## Envoyer les modifications sur GitHub

Pousser les changements locaux vers une branche distante et définir l'upstream :

    git push -u origin [nom_de_la_branche]

Pousser les modifications sans définir l'upstream :

    git push origin [nom_de_la_branche]

## Synchronisation avec GitHub

Télécharger les mises à jour sans les fusionner :

    git fetch

Télécharger et fusionner directement les modifications distantes :

    git pull origin [nom_de_la_branche]

# Guide Git - Automatisation des commandes

Pour simplifier le processus, vous pouvez exécuter un script qui configure automatiquement Git pour vous.

Le script effectuera les actions suivantes :

    Clonera le dépôt GitHub (si nécessaire).
    Passera sur la branche spécifiée par l'utilisateur.
    Ajoutera tous les fichiers modifiés pour les suivre.
    Effectuera un commit avec un message personnalisé fourni par l'utilisateur.
    Poussera les modifications vers la branche distante.
    Synchronisera le projet avec GitHub (télécharge les mises à jour et fusionne si nécessaire).

## **1. Utiliser le script**

Naviguer dans le répertoire contenant le fichier : Si le script est dans un répertoire spécifique, ils doivent se rendre dans ce répertoire avec la commande cd :

    cd chemin/vers/le/script

Rendre le script exécutable (ils doivent le faire une seule fois) :

    chmod +x setup_git.sh

Exécuter le script : Ensuite, ils peuvent exécuter le script avec la commande suivante :

    ./setup_git.sh

