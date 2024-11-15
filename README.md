Guide général des commandes Git

Ce guide liste les commandes Git essentielles, adaptées à tout projet et à toute branche.
1. Initialisation du projet

    Connecter un projet local à un dépôt GitHub :

git remote add origin [URL]

(Remplace [URL] par l'URL du dépôt GitHub.)

Cloner un projet depuis GitHub (si ce n'est pas encore fait) :

    git clone [URL]

2. Gestion des branches

    Passer sur une branche spécifique (ou la créer si elle n'existe pas) :

git checkout -b [nom_de_la_branche]

Vérifier les branches locales et distantes :

git branch -a

Revenir sur une branche existante :

    git checkout [nom_de_la_branche]

3. Suivi des fichiers

    Ajouter tous les fichiers pour le suivi dans Git :

git add .

Enregistrer les modifications avec un message descriptif :

    git commit -m "Votre message ici"

4. Envoyer les modifications sur GitHub

    Pousser les changements locaux vers une branche distante et définir l'upstream :

git push -u origin [nom_de_la_branche]

Pousser les modifications sans définir l'upstream :

    git push origin [nom_de_la_branche]

5. Synchronisation avec GitHub

    Télécharger les mises à jour sans les fusionner :

git fetch

Télécharger et fusionner directement les modifications distantes dans la branche locale :

    git pull origin [nom_de_la_branche]

6. Récapitulatif pour enregistrer et pousser un projet

    Ajouter tous les fichiers :

git add .

Faire un commit avec un message :

git commit -m "Votre message ici"

Pousser les modifications vers une branche distante :

git push origin [nom_de_la_branche]
