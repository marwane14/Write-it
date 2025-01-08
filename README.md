# Write-it

**Write-it** est un jeu vidéo éducatif conçu pour aider les enfants et les adultes dysgraphiques à améliorer leurs compétences en écriture. En complément, il peut être joué sous une forme dactylographique pour développer la vitesse et la précision de frappe au clavier.

## Fonctionnalités principales
- **Mode éducatif** : Propose des exercices interactifs pour améliorer la motricité fine et la coordination.
- **Mode dactylographie** : Permet aux utilisateurs de s'entraîner à taper rapidement et avec précision.
- **Adapté à tous les âges** : Convient aussi bien aux enfants qu'aux adultes.
- **Progression personnalisée** : Suivi des progrès et ajustement des niveaux en fonction des performances.

## Prérequis
Avant de commencer, assurez-vous d'avoir les éléments suivants :
- Un ordinateur avec un système d'exploitation Windows, macOS ou Linux.
- [Git](https://git-scm.com/) installé pour la gestion du dépôt.
- Python (version 3.8 ou ultérieure).

## Installation

1. Clonez le dépôt GitHub :
   ```bash
   git clone https://github.com/marwane14/Write-it.git
   ```
2. Naviguez dans le répertoire du projet :
   ```bash
   cd Write-it
   ```
3. Installez les dépendances requises :
   ```bash
   pip install -r requirements.txt
   ```

## Lancement du jeu

1. Exécutez le fichier principal pour démarrer le jeu :
   ```bash
   python main.py
   ```
2. Suivez les instructions à l'écran pour choisir entre le mode éducatif et le mode dactylographique.

## Gestion des branches Git

Pour contribuer au projet, voici quelques commandes utiles :

- Créer et passer sur une nouvelle branche :
  ```bash
  git checkout -b [nom_de_la_branche]
  ```
- Ajouter les modifications :
  ```bash
  git add .
  ```
- Committer avec un message :
  ```bash
  git commit -m "Votre message ici"
  ```
- Pousser les modifications :
  ```bash
  git push origin [nom_de_la_branche]
  ```

## Scripts Git pour simplifier le processus

- **setup_git.sh** : Configure automatiquement le projet Git.
- **push_git.sh** : Ajoute les fichiers modifiés, demande un message de commit et pousse les changements.
- **pull_git.sh** : Récupère les modifications distantes et les fusionne.
- **merge_git.sh** : Permet de fusionner une branche distante dans la branche locale.

## Contribution

Les contributions sont les bienvenues ! Pour contribuer :
1. Forkez le dépôt.
2. Créez une branche pour vos modifications.
3. Soumettez une pull request avec une description claire de vos changements.

## Licence

Ce projet est sous licence personnelle. Consultez le fichier [LICENSE](./LICENSE) pour plus d'informations.
