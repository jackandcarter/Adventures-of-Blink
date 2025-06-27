#!/usr/bin/env bash
# Ensure core dependencies for running Unity tests via Docker
set -e

if ! command -v docker >/dev/null 2>&1; then
    echo "Docker is required to run tests. Please install Docker and rerun." >&2
    exit 1
fi

# Determine Unity version from the project settings so the image
# always matches the editor used by the project.
PROJECT_VERSION_FILE="${PROJECT_VERSION_FILE:-ProjectSettings/ProjectVersion.txt}"
if [[ -f "$PROJECT_VERSION_FILE" ]]; then
    UNITY_VERSION=$(grep m_EditorVersion "$PROJECT_VERSION_FILE" | awk '{print $2}')
else
    UNITY_VERSION="2022.3.0f1"
fi
IMAGE="unityci/editor:${UNITY_VERSION}-base-0"

# Pull the Unity editor image if it does not exist locally
if ! docker image inspect "$IMAGE" >/dev/null 2>&1; then
    echo "Pulling Unity image $IMAGE..."
    docker pull "$IMAGE"
fi

echo "Unity image $IMAGE is available."

echo "\nSetup complete. Run ./scripts/run-tests.sh to execute Edit Mode tests." 
