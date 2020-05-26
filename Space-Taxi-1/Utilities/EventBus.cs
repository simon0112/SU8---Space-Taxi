using DIKUArcade.EventBus;

namespace SpaceTaxi_1.Utilities {
    public static class EventBus {
        private static GameEventBus<object> eventBus;

        ///<summary> returns a new  GameEventBus<object>() instance <summary/>
        ///<return> GameEventBus<object> object </returns> 
        public static GameEventBus<object> GetBus() {
            return EventBus.eventBus ?? (EventBus.eventBus = new GameEventBus<object>());
        }
    }
}