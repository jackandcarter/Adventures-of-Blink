# Targeting Guide

This guide explains how to configure the targeting system so the player can cycle between enemies and display their information in the UI.

## Setup Steps
1. Add the `TargetingSystem` component to your player or a persistent manager object.
2. On each enemy or NPC, attach `Targetable` and fill in **displayName**. Assign a `RuntimeStats` reference if health should be displayed.
3. Drag `TargetPanel.prefab` from `Assets/Prefabs` into your canvas and set its **targetingSystem** field.
4. During play press the **Tab** key to cycle through nearby targets.
