using System.Collections.Generic;

[System.Serializable]
public class TestimonyData {
    public int id = 0;
    public string Testifier = "Woodcutter";
    public string testimony = "There was once a man.";
    public string description = "Does nothing you fool!";
    public int effectId = 0;
}

[System.Serializable]
public class TestimonyList {
    public List<TestimonyData> list = new List<TestimonyData>();
}