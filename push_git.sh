#!/bin/bash

echo "Automatisation de la commande git push en cours..."

# Demande l'URL du dépôt
read -p "Entrez l'URL de votre dépôt GitHub : " REPO_URL

# Vérifier si le dossier existe déjà
if [ ! -d "$(basename $REPO_URL .git)" ]; then
  echo "Le dépôt n'existe pas localement. Veuillez d'abord cloner le projet."
  exit 1
fi

# Passer dans le répertoire du projet
cd "$(basename $REPO_URL .git)"

# Vérifier que la branche locale existe
read -p "Entrez le nom de la branche locale à pousser : " LOCAL_BRANCH
git checkout $LOCAL_BRANCH

# Ajouter tous les fichiers
git add .

# Demander un message de commit
read -p "Entrez le message du commit : " COMMIT_MSG
git commit -m "$COMMIT_MSG"

# Pousser les modifications vers GitHub
git push origin $LOCAL_BRANCH

echo "Push effectué avec succès ! 😊"
