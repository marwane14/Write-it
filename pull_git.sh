#!/bin/bash

echo "Automatisation de la commande git pull en cours..."

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
read -p "Entrez le nom de la branche locale à mettre à jour : " LOCAL_BRANCH
git checkout $LOCAL_BRANCH

# Synchroniser avec GitHub : récupérer les mises à jour et fusionner
git pull origin $LOCAL_BRANCH

echo "Pull effectué avec succès ! 😊"
