# Targeting Guide

This guide explains how to configure the targeting system so the player can cycle between enemies and display their information in the UI.

## Setup Steps
1. Add the `TargetingSystem` component to your player or a persistent manager object.
2. On each enemy or NPC, attach `Targetable` and fill in **displayName**. Add a `Health` component if the target should display hit points.
3. Create a panel named `TargetPanel` under your canvas.
   - Add a `TMP_Text` element for the name display and a `Slider` for health.
   - Attach the `TargetPanel` script and assign the `TargetingSystem`, `TMP_Text`, and `Slider` fields.
4. During play press the **Tab** key to cycle through nearby targets.
