#!/bin/bash

echo "Automatisation de la commande git merge en cours..."

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
read -p "Entrez le nom de la branche locale à fusionner : " LOCAL_BRANCH
git checkout $LOCAL_BRANCH

# Vérifier que la branche distante existe
read -p "Entrez le nom de la branche distante à fusionner avec la branche locale : " REMOTE_BRANCH

# Synchroniser avec GitHub avant de fusionner
git fetch

# Fusionner la branche distante dans la branche locale
git merge origin/$REMOTE_BRANCH

# Vérifier s'il y a des conflits de fusion
if [ $? -eq 0 ]; then
  echo "Fusion réussie sans conflits. 😊"
else
  echo "Des conflits sont survenus pendant la fusion. Veuillez les résoudre manuellement."
fi

# Pousser les modifications après fusion si nécessaire
read -p "Souhaitez-vous pousser les changements après fusion ? (y/n) : " PUSH_CHANGES
if [ "$PUSH_CHANGES" == "y" ]; then
  git push origin $LOCAL_BRANCH
  echo "Les modifications ont été poussées vers GitHub avec succès ! 😊"
else
  echo "Fusion terminée, mais les modifications n'ont pas été poussées."
fi
