using Starlight;

public sealed class IntentClassificatorSingleton {
    private static IntentClassificator instance = null;
    private static readonly object padlock = new object();

    IntentClassificatorSingleton() {
    }

    public static IntentClassificator Instance {
        get {
            lock (padlock) {
                if (instance == null) {
                    instance = new IntentClassificator();
                }
                return instance;
            }
        }
    }
}