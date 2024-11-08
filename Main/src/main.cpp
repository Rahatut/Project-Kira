#include <iostream>
#include <raylib.h>
#include <cstdlib> // For rand() and srand()
#include <ctime>   // For time()

using namespace std;

bool running = true; // Process running
float playerspeed = 6.0f, bg_speed = 4.0f;
float wrap_1 = 0, wrap_2 = 0;
Texture2D playerSprite, bg;
int framewidth = 96;
int gravity = 1;
int ground = 500; // Adjusted for a more visible ground position
bool playerMoving = false;
// Player position and velocity
Vector2 playerPosition;
Vector2 playerVelocity;

// Source rectangle for player animation
Rectangle playerSrc;
Rectangle playerDest; // Destination rectangle for player position
Rectangle bgSrc;
Rectangle bgDest;

unsigned frameDelay = 5;
unsigned frameDelayCounter = 0;
unsigned frameIndex = 0;
unsigned jumpUpFrame = 3;     // Adjusted jump-up frame
unsigned jumpDownFrame = 4;   // Adjusted jump-down frame
unsigned numFrames = 6;       // Number of animation frames

// Constants for screen dimensions
const int SCREEN_WIDTH = 1500;
const int SCREEN_HEIGHT = 800;
const int LEFT_BOUNDARY = 200; // Define the left boundary for player movement
const int RIGHT_BOUNDARY = 1000; // Define the right boundary for player movement

// Initialize the game window and settings
void init() {
    InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, "Project Kira");
    SetExitKey(0); // Disable the default exit key
    SetTargetFPS(60); // Set the target frames per second

    // Load player texture and initialize animation frame
    playerSprite = LoadTexture("RUN.png");
    bg = LoadTexture("background.png");
    playerSrc = {0, 0, 96, 96}; // Source rectangle: part of the texture to draw
    playerDest = {SCREEN_WIDTH / 2.0f, SCREEN_HEIGHT / 2.0f, 50, 50}; // Center player
    bgSrc = {0, 0, 768, 284};

    playerPosition = {50, ground - playerSrc.height};
    playerVelocity = {0.0f, 0.0f};
}

// Function to check if the player is on the ground
bool IsOnGround() {
    return playerPosition.y >= ground - playerSrc.height;
}

// Draw the game scene
void drawscene() {
    // Draw the background with wrap-around effect
    DrawTexturePro(bg, bgSrc, {0, 0, 1500, 800}, {0 + wrap_1, 0}, 0.0f, WHITE);
    DrawTexturePro(bg, bgSrc, {0, 0, 1500, 800}, {-1500 + wrap_2, 0}, 0.0f, WHITE);
    DrawTexturePro(playerSprite, playerSrc, {playerPosition.x, playerPosition.y, 400, 400}, {0, 48}, 0.0f, WHITE);
}

// Handle user input and update animation
void input() {
    playerVelocity.x = 0.0f; // Reset horizontal velocity
    bool playerMoving = false;

    // Check if player is on the ground to allow jump
    if (IsOnGround() && IsKeyDown(KEY_SPACE)) {
        playerVelocity.y = -3 * playerspeed; // Jump
        playerMoving = true;
    }

    // Horizontal movement (left/right)
    if (IsKeyDown(KEY_A) || IsKeyDown(KEY_LEFT)) {
        playerMoving = true;
        
        // Move background only if player is at left boundary
        if (playerPosition.x <= LEFT_BOUNDARY) {
            wrap_1 -= bg_speed;
            wrap_2 -= bg_speed;
        } else {
            playerVelocity.x = -playerspeed;
        }
        
        if (playerSrc.width > 0) {
            playerSrc.width = -playerSrc.width; // Face left
        }
    }
    if (IsKeyDown(KEY_D) || IsKeyDown(KEY_RIGHT)) {
        playerMoving = true;
        
        // Move background only if player is at right boundary
        if (playerPosition.x >= RIGHT_BOUNDARY) {
            wrap_1 += bg_speed;
            wrap_2 += bg_speed;
        } else {
            playerVelocity.x = playerspeed;
        }
        
        if (playerSrc.width < 0) {
            playerSrc.width = -playerSrc.width; // Face right
        }
    }

    // Apply gravity if player is in the air
    if (!IsOnGround()) {
        playerVelocity.y += gravity; // Apply gravity when airborne
    }

    // Update player position based on velocity
    playerPosition.x += playerVelocity.x;
    playerPosition.y += playerVelocity.y;

    // Check if player has hit the ground
    if (playerPosition.y >= ground - playerSrc.height) {
        playerVelocity.y = 0; // Stop downward velocity
        playerPosition.y = ground - playerSrc.height; // Set to ground level
    }

    // Update animation frame based on movement and jumping state
    if (playerMoving) {
        frameDelayCounter++;
        if (frameDelayCounter >= frameDelay) {
            frameDelayCounter = 0;

            // Jumping frames
            if (playerVelocity.y < 0) {
                frameIndex = jumpUpFrame; // Jumping up frame
            } else if (playerVelocity.y > 0 && !IsOnGround()) {
                frameIndex = jumpDownFrame; // Falling frame
            } else {
                // Regular running/walking animation
                frameIndex++;
                frameIndex %= 9; // Loop animation frames
            }
            playerSrc.x = (float)frameIndex * framewidth;
        }
    } else {
        // Reset to the first frame if no key is pressed
        frameIndex = 0;
        playerSrc.x = 0;
    }

}

// Render the scene
void render() {
    BeginDrawing();
    ClearBackground(RAYWHITE); // Clear the background
    drawscene();               // Draw the game scene
    EndDrawing();
}

// Update game state
void update() {
    running = !WindowShouldClose();
}

// Clean up and close the game window
void quit() {
    UnloadTexture(playerSprite); // Unload the player texture
    CloseWindow();               // Close the window
}

int main() {
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
