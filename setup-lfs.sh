#!/bin/bash

echo "🔧 Initialisation de Git LFS..."

# =========================
# VARIABLES CONFIGURABLES
# =========================

LFS_VERSION="v3.6.1"
LFS_URL="https://github.com/git-lfs/git-lfs/releases/download/${LFS_VERSION}/git-lfs-windows-${LFS_VERSION}.exe"
INSTALLER="git-lfs-installer.exe"
TRACKED_EXTENSIONS=("*.psd" "*.png" "*.wav" "*.mp4" "*.fbx" "*.blend" "*.spp")

# =========================
# PRÉCONDITION
# =========================

if ! git rev-parse --is-inside-work-tree &> /dev/null; then
  echo "❌ Ce script doit être lancé depuis un dossier Git valide."
  exit 1
fi

# =========================
# Fonction d'installation Windows
# =========================

install_git_lfs_windows() {
    echo "⬇️ Téléchargement de Git LFS pour Windows..."
    curl -L "$LFS_URL" -o "$INSTALLER"

    if [ ! -f "$INSTALLER" ]; then
        echo "❌ Échec du téléchargement."
        exit 1
    fi

    echo "📦 Installation silencieuse..."
    ./git-lfs-installer.exe /VERYSILENT

    if [ $? -ne 0 ]; then
        echo "❌ L'installation de Git LFS a échoué."
        exit 1
    fi

    rm "$INSTALLER"
    echo "✅ Git LFS installé avec succès sous Windows."
}

# =========================
# Installation Git LFS
# =========================

if ! command -v git-lfs &> /dev/null; then
    echo "⚠️ Git LFS n'est pas installé."

    case "$OSTYPE" in
        linux-gnu*)
            sudo apt update && sudo apt install git-lfs -y
            ;;
        darwin*)
            brew install git-lfs
            ;;
        msys*|cygwin*)
            install_git_lfs_windows
            ;;
        *)
            echo "❌ OS non reconnu. Installe Git LFS manuellement : https://git-lfs.com/"
            exit 1
            ;;
    esac
else
    echo "✅ Git LFS est déjà installé."
fi

# =========================
# Configuration du repo
# =========================

git lfs install

echo "📁 Fichiers trackés par Git LFS :"
for ext in "${TRACKED_EXTENSIONS[@]}"; do
    echo "  - $ext"
    git lfs track "$ext"
done

# =========================
# Commit .gitattributes si modifié
# =========================

if git diff --cached --name-only | grep -q ".gitattributes"; then
    git add .gitattributes
    git commit -m "Ajout de Git LFS tracking"
fi

echo "🎉 Git LFS est prêt à l’emploi."
