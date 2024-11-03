#include "raylib/raylib.h"
#include <iostream>
#include <cstdlib> // For rand() and srand()
#include <ctime>   // For time()

using namespace std;

int currentBackground = 0;//tracking bg changes

bool running = true;//process running

float playerspeed = 3;

Texture2D background[3]; // Declare the texture globally
Texture2D playerSprite; // Declare the player texture

Rectangle playerDest; // Destination rectangle for the player
Rectangle playerSrc;  // Source rectangle for the player

// Constants for screen dimensions
const int SCREEN_WIDTH = 800;
const int SCREEN_HEIGHT = 433;

// Initialize the game window and settings
void init() {
    InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, "Project Kira");
    SetExitKey(0); // Disable the default exit key
    SetTargetFPS(60); // Set the target frames per second

    // Load textures
    background[0] = LoadTexture("moon2.png");
    background[1]= LoadTexture("test_12.png");
    background[2]= LoadTexture("test_22.png");

    playerSprite = LoadTexture("Basic Charakter Spritesheet.png"); // Ensure you have "Player.png" in the correct directory

    // Initialize source and destination rectangles for the player
    playerSrc = {0, 0, 48, 48}; // Source rectangle: part of the texture to draw
    playerDest = {200, 200, 100, 100}; // Destination rectangle: where to draw it on screen
}

// void populate(){

//     int num1 = rand() % 2;
//     int num2 = rand() % 2;

//     // Draw the randomly chosen textures
//     DrawTexture(asset[num1], 0, x, WHITE);  // First texture
//     DrawTexture(asset[num2], x, y, WHITE); // Second texture

// }

// Draw the game scene
void drawscene() {
    DrawTexture(background[currentBackground], 0, 0, WHITE); 
    
    // Draw player texture with transformation
    DrawTexturePro(playerSprite, playerSrc, playerDest, (Vector2){playerDest.width / 2, playerDest.height / 2}, 0.0f, WHITE);
}

// Handle user input
void input() {

    if(IsKeyDown(KEY_W) || IsKeyDown(KEY_UP)){

        playerDest.y -= playerspeed;

    }

    if(IsKeyDown(KEY_S) || IsKeyDown(KEY_DOWN)){

        playerDest.y += playerspeed;

    }

    if(IsKeyDown(KEY_A) || IsKeyDown(KEY_LEFT)){

        playerDest.x -= playerspeed;

    }

    if(IsKeyDown(KEY_D) || IsKeyDown(KEY_RIGHT)){

        playerDest.x += playerspeed;

    }

    if (playerDest.x > SCREEN_WIDTH) {
        playerDest.x = 0;  // Reset the player's x position to the left side
        currentBackground = (currentBackground+1)%3;
    }
    // Add input handling logic here
}

// Render the scene
void render() {
    BeginDrawing();
    drawscene();
    EndDrawing();
}

// Update game state
void update() {
    running = !WindowShouldClose();
}

// Clean up and close the game window
void quit() {
    for(int i=0; i<3; i++){
        UnloadTexture(background[i]); 
    }
    UnloadTexture(playerSprite); // Unload the player texture
    CloseWindow(); // Close the window
}

int main() {

    //srand(static_cast<unsigned int>(time(0)));
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

