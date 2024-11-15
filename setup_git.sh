#!/bin/bash

echo "Automatisation des commandes Git en cours..."

# Demande l'URL du dépôt
read -p "Entrez l'URL de votre dépôt GitHub : " REPO_URL

# Si le dossier n'existe pas encore, clone le projet
if [ ! -d "$(basename $REPO_URL .git)" ]; then
  git clone $REPO_URL
  cd "$(basename $REPO_URL .git)"
else
  cd "$(basename $REPO_URL .git)"
  echo "Dépôt déjà cloné. Passage au répertoire du projet."
fi

# Initialisation du projet (au cas où le remote origin ne serait pas déjà configuré)
git remote add origin $REPO_URL

# Gestion des branches : Créer ou passer sur la branche spécifiée
read -p "Entrez le nom de la branche à créer ou passer : " BRANCH_NAME
git checkout -b $BRANCH_NAME || git checkout $BRANCH_NAME

# Afficher les branches locales et distantes
git branch -a

# Suivi des fichiers
git add .

# Demander le message du commit
read -p "Entrez le message du commit : " COMMIT_MSG
git commit -m "$COMMIT_MSG"

# Pousser les modifications vers la branche distante
git push -u origin $BRANCH_NAME

# Synchronisation avec GitHub : récupérer les mises à jour
git fetch
git pull origin $BRANCH_NAME

echo "Automatisation terminée avec succès ! 😊"
