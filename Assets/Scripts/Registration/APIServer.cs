using System.Collections.Generic;

[System.Serializable]
public class ApiServer
{
    public object calAmpUserName;
    public object calAmpPassword;
    public List<TeamList> teamList;
    public UserRole userRole;
    public bool status;
    public int recordID;
    public string message;
    public object record;
}
[System.Serializable]
public class TeamList
{
    public int teamId;
    public string teamName;
    public int vehicleID;
}

[System.Serializable]
public class UserRole
{
    public bool disabled;
    public object group;
    public bool selected;
    public string text;
    public string value;
}



