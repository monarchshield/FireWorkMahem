using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AIE
{
    public abstract class IGameState
    {
        public IGameState()
        {
            Content = AIE.GameStateManager.Game.Content;
            UpdateBlocking = false;
            DrawBlocking = false;
        }

        public IGameState(bool updateBlocking, bool drawBlocking)
        {
            Content = AIE.GameStateManager.Game.Content;
            UpdateBlocking = updateBlocking;
            DrawBlocking = drawBlocking;
        }

        /// <summary>
        /// Update will be called by the game state manager 
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draw will be called by the game state manager 
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        
        /// <summary>
        /// OnPush will be called by the gamestate manager just before
        /// this state is added to to the Game State Stack
        /// </summary>
        public abstract void OnPush();

        /// <summary>
        /// OnPop will be called by the game state manager just before
        /// it has been removed from the game state stack
        /// </summary>
        public abstract void OnPop();

        /// <summary>
        /// when on the game state stack, if update blocking is true, states benieth it will not be updated
        /// this could be good for implementing things such as a pause menu
        /// where the "pause" state is updat blocking causeing the "game" benieth it to not update.
        /// </summary>
        public bool UpdateBlocking  { get; set; }

        /// <summary>
        /// when on the game state stack, if draw blocking is true, states benieth it will not be drawn
        /// this could be good for implementing things such as a full screen dialog box or options, where you do not 
        /// want or need the game benieth it to be rendered. 
        /// </summary>
        public bool DrawBlocking    { get; set; }

        public ContentManager Content;
    }

    public static class GameStateManager
    {
        /// <summary>
        /// helper property for getting access to the Game object easily.
        /// 
        /// </summary>
        public static Game Game
        {
            get { return m_game; }
        }

        // static constructor
        static GameStateManager()
        {
            m_gameStates        = new Dictionary<string, IGameState>();
            m_stateStack        = new List<IGameState>();
            m_gameStateEvents   = new List<GameStateEvents>();
        }

        /// <summary>
        /// should be called before any other function is called in the GameStateManager
        /// should be initialised within "Game1.cs" Initialise function.
        /// </summary>
        /// <param name="game"></param>
        public static void Initialise(Game game)
        {
            m_game = game;
        }

        /// <summary>
        /// This function will ascoaciate a Game State Instance with a name.
        /// To reload a game state, call this function with a new instance of the GameState
        /// </summary>
        /// <param name="name">string to ascoaciate with the state</param>
        /// <param name="state">an instance of a game state object</param>
        public static void SetState(string name, IGameState state)
        {
            m_gameStates[name] = state;
        }

        /// <summary>
        /// Whatever state is currently on top of the stack will change to the new state on the next update
        /// </summary>
        /// <param name="name">The name ascoaciated with a game state instance... must have been previously defined via the SetState function</param>
        public static void ChangeState(string name)
        {
            if (m_gameStates.ContainsKey(name) == false)
                throw new Exception("The game state \"name\" does not exist");

            m_gameStateEvents.Add(new GameStateEvents("", GameStateCommands.POP));
            m_gameStateEvents.Add(new GameStateEvents(name, GameStateCommands.PUSH));
        }

        /// <summary>
        /// Will add a new state onto the game state stack
        /// this state will render on top of any other states that is currently on the stack
        /// Other states will continue to be updated and drawn as normal.
        /// </summary>
        /// <param name="name">The name ascoaciated with a game state instance... must have been previously defined via the SetState function</param>
        public static void PushState(string name)
        {
            if (m_gameStates.ContainsKey(name) == false)
                throw new Exception("The game state \"name\" does not exist");

            m_gameStateEvents.Add(new GameStateEvents(name, GameStateCommands.PUSH));
        }

        /// <summary>
        /// the state on top of the stack will be removed
        /// </summary>
        public static void PopState()
        {
            m_gameStateEvents.Add(new GameStateEvents("", GameStateCommands.POP));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">the game state instance to check if is on top of the game state stack</param>
        /// <returns>True if the state is on top of the stack</returns>
        public static bool IsStateOnTop(IGameState state)
        {
            if (m_stateStack.Count == 0) return false;
            return m_stateStack[ m_stateStack.Count - 1 ] == state;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">The name ascoaciated with a game state instance... must have been previously defined via the SetState function</param>
        /// <returns></returns>
        public static bool IsStateOnTop(string name)
        {
            if (m_gameStates.ContainsKey(name) == false)
                throw new Exception("The game state \"name\" does not exist");

            return IsStateOnTop(m_gameStates[name]);
        }

        /// <summary>
        /// will update all game states on the game state stack and process push, pop or change commands that has been called
        /// </summary>
        /// <param name="gameTime">Game time should be passed around all update functions </param>
        public static void UpdateGameStates(GameTime gameTime)
        {
            ProcessGameStateEvents();

            for (int i = 0; i < m_stateStack.Count; i++)
            {
                bool isBlocked = false;

                for (int j = i + 1; j < m_stateStack.Count; j++)
                    isBlocked = isBlocked & m_stateStack[j].UpdateBlocking;
                        

                if( isBlocked == false )
                    m_stateStack[i].Update(gameTime);
            }

            foreach (IGameState state in m_stateStack)
                state.Update(gameTime);
        }

        /// <summary>
        /// will draw all games states on the game state stack.
        /// states on top of the stack will be drawn on top of previous states.
        /// </summary>
        /// <param name="gameTime">Game time should be passed around all draw functions </param>
        /// <param name="spriteBatch">the sprite batch object to preform 2D rendering</param>
        public static void DrawGameStates(GameTime gameTime, SpriteBatch spriteBatch)
        {

            for (int i = 0; i < m_stateStack.Count; i++)
            {
                bool isBlocked = false;

                for (int j = i + 1; j < m_stateStack.Count; j++)
                    isBlocked = isBlocked & m_stateStack[j].DrawBlocking;

                if (isBlocked == false)
                    m_stateStack[i].Draw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// processes all recorded Push, Pop and Change events
        /// clears the event log for the next frame.
        /// Internal use only, called by the UpdateGameStates Function
        /// </summary>
        private static void ProcessGameStateEvents()
        {
            foreach (GameStateEvents e in m_gameStateEvents)
            {
                if ( e.cmd == GameStateCommands.PUSH)
                {
                    if( m_gameStates.ContainsKey(e.name) == false ) continue;
                    if (m_gameStates[e.name] != null)
                    {
                        m_gameStates[e.name].OnPush();
                        m_stateStack.Add(m_gameStates[e.name]);
                    }
                }
                if (e.cmd == GameStateCommands.POP)
                {
                    if (m_stateStack.Count == 0) continue;
                    m_stateStack[m_stateStack.Count - 1].OnPop();
                    m_stateStack.RemoveAt(m_stateStack.Count - 1);
                }
            }

            m_gameStateEvents.Clear();
        }

        // private variables and types
        //---------------------------------------------------------------------

        // Game states are assigned a name and set a value
        // via the SetState function, and stored in this variable.
        private static Dictionary<string, IGameState> m_gameStates;

        // List of Gamestates that are being updated and drawn
        private static List<IGameState> m_stateStack;

        // list of events that have occured (PUSH or POP )
        // between updates...
        private static List<GameStateEvents> m_gameStateEvents;

        // reference to the Game object
        private static Game m_game;
        
        enum GameStateCommands
        {
            PUSH,
            POP
        }

        struct GameStateEvents
        {
            public string name;
            public GameStateCommands cmd;

            public GameStateEvents(string name, GameStateCommands cmd)
            {
                this.name = name;
                this.cmd = cmd;
            }
        }
        //---------------------------------------------------------------------
    }
}
