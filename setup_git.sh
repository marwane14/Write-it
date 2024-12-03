#!/bin/bash

echo "Automatisation des commandes Git en cours..."

# Vérifier si Git est installé
if ! command -v git &> /dev/null; then
  echo "Git n'est pas installé. Veuillez l'installer avant de lancer ce script."
  exit 1
fi

# Configurer les informations utilisateur si nécessaires
if ! git config --global user.name &> /dev/null || ! git config --global user.email &> /dev/null; then
  read -p "Entrez votre nom Git (user.name) : " USER_NAME
  git config --global user.name "$USER_NAME"
  
  read -p "Entrez votre email Git (user.email) : " USER_EMAIL
  git config --global user.email "$USER_EMAIL"
fi

# Demande l'URL du dépôt
read -p "Entrez l'URL de votre dépôt GitHub : " REPO_URL

# Obtenir le nom du dossier du dépôt
REPO_NAME=$(basename "$REPO_URL" .git)

# Cloner ou accéder au dépôt
if [ ! -d "$REPO_NAME" ]; then
  git clone "$REPO_URL" || { echo "Erreur lors du clonage du dépôt."; exit 1; }
  cd "$REPO_NAME" || exit 1
else
  cd "$REPO_NAME" || exit 1
  echo "Dépôt déjà cloné. Passage au répertoire du projet."
fi

# Vérifier que l'origine est correctement configurée
if ! git remote | grep -q origin; then
  git remote add origin "$REPO_URL"
fi

# Gestion des branches
read -p "Entrez le nom de la branche à créer ou passer : " BRANCH_NAME
if ! git checkout "$BRANCH_NAME" 2>/dev/null; then
  git checkout -b "$BRANCH_NAME" || { echo "Erreur lors de la création ou du changement de branche."; exit 1; }
fi

# Afficher les branches locales et distantes
git branch -a

# Suivi des fichiers
git add .

# Demander le message du commit
read -p "Entrez le message du commit : " COMMIT_MSG
if ! git commit -m "$COMMIT_MSG"; then
  echo "Aucun changement à valider. Ignoré."
fi

# Pousser les modifications
if ! git push -u origin "$BRANCH_NAME"; then
  echo "Erreur lors de la poussée des modifications. Veuillez vérifier votre configuration."
  exit 1
fi

# Synchronisation avec GitHub : récupérer les mises à jour
git fetch || { echo "Erreur lors du fetch."; exit 1; }
git pull origin "$BRANCH_NAME" || { echo "Erreur lors du pull."; exit 1; }

echo "Automatisation terminée avec succès ! 😊"
