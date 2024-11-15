Guide général des commandes Git

Ce guide contient toutes les commandes essentielles pour collaborer efficacement sur ce projet.
1. Initialisation du projet
Connecter un projet local à un dépôt GitHub

git remote add origin [URL]

Cloner un projet depuis GitHub

git clone [URL]

2. Gestion des branches
Créer et passer sur une branche spécifique

git checkout -b [nom_de_la_branche]

Vérifier les branches locales et distantes

git branch -a

Passer sur une branche existante

git checkout [nom_de_la_branche]

3. Suivi des fichiers
Ajouter tous les fichiers pour le suivi

git add .

Enregistrer les modifications avec un message

git commit -m "Votre message ici"

4. Envoyer les modifications sur GitHub
Pousser les changements locaux vers une branche distante et définir l'upstream

git push -u origin [nom_de_la_branche]

Pousser les modifications sans définir l'upstream

git push origin [nom_de_la_branche]

5. Synchronisation avec GitHub
Télécharger les mises à jour sans les fusionner

git fetch

Télécharger et fusionner directement les modifications distantes

git pull origin [nom_de_la_branche]

6. Récapitulatif pour enregistrer et pousser un projet
Ajouter tous les fichiers

git add .

Faire un commit avec un message

git commit -m "Votre message ici"

Pousser les modifications

git push origin [nom_de_la_branche]

Astuce : Les commandes entre les blocs de code bash s'afficheront sur GitHub avec un bouton "Copy" pour simplifier le travail de ton équipe. 😊
