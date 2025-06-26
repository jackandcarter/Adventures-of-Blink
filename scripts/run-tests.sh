#!/usr/bin/env bash
# Execute Unity Edit Mode tests inside a Docker container
set -e

UNITY_VERSION="2022.3.0f1"
IMAGE="unityci/editor:${UNITY_VERSION}-base-0"

PROJECT_PATH="/project"
RESULTS_DIR="/project/TestResults"

if [ -z "$UNITY_LICENSE" ]; then
    echo "UNITY_LICENSE environment variable must contain a valid Unity license." >&2
    exit 1
fi

# Ensure test results directory exists
mkdir -p TestResults

# Run the Unity Test Runner container
docker run --rm \
    -e UNITY_LICENSE="${UNITY_LICENSE}" \
    -v "$(pwd)":${PROJECT_PATH} \
    -w ${PROJECT_PATH} \
    "$IMAGE" \
    unity -batchmode -nographics -projectPath ${PROJECT_PATH} \
        -runTests -testPlatform editmode -testResults ${RESULTS_DIR}/results.xml \
        -quit

echo "Test results written to TestResults/results.xml"
