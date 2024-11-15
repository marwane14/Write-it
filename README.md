## Initialisation du projet

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

---

## **1. Utiliser le script**
Téléchargez ou clonez ce projet, puis exécutez simplement le script suivant dans votre terminal :

```bash
bash setup_git.sh
```bash
