## Initialisation du projet

Naviguer dans le répertoire contenant le fichier : Si le script est dans un répertoire spécifique, se rendre dans ce répertoire avec la commande cd :

    cd chemin/versprojet

Connecter un projet local à un dépôt GitHub :

    git remote add origin [URL]

Cloner un projet depuis GitHub :

    git clone https://github.com/marwane14/Write-it.git

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

Setup_git.sh:

    Clonera le dépôt GitHub (si nécessaire).
    Passera sur la branche spécifiée par l'utilisateur.
    Ajoutera tous les fichiers modifiés pour les suivre.
    Effectuera un commit avec un message personnalisé fourni par l'utilisateur.
    Poussera les modifications vers la branche distante.
    Synchronisera le projet avec GitHub (télécharge les mises à jour et fusionne si nécessaire).
    
push_git.sh :

    Ce script ajoute tous les fichiers modifiés, demande un message pour le commit
    et pousse les changements vers la branche distante spécifiée.

pull_git.sh :

    Ce script permet de récupérer les changements à partir de la branche distante 
    et de les fusionner avec la branche locale spécifiée.
    
merge_git.sh :
    Ce script permet de fusionner une branche distante spécifiée dans la branche locale.
    Il vérifie si la branche distante est bien synchronisée avec la branche locale avant de tenter la fusion.
    Si la fusion réussit sans conflit, le script informe l'utilisateur et propose de pousser les modifications.
    Si des conflits surviennent, il avertit l'utilisateur que les conflits doivent être résolus manuellement.

## **1. Utiliser le script**

Étapes d'Exécution :

Placer les scripts dans le répertoire de votre projet.

Pour récupérer les modifications :
 
    ./setup_git.sh

Pour pousser les modifications :

    ./push_git.sh

Pour récupérer les modifications :

    ./pull_git.sh
    
Exécuter le script :

    ./merge_git.sh
