# TowerDefense2D

## Requirements

- Unity 2020.27f1 (via Unity Hub)
- Python 3.x only for command line build support

## Stages of prototyping

### Stage 1

#### Entities

Enemy
- move: walking path
- attack: range, interval, damage

Walking path for enemies
- waypoints

Players base
- health: current, start

#### Game flow

- Enemies spawn in multiple waves (with configurable delays).
- Enemies walking towards players base.
- Enemies attack players base when in range.
- Players base loses health through attacks of enemies.
- When players base health is equal or below zero, it's game over and a dialog to restart or quit the game is shown.

### Stage 2

Players base has an indicator that shows the current amount of its health points (in comparison to start health; e.g. 50/100).

