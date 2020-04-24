using DIKUArcade.EventBus;

namespace SpaceTaxi_1.Utilities {
    public static class EventBus {
        private static GameEventBus<object> eventBus;

        /// <summary>
        /// Gets the event bus if there is one otherwise it creates a new event bus.
        /// </summary>
        /// <returns>Event bus.</returns>

        //<summary> returns a new  GameEventBus<object>() instance <summary/>
        //<input> none </insput>
        //<return> GameEventBus<object> object </insput> 
        public static GameEventBus<object> GetBus() {
            return EventBus.eventBus ?? (EventBus.eventBus = new GameEventBus<object>());
        }
    }
}