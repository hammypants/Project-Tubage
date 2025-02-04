﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PROJECT_RPG
{

    // Note: This class is relatively unusable without game content to push loading,
    // as it does not allow the ScreenManager to manage any other game screens simultaneously. -- Might need adjusting in the future, but not in our current dev state.
    class LoadingScreen : GameScreen
    {
        #region Fields

        bool otherScreensAreGone;
        GameScreen[] screensToLoad;
        
        #endregion

        #region Initialization

        // Do not use this. Use load method instead.
        private LoadingScreen(ScreenManager screenManager, GameScreen[] screensToLoad)
        {
            this.screensToLoad = screensToLoad;
            TransitionOnTime = TimeSpan.FromSeconds(1.0);
        }

        public static void Load(ScreenManager screenManager,
                                params GameScreen[] screensToLoad)
        {
            foreach (GameScreen screen in screenManager.GetScreens())
                screen.ExitScreen();

            LoadingScreen loadingScreen = new LoadingScreen(screenManager, screensToLoad);

            screenManager.AddScreen(loadingScreen);
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Only load after all the other screens are gone.
            if (otherScreensAreGone)
            {
                
                ScreenManager.RemoveScreen(this);

                foreach (GameScreen screen in screensToLoad)
                {
                    if (screen != null)
                    {
                        ScreenManager.AddScreen(screen);
                    }
                }

                // Might need this if the loading is something huge.
                ScreenManager.Game.ResetElapsedTime();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            // Don't draw anything because it's just a black screen!
            // Read somewhere that updating this stuff here makes transitions look better;
            // so we're sticking with this for now.

            if ((ScreenState == ScreenState.Active) &&
                (ScreenManager.GetScreens().Length == 1))
            {
                otherScreensAreGone = true;
            }
        }

        #endregion


    }
}
