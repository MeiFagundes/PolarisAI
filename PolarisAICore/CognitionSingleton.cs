using Starlight;

public sealed class CognitionSingleton {
    private static ClassificationController instance = null;
    private static readonly object padlock = new object();

    CognitionSingleton() {
    }

    public static ClassificationController Instance {
        get {
            lock (padlock) {
                if (instance == null) {
                    instance = new ClassificationController();
                }
                return instance;
            }
        }
    }
}