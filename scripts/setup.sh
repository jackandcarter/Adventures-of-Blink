#!/usr/bin/env bash
# Ensure core dependencies for running Unity tests via Docker
set -e

if ! command -v docker >/dev/null 2>&1; then
    echo "Docker is required to run tests. Please install Docker and rerun." >&2
    exit 1
fi

# Default Unity version and image tag used for local test runs
UNITY_VERSION="2022.3.0f1"
IMAGE="unityci/editor:${UNITY_VERSION}-base-0"

# Pull the Unity editor image if it does not exist locally
if ! docker image inspect "$IMAGE" >/dev/null 2>&1; then
    echo "Pulling Unity image $IMAGE..."
    docker pull "$IMAGE"
fi

echo "Unity image $IMAGE is available."

echo "\nSetup complete. Run ./scripts/run-tests.sh to execute Edit Mode tests." 
