#!/bin/bash

echo "Automatisation de la commande git push en cours..."

# URL du dépôt GitHub (fixe dans le script)
REPO_URL="https://github.com/marwane14/Write-it.git"

# Vérifier si le dossier existe déjà
REPO_NAME=$(basename "$REPO_URL" .git)

if [ ! -d "$REPO_NAME" ]; then
  echo "Le dépôt '$REPO_NAME' n'existe pas localement. Veuillez d'abord cloner le projet."
  exit 1
fi

# Passer dans le répertoire du projet
cd "$REPO_NAME" || { echo "Impossible d'accéder au répertoire '$REPO_NAME'."; exit 1; }

# Vérifier que la branche locale existe
read -p "Entrez le nom de la branche locale à pousser : " LOCAL_BRANCH
if ! git rev-parse --verify "$LOCAL_BRANCH" &>/dev/null; then
  echo "La branche '$LOCAL_BRANCH' n'existe pas localement."
  exit 1
fi

# Passer sur la branche locale
git checkout "$LOCAL_BRANCH" || { echo "Impossible de passer sur la branche '$LOCAL_BRANCH'."; exit 1; }

# Ajouter tous les fichiers
git add .

# Demander un message de commit
read -p "Entrez le message du commit : " COMMIT_MSG
if ! git commit -m "$COMMIT_MSG"; then
  echo "Aucun changement à valider. Commit ignoré."
fi

# Pousser les modifications vers GitHub
if ! git push origin "$LOCAL_BRANCH"; then
  echo "Erreur lors du push. Vérifiez votre configuration ou vos permissions."
  exit 1
fi

echo "Push effectué avec succès ! 😊"
