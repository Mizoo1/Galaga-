﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Galaga.Menu
{
    public class LevelMenu : IGameState
    {
        // Fields to store the SDL window and renderer
        private IntPtr _window;
        private IntPtr _renderer;

        // Fields to store the level menu textures and rectangles
        private IntPtr _easyTexture;
        private SDL.SDL_Rect _easyRect;
        private IntPtr _mediumTexture;
        private SDL.SDL_Rect _mediumRect;
        private IntPtr _hardTexture;
        private SDL.SDL_Rect _hardRect;
        private IntPtr _backTexture;
        private SDL.SDL_Rect _backButtonRect;
        private IntPtr _hintergrungTexture;
        private SDL.SDL_Rect _hintergrungRect;

        // Constructor
        public LevelMenu(IntPtr window, IntPtr renderer)
        {
            _window = window;
            _renderer = renderer;

            _hintergrungTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Background.png");

            _hintergrungRect = new SDL.SDL_Rect()
            {
                x = 0,
                y = 0,
                w = 640,
                h = 480
            };
            // Load the level menu textures and set the level menu rectangles
            _easyTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Easy.png");
            _easyRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 100,
                w = 150,
                h = 50

            };
            _mediumTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Medium.png");
            _mediumRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 180,
                w = 150,
                h = 50
            };
            _hardTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Hard.png");
            _hardRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 260,
                w = 150,
                h = 50
            };
            _backTexture = SDL_image.IMG_LoadTexture(_renderer,
                "D:\\Muaaz\\Studim\\Semester 3\\c#\\Github\\Galaga-\\Galaga\\Assest\\Back.png");
            _backButtonRect = new SDL.SDL_Rect()
            {
                x = 400,
                y = 340,
                w = 150,
                h = 50
            };
        }

        // Method to display the level menu
        public void Update()
        {
            UpdateButtonPositions();
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player clicked on the back button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _hintergrungRect.x && x <= _hintergrungRect.x + _hintergrungRect.w &&
                        y >= _hintergrungRect.y && y <= _hintergrungRect.y + _hintergrungRect.h)
                    {
                        // Transition back to the main menu state
                        GameState.SetState(new MainMenu(_window, _renderer));
                    }
                }
            }
        }

        public void Draw()
        {
            // Clear the renderer
            SDL.SDL_RenderClear(_renderer);
            // Render the level menu textures
            SDL.SDL_RenderCopy(_renderer, _hintergrungTexture, IntPtr.Zero, ref _hintergrungRect);
            SDL.SDL_RenderCopy(_renderer, _easyTexture, IntPtr.Zero, ref _easyRect);
            SDL.SDL_RenderCopy(_renderer, _mediumTexture, IntPtr.Zero, ref _mediumRect);
            SDL.SDL_RenderCopy(_renderer, _hardTexture, IntPtr.Zero, ref _hardRect);
            SDL.SDL_RenderCopy(_renderer, _backTexture, IntPtr.Zero, ref _backButtonRect);
        }

        // Method to handle player input
        public void HandleInput()
        {
            // Poll for events
            SDL.SDL_Event e;
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                // Check if the player pressed the Esc key
                if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE)
                {
                    // Transition to the settings menu state
                    GameState.SetState(new SettingsMenu(_window, _renderer));
                }
                // Check if the player clicked on the back button
                if (e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && e.button.button == SDL.SDL_BUTTON_LEFT)
                {
                    int x = e.button.x;
                    int y = e.button.y;
                    if (x >= _backButtonRect.x && x <= _backButtonRect.x + _backButtonRect.w &&
                        y >= _backButtonRect.y && y <= _backButtonRect.y + _backButtonRect.h)
                    {
                        // Transition to the main menu state
                        GameState.SetState(new MainMenu(_window, _renderer));
                    }
                }


            }
        }
        private void UpdateButtonPositions()
        {
            int mouseX, mouseY;
            SDL.SDL_GetMouseState(out mouseX, out mouseY);

            if (mouseX >= _easyRect.x && mouseX <= _easyRect.x + _easyRect.w &&
                mouseY >= _easyRect.y && mouseY <= _easyRect.y + _easyRect.h && _easyRect.x > 370)
            {
                // Mouse is hovering over the sound button
                _easyRect.x -= 1; 
            }
            else if (_easyRect.x < 400)
            {
                _easyRect.x += 1; 
            }


            if (mouseX >= _mediumRect.x && mouseX <= _mediumRect.x + _mediumRect.w &&
                mouseY >= _mediumRect.y && mouseY <= _mediumRect.y + _mediumRect.h && _mediumRect.x > 370)
            {
                _mediumRect.x -= 1;
            }
            else if (_mediumRect.x < 400)
            {
                _mediumRect.x += 1;
            }

            if (mouseX >= _hardRect.x && mouseX <= _hardRect.x + _hardRect.w &&
                mouseY >= _hardRect.y && mouseY <= _hardRect.y + _hardRect.h && _hardRect.x > 370)
            {
                _hardRect.x -= 1;
            }
            else if (_hardRect.x < 400)
            {
                _hardRect.x += 1;
            }

            if (mouseX >= _backButtonRect.x && mouseX <= _backButtonRect.x + _backButtonRect.w &&
                mouseY >= _backButtonRect.y && mouseY <= _backButtonRect.y + _backButtonRect.h && _backButtonRect.x > 370)
            {
                _backButtonRect.x -= 1;
            }
            else if (_backButtonRect.x < 400)
            {
                _backButtonRect.x += 1;
            }
        }
    }
}
