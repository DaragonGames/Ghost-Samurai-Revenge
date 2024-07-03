using System.Collections.Generic;

[System.Serializable]
public class TestimonyData {
    public int id;
    public string testifier;
    public string testimony;
    public string description;
    public int effectId;
}

[System.Serializable]
public class TestimonyList {
    public List<TestimonyData> list = new List<TestimonyData>();
}