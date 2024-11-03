#include "raylib/raylib.h"
#include <iostream>
#include <raylib.h>
#include <cstdlib> // For rand() and srand()
#include <ctime>   // For time()

using namespace std;
int num1, num2, num3;
bool running = true; // Process running
float playerspeed = 3;
Texture2D asset[3]; // Asset textures
Texture2D bg, mid_bg;
Texture2D playerSprite; // Declare the player texture

Rectangle playerDest; // Destination rectangle for the player
Rectangle playerSrc;  // Source rectangle for the player

// Constants for screen dimensions
const int SCREEN_WIDTH = 384;
const int SCREEN_HEIGHT = 288;

// Flag to track whether assets need to be updated
bool updateAssets = true;

// Function to populate assets
void populate() {
    if (updateAssets) {
        num1 = rand() % 3;
        num2 = rand() % 3;
        num3 = rand() % 3;

        updateAssets = false; // Set the flag to false after populating
    }
}

// Initialize the game window and settings
void init() {
    populate();
    InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, "Project Kira");
    SetExitKey(0); // Disable the default exit key
    SetTargetFPS(60); // Set the target frames per second

    // Load textures
    bg = LoadTexture("background.png");
    mid_bg = LoadTexture("middleground.png");
    asset[0] = LoadTexture("house-a.png");
    asset[1] = LoadTexture("wagon.png");
    asset[2] = LoadTexture("crate-stack.png");

    playerSprite = LoadTexture("Basic Charakter Spritesheet.png");

    // Initialize source and destination rectangles for the player
    playerSrc = {0, 0, 48, 48}; // Source rectangle: part of the texture to draw
    playerDest = {20, 244, 50, 50}; // Destination rectangle: where to draw it on screen
}

// Draw the game scene
void drawscene() {
    DrawTexture(bg, 0, 0, WHITE); 
    DrawTexture(mid_bg, 0, 0, WHITE); 
    DrawTexture(asset[num1], 0, 64, WHITE);  // First texture
    DrawTexture(asset[num2], 168, 64, WHITE); // Second texture
    DrawTexture(asset[num3], 336, 64, WHITE);  // Third texture
    DrawTexturePro(playerSprite, playerSrc, playerDest, (Vector2){playerDest.width / 2, playerDest.height / 2}, 0.0f, WHITE);
}

// Handle user input
void input() {
    if (IsKeyDown(KEY_A) || IsKeyDown(KEY_LEFT)) {
        playerDest.x -= playerspeed;
    }

    if (IsKeyDown(KEY_D) || IsKeyDown(KEY_RIGHT)) {
        playerDest.x += playerspeed;
    }

    // Check if the player moves out of the screen
    if (playerDest.x > SCREEN_WIDTH) {
        playerDest.x = 0; // Reset the player's x position to the left side
        updateAssets = true; // Set the flag to update assets
        populate();
    }
}

// Render the scene
void render() {
    BeginDrawing();
    ClearBackground(RAYWHITE); // Clear the background
    drawscene();
    EndDrawing();
}

// Update game state
void update() {
    running = !WindowShouldClose();
}

// Clean up and close the game window
void quit() {
    for (int i = 0; i < 3; i++) {
        UnloadTexture(asset[i]); 
    }
    UnloadTexture(bg);
    UnloadTexture(mid_bg);
    UnloadTexture(playerSprite); // Unload the player texture
    CloseWindow(); // Close the window
}

int main() {
    srand(static_cast<unsigned int>(time(0))); // Seed for random number generation
    init(); // Initialize the game
    // Main game loop
    while (running) {
        input();  // Handle input
        update(); // Update game state
        render(); // Render the scene
    }

    quit(); // Clean up resources
    return 0;
}
